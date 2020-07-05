using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Abstract.IRepository;
using DAL.Entities.Tables;
using DAL.Impl.ImplRepository.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Impl.ImplRepository
{
    public class UnitClassificationRepository:GenericKeyRepository<int, UnitClassification>, IUnitClassificationRepository
    {
        public UnitClassificationRepository(CoataDbContext context) : base(context)
        {
        }

        public override Task<UnitClassification> FirstOrDefaultAsync(Expression<Func<UnitClassification, bool>> predicate)
        {
            return Context.UnitClassifications.Include(x=>x.UnitType).FirstOrDefaultAsync(predicate);
        }
    }
}