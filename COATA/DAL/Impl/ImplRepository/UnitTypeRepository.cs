using DAL.Abstract.IRepository;
using DAL.Entities.Tables;
using DAL.Impl.ImplRepository.Base;

namespace DAL.Impl.ImplRepository
{
    public class UnitTypeRepository: GenericKeyRepository<int, UnitType>, IUnitTypeRepository
    {
        public UnitTypeRepository(CoataDbContext context) : base(context)
        {
        }
    }
}