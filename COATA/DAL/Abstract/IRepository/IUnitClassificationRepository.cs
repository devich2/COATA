using DAL.Abstract.IRepository.Base;
using DAL.Entities.Tables;

namespace DAL.Abstract.IRepository
{
    public interface IUnitClassificationRepository: IGenericKeyRepository<int, UnitClassification>
    {
        
    }
}