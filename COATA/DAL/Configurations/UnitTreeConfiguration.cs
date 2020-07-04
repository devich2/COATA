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

            builder.Property(x => x.Lft).HasColumnName("Lft");
            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.Rgt).HasColumnName("Rgt");
            builder.Property(x => x.ParentId).HasColumnName("ParentId");
            builder.Property(x => x.UnitClassificationId).HasColumnName("UnitClassificationId");
            builder.HasOne(x => x.Parent).WithOne().HasForeignKey<UnitTree>(x => x.ParentId);
            builder.HasOne(x => x.UnitClassification).WithOne().HasForeignKey<UnitTree>(x => x.UnitClassificationId);
        }
    }
}