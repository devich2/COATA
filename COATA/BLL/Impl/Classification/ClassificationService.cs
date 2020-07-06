using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Abstract.Classification;
using BLL.DTO.Classification;
using BLL.DTO.Result;
using DAL.Abstract;
using DAL.Entities.Tables;

namespace BLL.Impl.Classification
{
    public class ClassificationService : IClassificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClassificationService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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