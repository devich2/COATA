using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Abstract.UnitTree;
using BLL.DTO.Result;
using DAL;
using DAL.Abstract;
using DAL.Entities.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/test/")]
    public class TestController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CoataDbContext _coataDbContext;
        private readonly IUnitSelectionService _unitSelectionService;

        public TestController(IUnitOfWork unitOfWork, CoataDbContext coataDbContext, IUnitSelectionService unitSelectionService)
        {
            _unitOfWork = unitOfWork;
            _coataDbContext = coataDbContext;
            _unitSelectionService = unitSelectionService;
        }
        [HttpGet]
        public async Task<List<UnitTree>> GetUnitTypes()
        {
            return await _unitOfWork.Units.GetAllAsync();
        }
        
        [HttpGet]
        [Route("search")]
        public async Task<List<UnitTree>> Search()
        {
            return await _unitOfWork.Units.GetSinglePathByTypeAndName("Ва", "Селища");
        }
        [HttpGet]
        [Route("search_my")]
        public async Task<DataResult<List<UnitTree>>> SearchMy()
        {
            return await _unitSelectionService.SearchByTypeAndName("ВІННИЦЬКА ОБЛАСТЬ/М.ВІННИЦЯ", null);
        }
        [HttpPost]
        public async Task Create()
        {
            var s = await _unitOfWork.Units.AddAsync(new UnitTree()
            {
                ParentId = 2,
                UnitClassificationId = 2,
                Name = "NEw rayon"
            });
            await _unitOfWork.SaveAsync();
            await _coataDbContext.Database.ExecuteSqlRawAsync("Exec UpdateBowers @p0, @p1", 2, s.Id);
            var b = 3;
        }

        [HttpDelete]
        public async Task DeleteNode()
        {
            var s = await _unitOfWork.Units.FirstOrDefaultAsync(x=>x.Name=="NEw rayon");
            await _coataDbContext.Database.ExecuteSqlRawAsync("Exec DeleteNode @p0", s.Id);
            var b = 3;
        }
    }
}