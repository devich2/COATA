using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Abstract;
using DAL.Entities.Tables;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<List<UnitTree>> GetUnitTypes()
        {
            return await _unitOfWork.Units.GetAllAsync();
        }
    }
}