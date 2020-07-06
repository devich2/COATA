using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Abstract.UnitTree;
using BLL.Abstract.UnitType;
using BLL.DTO.Classification;
using BLL.DTO.Response;
using BLL.DTO.Result;
using BLL.DTO.Unit;
using Common.Utils;
using DAL.Abstract;
using DAL.Abstract.Transactions;
using DAL.Entities.Tables;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.Logging;

namespace BLL.Impl.UnitTree
{
    public class UnitEditService : IUnitEditService
    {
        private readonly ITransactionManager _transactionManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitTypeCache _unitTypeCache;

        public UnitEditService(ITransactionManager transactionManager,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUnitTypeCache unitTypeCache)
        {
            _transactionManager = transactionManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _unitTypeCache = unitTypeCache;
        }

        public async Task<DataResult<UnitAddResponse>> ProcessUnitCreate(UnitCreateOrUpdateDTO model)
        {
            return await ProcessUnitCreateTransaction(model);
        }

        public async Task<DataResult<UnitUpdateResponse>> ProcessUnitUpdate(UnitCreateOrUpdateDTO model)
        {
            return await ProcessUnitUpdateTransaction(model);
        }

        public async Task<Result> ProcessUnitDelete(int unitId)
        {
            return await ProcessUnitDeleteTransaction(unitId);
        }

        #region Delete

        private async Task<Result> ProcessUnitDeleteTransaction(int unitId)
        {
            if (unitId < 1)
            {
                return new Result
                {
                    Message = ResponseMessageType.InvalidId,
                    ResponseStatusType = ResponseStatusType.Error,
                    MessageDetails = "id could not be less 1"
                };
            }

            return await _transactionManager.ExecuteInImplicitTransactionAsync(async () =>
            {
                var itemToDelete = await _unitOfWork.Units.GetByIdAsync(unitId);
                    
                if (itemToDelete != null)
                {
                    await _unitOfWork.Units.DeleteSubTree(unitId);
                    return new Result
                    {
                        ResponseStatusType = ResponseStatusType.Succeed
                    };
                }

                return new Result
                {
                    Message = ResponseMessageType.NotFound,
                    ResponseStatusType = ResponseStatusType.Error,
                    MessageDetails = "Could not find item with given id"
                };
            });
        }

        #endregion

        #region Update

        private async Task<DataResult<UnitUpdateResponse>> ProcessUnitUpdateTransaction(UnitCreateOrUpdateDTO model)
        {
            return await _transactionManager.ExecuteInImplicitTransactionAsync(async ()=>
            {
                DAL.Entities.Tables.UnitTree unitEntity = await _unitOfWork.Units.GetByIdAsync(model.Id);
                if(unitEntity == null)
                {
                    return new DataResult<UnitUpdateResponse>()
                    {
                        ResponseStatusType = ResponseStatusType.Error,
                        Message = ResponseMessageType.NotFound
                    };
                }
                if(string.IsNullOrWhiteSpace(model.Name))
                {
                    return new DataResult<UnitUpdateResponse>()
                    {
                        ResponseStatusType = ResponseStatusType.Error,
                        Message = ResponseMessageType.IncorrectParameter
                    };
                }
                unitEntity.Name = model.Name;
                await _unitOfWork.Units.UpdateAsync(unitEntity);
                await _unitOfWork.SaveAsync();
            
                return new DataResult<UnitUpdateResponse>()
                {
                    ResponseStatusType = ResponseStatusType.Succeed,
                    Data = new UnitUpdateResponse()
                    {
                        UnitId = unitEntity.Id,
                        Name = unitEntity.Name
                    }
                };
            });
            
        }

        #endregion

        #region Create

        private async Task<DataResult<UnitAddResponse>> ProcessUnitCreateTransaction(UnitCreateOrUpdateDTO model)
        {
            return await _transactionManager.ExecuteInImplicitTransactionAsync(async () =>
            {
                ClassificationDTO classification = model.Classification;
                if (classification == null)
                {
                    return new DataResult<UnitAddResponse>()
                    {
                        Message = ResponseMessageType.ClassificationMissing,
                        ResponseStatusType = ResponseStatusType.Error
                    };
                }

                DAL.Entities.Tables.UnitTree parentUnit = await _unitOfWork.Units.GetByIdExpandedAsync(model.ParentId);

                if (parentUnit == null)
                {
                    return new DataResult<UnitAddResponse>()
                    {
                        Message = ResponseMessageType.ParentIdMissing,
                        ResponseStatusType = ResponseStatusType.Error
                    };
                }

                var allowedTypes = _unitTypeCache.GetFromCache(parentUnit.UnitClassification.UnitType.Name);
                if (!allowedTypes.Any(x=>x.Id == classification.UnitType.Id && x.Name == classification.UnitType.Name))
                {
                    return new DataResult<UnitAddResponse>()
                    {
                        Message = ResponseMessageType.OperationNotAllowedForUnitType,
                        ResponseStatusType = ResponseStatusType.Error
                    };
                }

                DAL.Entities.Tables.UnitTree unitEntity = _mapper.Map<DAL.Entities.Tables.UnitTree>(model);

                UnitClassification unitClassification =
                    await _unitOfWork.UnitClassifications.FirstOrDefaultAsync(x =>
                        x.Name == classification.CustomName && Equals(x.UnitType, classification.UnitType));

                if (unitClassification == null)
                {
                    unitClassification = await _unitOfWork.UnitClassifications.AddAsync(new UnitClassification()
                    {
                        UnitTypeId = classification.UnitType.Id,
                        Name = classification.CustomName
                    });
                    await _unitOfWork.SaveAsync();
                }

                unitEntity.UnitClassificationId = unitClassification.Id;

                await _unitOfWork.Units.AddAsync(unitEntity);
                await _unitOfWork.SaveAsync();
                
                await _unitOfWork.Units.UpdateBowers(unitEntity.Id, model.ParentId);
                return new DataResult<UnitAddResponse>()
                {
                    ResponseStatusType = ResponseStatusType.Succeed,
                    Data = new UnitAddResponse()
                    {
                        UnitId = unitEntity.Id
                    }
                };
            });
        }

        #endregion
        
    }
}