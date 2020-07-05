using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Abstract.IRepository;
using DAL.Entities.Tables;
using DAL.Impl.ImplRepository.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Impl.ImplRepository
{
    public class UnitTypeParentShipRepository : GenericKeyRepository<int, UnitTypeParentShip>,
        IUnitTypeParentShipRepository
    {
        public UnitTypeParentShipRepository(CoataDbContext context) : base(context)
        {
        }

        public Dictionary<string, List<string>> GetUnitTypesGroupedByParent()
        {
            return Context.UnitTypeHierarchy
                .Include(x => x.UnitType)
                .Include(x => x.ParentUnitType)
                .AsEnumerable()
                .GroupBy(x => x.ParentUnitType.Name)
                .ToDictionary(x => x.Key,
                    x => x.Select(z => z.UnitType.Name).ToList());
        }
    }
}