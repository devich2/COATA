using System.Threading.Tasks;
using BLL.Abstract.UnitType;
using BLL.DTO.Result;
using BLL.DTO.UnitType;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/unit_types/")]
    [ApiController]
    public class UnitTypeController : ControllerBase
    {
        private readonly IUnitTypeService _unitTypeService;

        public UnitTypeController(IUnitTypeService unitTypeService)
        {
            _unitTypeService = unitTypeService;
        }
        
        [HttpGet]
        public DataResult<UnitTypeAggrDTO> GetTypesGroupedByParents()
        {
            return _unitTypeService.GetTypesGroupedByParents();
        }
    }
}