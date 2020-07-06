using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Schema;
using DAL.Abstract.IRepository;
using DAL.Entities.Tables;
using DAL.Impl.ImplRepository.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Impl.ImplRepository
{
    public class UnitTreeRepository : GenericKeyRepository<int, UnitTree>, IUnitTreeRepository
    {
        public UnitTreeRepository(CoataDbContext context) : base(context)
        {
        }

        public async Task<List<UnitTree>> GetSinglePathByTypeAndName(string name, string type)
        {
            IQueryable<UnitTree> query =
                (from tofind in Context.Units
                    from upline in Context.Units.Include(x => x.UnitClassification).ThenInclude(x => x.UnitType)
                        .Where(x => x.Lft <= tofind.Lft && x.Rgt >= tofind.Rgt)
                    where (name == null || tofind.Name.ToLower().Contains(name.ToLower())) &&
                          (type == null || tofind.UnitClassification.UnitType.Name == type)
                    orderby upline.Lft
                    select upline).AsNoTracking();
            return await query.ToListAsync();
        }

        public async Task<List<UnitTree>> GetNodesByParentIds(List<int?> parentIds)
        {
            IQueryable<UnitTree> query =
                (from tofind in Context.Units
                    where parentIds.Contains(tofind.ParentId)
                    select tofind).Include(x => x.UnitClassification).ThenInclude(x => x.UnitType).AsNoTracking()
                .Distinct();
            return await query.ToListAsync();
        }

        public async Task<List<UnitTree>> GetByParentId(int? parentId)
        {
            return
                await Context.Units.Include(x => x.UnitClassification).ThenInclude(x => x.UnitType)
                    .Where(x => x.ParentId == parentId).ToListAsync();
        }

        public async Task UpdateBowers(int unitId, int parentId)
        {
            await Context.Database.ExecuteSqlRawAsync("Exec UpdateBowers @p0, @p1", parentId, unitId);
        }

        public async Task DeleteSubTree(int unitId)
        {
            await Context.Database.ExecuteSqlRawAsync("Exec DeleteNode @p0", unitId);
        }

        public Task<UnitTree> GetByIdExpandedAsync(int id)
        {
            return Context.Units.Include(x => x.UnitClassification).ThenInclude(x => x.UnitType)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}