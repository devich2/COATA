using System;
using System.Threading.Tasks;
using DAL.Abstract;
using DAL.Abstract.IRepository;
using DAL.Impl.ImplRepository;

namespace DAL.Impl
{
     public class UnitOfWork: IUnitOfWork
    {
        private readonly CoataDbContext _coataDbContext;
        private bool _disposed;
        public UnitOfWork(CoataDbContext coataDbContext)
        {
            _coataDbContext = coataDbContext;
        }
        
        #region Fields

        private IUnitTreeRepository _unitTreeRepository;
        private IUnitTypeRepository _unitTypeRepository;
        private IUnitTypeParentShipRepository _unitTypeParentShipRepository;
        private IUnitClassificationRepository _unitClassificationRepository;
        #endregion

        #region Properties
        
        public IUnitTreeRepository Units
        {
            get
            {
                return _unitTreeRepository ??= new UnitTreeRepository(_coataDbContext);
            }
        }
        
        public IUnitTypeRepository UnitTypes
        {
            get
            {
                return _unitTypeRepository ??= new UnitTypeRepository(_coataDbContext);
            }
        }
        
        public IUnitTypeParentShipRepository UnitTypeTypeParentShip
        {
            get
            {
                return _unitTypeParentShipRepository ??= new UnitTypeParentShipRepository(_coataDbContext);
            }
        }
        
        public IUnitClassificationRepository UnitClassifications
        {
            get
            {
                return _unitClassificationRepository ??= new UnitClassificationRepository(_coataDbContext);
            }
        }
        #endregion

        #region Methods
        public Task<int> SaveAsync()
        {
            return _coataDbContext.SaveChangesAsync();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _coataDbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}