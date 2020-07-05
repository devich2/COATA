using DAL.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class UnitClassificationConfiguration : IEntityTypeConfiguration<UnitClassification>
    {
        public void Configure(EntityTypeBuilder<UnitClassification> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}