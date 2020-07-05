using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Abstract.IRepository.Base;
using DAL.Entities.Tables;

namespace DAL.Abstract.IRepository
{
    public interface IUnitTypeParentShipRepository: IGenericKeyRepository<int, UnitTypeParentShip>
    {
        Dictionary<string, List<string>> GetUnitTypesGroupedByParent();
    }
}