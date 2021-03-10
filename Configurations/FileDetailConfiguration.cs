using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestAPT.Models;

namespace TestAPT.Configurations
{
    public class FileDetailConfiguration : IEntityTypeConfiguration<FileDetail>
    {
        public void Configure(EntityTypeBuilder<FileDetail> builder)
        {
            builder.HasKey(fd => fd.Id);
            builder.Property(f => f.Amount)
                .HasColumnType("decimal(13,2)")
                .IsRequired(true);
        }
    }
}
