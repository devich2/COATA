using DAL.Abstract.IRepository;
using DAL.Entities.Tables;
using DAL.Impl.ImplRepository.Base;

namespace DAL.Impl.ImplRepository
{
    public class UnitClassificationRepository:GenericKeyRepository<int, UnitClassification>, IUnitClassificationRepository
    {
        public UnitClassificationRepository(CoataDbContext context) : base(context)
        {
        }
    }
}