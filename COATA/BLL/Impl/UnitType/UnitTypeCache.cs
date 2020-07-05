using System;
using System.Collections.Generic;
using BLL.Abstract.UnitType;
using DAL.Abstract.IRepository;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Impl.UnitType
{
    public class UnitTypeCache: IUnitTypeCache
    {
        private readonly Dictionary<string, List<string>> _allowedUnitTypes;
        public UnitTypeCache(IServiceProvider sp)
        {
            using (var scope = sp.CreateScope())
            {
                var unitTypeParentShipRepository = scope.ServiceProvider.GetService<IUnitTypeParentShipRepository>();
                _allowedUnitTypes = unitTypeParentShipRepository.GetUnitTypesGroupedByParent();
            }
        }
        public List<string> GetFromCache(string parentUnit)
        {
            return _allowedUnitTypes.TryGetValue(parentUnit, out List<string> allowedSubjects) ? allowedSubjects : new List<string>();
        }
    }
}