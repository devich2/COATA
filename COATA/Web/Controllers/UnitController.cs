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
        public async Task<DataResult<UnitSelectionDTO>> GetByParentId(int? unitId)
        {
            return await _unitSelectionService.GetUnitsByParentId(unitId);
        }
        [HttpGet]
        [Route("search")]
        public async Task<DataResult<UnitSelectionDTO>> Search(string name, string unitType)
        {
            return await _unitSelectionService.SearchByTypeAndName(name, unitType);
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