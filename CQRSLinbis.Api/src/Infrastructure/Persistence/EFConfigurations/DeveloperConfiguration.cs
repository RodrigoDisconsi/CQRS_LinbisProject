using CQRSLinbis.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CQRSLinbis.Infrastructure.Persistence.EFConfigurations
{
    public class DeveloperConfiguration : IEntityTypeConfiguration<Developer>
    {
        public void Configure(EntityTypeBuilder<Developer> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK_Developer_Id");

            builder.Property(t => t.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.AddedDate)
                .HasColumnType("datetime");

            builder.HasOne(c => c.Project)
                .WithMany(t => t.Developers)
                .HasForeignKey(c => c.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}