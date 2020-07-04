using DAL.Abstract.IRepository;
using DAL.Entities.Tables;
using DAL.Impl.ImplRepository.Base;

namespace DAL.Impl.ImplRepository
{
    public class UnitTreeRepository: GenericKeyRepository<int, UnitTree>, IUnitTreeRepository
    {
        public UnitTreeRepository(CoataDbContext context) : base(context)
        {
        }
    }
}
