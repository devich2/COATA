using System;
using System.Threading.Tasks;
using DAL.Abstract.IRepository;

namespace DAL.Abstract
{
    public interface IUnitOfWork: IDisposable
    {
        public IUnitTreeRepository Units { get; }
        public IUnitTypeRepository UnitTypes { get; }
        public IUnitTypeParentShipRepository UnitTypeTypeParentShip {get;}
        public IUnitClassificationRepository UnitClassifications {get;}
        public Task<int> SaveAsync();
    }
}