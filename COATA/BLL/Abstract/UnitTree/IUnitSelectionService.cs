using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO.Result;
using BLL.DTO.Unit;

namespace BLL.Abstract.UnitTree
{
    public interface IUnitSelectionService
    {
        Task<DataResult<List<UnitPlainDTO>>> GetUnitsByParentId(int unitId);
        Task<DataResult<List<DAL.Entities.Tables.UnitTree>>> SearchByTypeAndName(string name, string unitType);
    }
}