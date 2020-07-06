using System.Collections.Generic;
using DAL.Entities.Tables;

namespace DAL.Seed
{
    public class UnitTypeEntitiesHolder
    {
        private readonly List<UnitType> _unitTypes = new List<UnitType>()
        {
            new UnitType()
            {
                Id = 1,
                Name = "Області"
            },
            new UnitType()
            {
                Id = 2,
                Name = "Райони"
            },
            new UnitType()
            {
                Id = 3,
                Name = "Міста"
            },
            new UnitType()
            {
                Id = 4,
                Name = "Селища"
            },
            new UnitType()
            {
                Id = 5,
                Name = "Сільради"
            },
            new UnitType()
            {
                Id = 6,
                Name = "Села"
            }
        };
        public List<UnitType> GetUnitTypes()
        {
            return _unitTypes;
        }
    }
}