using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Abstract.Classification;
using BLL.Abstract.UnitType;
using BLL.DTO.Classification;
using BLL.DTO.Result;
using DAL;
using DAL.Abstract;
using DAL.Entities.Tables;

namespace BLL.Impl.Classification
{
    public class ClassificationService : IClassificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUnitTypeCache _unitTypeCache;

        public ClassificationService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IUnitTypeCache unitTypeCache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _unitTypeCache = unitTypeCache;
        }

        public async Task<DataResult<ClassificationAddResponse>> ProcessClassificationCreate(ClassificationCreateDTO model)
        {
            DAL.Entities.Tables.UnitTree unitTree = await _unitOfWork.Units.GetByIdExpandedAsync(model.ParentId);
            if(unitTree == null)
            {
                return new DataResult<ClassificationAddResponse>()
                {
                    ResponseStatusType = ResponseStatusType.Error,
                    Message = ResponseMessageType.ParentIdMissing
                };
            }
            DAL.Entities.Tables.UnitType unitType = await _unitOfWork.UnitTypes.GetByIdAsync(model.UnitTypeId);
            if(unitType == null)
            {
                return new DataResult<ClassificationAddResponse>()
                {
                    ResponseStatusType = ResponseStatusType.Error,
                    Message = ResponseMessageType.NotFound
                };
            }
            
            List<DAL.Entities.Tables.UnitType> allowedTypes = _unitTypeCache.GetFromCache(unitTree.UnitClassification.UnitType.Name);
            if(allowedTypes.All(x => x.Id != model.UnitTypeId))
            {
                return new DataResult<ClassificationAddResponse>()
                {
                    Message = ResponseMessageType.OperationNotAllowedForUnitType,
                    ResponseStatusType = ResponseStatusType.Error
                };
            }
            if(string.IsNullOrWhiteSpace(model.Name))
            {
                return new DataResult<ClassificationAddResponse>()
                {
                    ResponseStatusType = ResponseStatusType.Error,
                    Message = ResponseMessageType.InvalidModel
                };
            }
            UnitClassification classification = _mapper.Map<UnitClassification>(model);
            await _unitOfWork.UnitClassifications.AddAsync(classification);
            await _unitOfWork.SaveAsync();
            return new DataResult<ClassificationAddResponse>()
            {
                ResponseStatusType = ResponseStatusType.Succeed,
                Data = new ClassificationAddResponse()
                {
                    ClassificationId = classification.Id
                }
            };
        }

        public async Task<DataResult<List<ClassificationDTO>>> GetClassificationByParentId(int? unitId)
        {
            List<UnitClassification> unitClassifications =
                await _unitOfWork.UnitClassifications.GetClassificationByParentId(unitId);
            if (!unitClassifications.Any())
            {
                return new DataResult<List<ClassificationDTO>>()
                {
                    ResponseStatusType = ResponseStatusType.Warning,
                    Message = ResponseMessageType.EmptyResult
                };
            }

            var mapped = unitClassifications.Select(_mapper.Map<ClassificationDTO>).ToList();
            return new DataResult<List<ClassificationDTO>>()
            {
                Data = mapped,
                ResponseStatusType = ResponseStatusType.Succeed
            };
        }
    }
}