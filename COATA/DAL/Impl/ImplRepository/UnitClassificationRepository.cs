using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Abstract.IRepository;
using DAL.Entities.Tables;
using DAL.Impl.ImplRepository.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Impl.ImplRepository
{
    public class UnitClassificationRepository : GenericKeyRepository<int, UnitClassification>,
        IUnitClassificationRepository
    {
        public UnitClassificationRepository(CoataDbContext context) : base(context)
        {
        }

        public override Task<UnitClassification> GetByIdAsync(int id)
        {
            return Context.UnitClassifications
                .Include(x => x.UnitType)
                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UnitClassification>> GetClassificationByParentId(int? unitId)
        {
            var list = await Context.Units.Include(x => x.UnitClassification).ThenInclude(x => x.UnitType)
                .Where(x => unitId == x.ParentId).ToListAsync();
            return list.GroupBy(x => x.UnitClassification.Id).Select(x => x.First().UnitClassification).ToList();
        }

        public override Task<UnitClassification> FirstOrDefaultAsync(
            Expression<Func<UnitClassification, bool>> predicate)
        {
            return Context.UnitClassifications.Include(x => x.UnitType).FirstOrDefaultAsync(predicate);
        }
    }
}