using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Abstract.UnitType;
using BLL.DTO.Result;
using BLL.DTO.UnitType;

namespace BLL.Impl.UnitType
{
    public class UnitTypeService: IUnitTypeService
    {
        private readonly IUnitTypeCache _unitTypeCache;

        public UnitTypeService(IUnitTypeCache unitTypeCache)
        {
            _unitTypeCache = unitTypeCache;
        }
        public DataResult<UnitTypeAggrDTO> GetTypesGroupedByParents()
        {
            return new DataResult<UnitTypeAggrDTO>()
            {
                Data = new UnitTypeAggrDTO()
                {
                    SubjectTypes = _unitTypeCache.GetAllFromCache()
                },
                ResponseStatusType = ResponseStatusType.Succeed
            };
        }
    }
}