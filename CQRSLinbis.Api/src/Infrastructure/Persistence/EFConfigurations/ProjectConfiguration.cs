using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CRUDCleanArchitecture.Domain.Entities;

namespace CRUDCleanArchitecture.Infrastructure.Persistence.EFConfigurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK_Project_Id");

            builder.Property(t => t.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.AddedDate)
                .HasColumnType("datetime");
        }
    }

}