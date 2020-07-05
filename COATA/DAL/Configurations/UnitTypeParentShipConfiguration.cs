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
            builder.HasOne(x => x.UnitType)
                .WithMany()
                .HasForeignKey(x => x.UnitTypeId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.ParentUnitType)
                .WithMany()
                .HasForeignKey(x => x.ParentUnitTypeId)
                .OnDelete(DeleteBehavior.NoAction);;
        }
    }
}