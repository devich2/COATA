using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.Abstract.UnitType;
using BLL.DTO.UnitType;
using DAL.Abstract.IRepository;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Impl.UnitType
{
    public class UnitTypeCache: IUnitTypeCache
    {
        private readonly Dictionary<string, List<DAL.Entities.Tables.UnitType>> _allowedUnitTypes;
        public UnitTypeCache(IServiceProvider sp)
        {
            using (var scope = sp.CreateScope())
            {
                var unitTypeParentShipRepository = scope.ServiceProvider.GetService<IUnitTypeParentShipRepository>();
                _allowedUnitTypes = unitTypeParentShipRepository.GetUnitTypesGroupedByParent();
            }
        }
        public List<DAL.Entities.Tables.UnitType> GetFromCache(string parentUnit)
        {
            return _allowedUnitTypes.TryGetValue(parentUnit, out List<DAL.Entities.Tables.UnitType> allowedSubjects) ? allowedSubjects : new List<DAL.Entities.Tables.UnitType>();
        }
        public Dictionary<string, List<DAL.Entities.Tables.UnitType>> GetAllFromCache()
        {
            return _allowedUnitTypes;
        }
    }
}