using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO.Classification;
using BLL.DTO.Result;
using BLL.DTO.Unit;

namespace BLL.Abstract.UnitTree
{
    public interface IUnitSelectionService
    {
        Task<DataResult<UnitSelectionDTO>> GetUnitsByParentId(int? unitId);
        Task<DataResult<UnitSelectionDTO>> SearchByTypeAndName(string name, string unitType);
    }
}