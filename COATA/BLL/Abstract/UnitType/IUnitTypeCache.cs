using System.Collections.Generic;
using BLL.DTO.UnitType;

namespace BLL.Abstract.UnitType
{
    public interface IUnitTypeCache
    {
        Dictionary<string, List<DAL.Entities.Tables.UnitType>> GetAllFromCache();
        List<DAL.Entities.Tables.UnitType> GetFromCache(string parentUnit);
        
    }
}