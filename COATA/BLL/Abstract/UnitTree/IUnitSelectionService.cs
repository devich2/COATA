using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO.Classification;
using BLL.DTO.Result;
using BLL.DTO.Unit;

namespace BLL.Abstract.UnitTree
{
    public interface IUnitSelectionService
    {
        Task<DataResult<UnitSelectionDTO>> GetUnitsGroupedByClassificationByParentId(int? unitId, int? classificationId);
        Task<DataResult<List<UnitPlainDTO>>> GetUnitsByParentId(int? unitId, int? classificationId);
        Task<DataResult<UnitSelectionDTO>> SearchByTypeAndNameWithParents(string name, string unitType);
        Task<DataResult<UnitSelectionDTO>> SearchWithExpandedClassifications(string name, string unitType);
    }
}