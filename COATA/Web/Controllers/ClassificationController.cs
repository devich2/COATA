using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Abstract.Classification;
using BLL.DTO.Classification;
using BLL.DTO.Result;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/classification/")]
    [ApiController]
    public class ClassificationController : ControllerBase
    {
        private readonly IClassificationService _classificationService;

        public ClassificationController(IClassificationService classificationService)
        {
            _classificationService = classificationService;
        }
        [HttpGet]
        [Route("{unitId?}")]
        public async Task<DataResult<List<ClassificationDTO>>> GetClassificationsForChildren(int? unitId)
        {
            return await _classificationService.GetClassificationByParentId(unitId);
        }
    }
}