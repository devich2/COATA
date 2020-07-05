using DAL.Configurations;
using DAL.Entities.Tables;
using DAL.Seed;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class CoataDbContext : DbContext
    {
        public CoataDbContext(DbContextOptions<CoataDbContext> dbContextOptions):base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.ApplyConfiguration(new UnitTypeConfiguration());
            builder.ApplyConfiguration(new UnitClassificationConfiguration());
            builder.ApplyConfiguration(new UnitTypeParentShipConfiguration());
            builder.ApplyConfiguration(new UnitTreeConfiguration());
            
            DatabaseInitializer.SeedDatabase(builder);
        }

        public DbSet<UnitTree> Units { get; set; }
        public DbSet<UnitType> UnitTypes { get; set; }
        public DbSet<UnitTypeParentShip> UnitTypeHierarchy { get; set; }
        public DbSet<UnitClassification> UnitClassifications {get; set;}
    }
}