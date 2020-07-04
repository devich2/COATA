using DAL.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class UnitTypeParentShipConfiguration : IEntityTypeConfiguration<UnitTypeParentShip>
    {
        public void Configure(EntityTypeBuilder<UnitTypeParentShip> builder)
        {
            builder.HasKey(x => new {x.UnitTypeId, x.ParentUnitTypeId});
            builder.Property(x => x.UnitType).HasColumnName("UnitType");
            builder.Property(x => x.ParentUnitTypeId).HasColumnName("ParentUnitTypeId");
            builder.HasOne(x => x.UnitType)
                .WithOne()
                .HasForeignKey<UnitTypeParentShip>(x => x.UnitTypeId);
            builder.HasOne(x => x.ParentUnitType)
                .WithOne()
                .HasForeignKey<UnitTypeParentShip>(x => x.ParentUnitTypeId);
        }
    }
}