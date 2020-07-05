using DAL.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class UnitTypeConfiguration: IEntityTypeConfiguration<UnitType>
    {
        public void Configure(EntityTypeBuilder<UnitType> builder)
        {
            builder.HasKey(x=>x.Id);
        }
    }
}