using DAL.Abstract.IRepository;
using DAL.Entities.Tables;
using DAL.Impl.ImplRepository.Base;

namespace DAL.Impl.ImplRepository
{
    public class UnitTypeParentShipRepository: GenericKeyRepository<int, UnitTypeParentShip>, IUnitTypeParentShipRepository
    {
        public UnitTypeParentShipRepository(CoataDbContext context) : base(context)
        {
        }
    }
}