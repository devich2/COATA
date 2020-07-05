using System.Xml.Schema;
using DAL.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class UnitTreeConfiguration : IEntityTypeConfiguration<UnitTree>
    {
        public void Configure(EntityTypeBuilder<UnitTree> builder)
        {
            builder
                .HasKey(x => x.Id);
            
            builder.HasOne(x => x.Parent).WithMany().HasForeignKey(x => x.ParentId);
            builder.HasOne(x => x.UnitClassification).WithMany().HasForeignKey(x => x.UnitClassificationId);
        }
    }
}