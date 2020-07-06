using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO.Result;
using BLL.DTO.UnitType;

namespace BLL.Abstract.UnitType
{
    public interface IUnitTypeService
    {
        DataResult<UnitTypeAggrDTO> GetTypesGroupedByParents();
    }
}