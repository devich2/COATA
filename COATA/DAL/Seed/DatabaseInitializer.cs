using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Seed
{
    public static class DatabaseInitializer
    {
        public static void SeedDatabase(ModelBuilder builder)
        {
            AddEntities(builder, new UnitTypeEntitiesHolder().GetUnitTypes());
            AddEntities(builder, new UnitTypeParentShipEntitiesHolder().GetUnitParentShips());
            AddEntities(builder, new UnitClassificationEntitiesHolder().GetClassifications());
            AddEntities(builder, new UnitEntitiesHolder().GetUnitList());
        }
        private static void AddEntities<T>(ModelBuilder builder, List<T> entities) where T : class
        {
            builder.Entity<T>().HasData(entities);
        }
    }
}