using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Abstract.UnitTree;
using BLL.DTO.Response;
using BLL.DTO.Result;
using BLL.DTO.Unit;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/unit/")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitEditService _unitEditService;
        private readonly IUnitSelectionService _unitSelectionService;

        public UnitController(IUnitEditService unitEditService,
            IUnitSelectionService unitSelectionService)
        {
            _unitEditService = unitEditService;
            _unitSelectionService = unitSelectionService;
        }

        [HttpGet]
        [Route("expand")]
        public async Task<DataResult<List<UnitPlainDTO>>> GetByParentId(int? unitId, int? classificationId)
        {
            return await _unitSelectionService.GetUnitsByParentId(unitId, classificationId);
        }
        [HttpGet]
        [Route("expand_grouped")]
        public async Task<DataResult<UnitSelectionDTO>> GetGroupedByClassificationAndParentId(int? unitId, int? classificationId)
        {
            return await _unitSelectionService.GetUnitsGroupedByClassificationByParentId(unitId, classificationId);
        }
        [HttpGet]
        [Route("search")]
        public async Task<DataResult<UnitSelectionDTO>> SearchWithExpandedClassifications(string name, string unitType)
        {
            return await _unitSelectionService.SearchWithExpandedClassifications(name, unitType);
        }
        [HttpGet]
        [Route("search_expanded")]
        public async Task<DataResult<UnitSelectionDTO>> SearchWithExpandedParents(string name, string unitType)
        {
            return await _unitSelectionService.SearchByTypeAndNameWithParents(name, unitType);
        }
        [HttpPost]
        public async Task<DataResult<UnitAddResponse>> Create([FromBody] UnitCreateOrUpdateDTO model)
        {
            return await _unitEditService.ProcessUnitCreate(model);
        }
        
        [HttpPut]
        [Route("{unitId}")]
        public async Task<DataResult<UnitUpdateResponse>> Update([FromBody] UnitCreateOrUpdateDTO model, int unitId)
        {
            model.Id = unitId;
            return await _unitEditService.ProcessUnitUpdate(model);
        }
        [HttpDelete]
        [Route("{unitId}")]
        public async Task<Result> Delete(int unitId)
        {
            return await _unitEditService.ProcessUnitDelete(unitId);
        }
    }
}