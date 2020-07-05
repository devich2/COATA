using System.Collections.Generic;

namespace BLL.Abstract.UnitType
{
    public interface IUnitTypeCache
    {
        List<string> GetFromCache(string parentUnit);
    }
}