using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestAPT.Models;

namespace TestAPT.Configurations
{
    public class FileUploadedConfiguration : IEntityTypeConfiguration<FileUploaded>
    {
        public void Configure(EntityTypeBuilder<FileUploaded> builder)
        {
            builder.HasKey(fu => fu.Id);
            //builder.Property(f => f.Name)
            //    .HasMaxLength(255);
            builder.Property(t => t.TotalAmount)
                .HasColumnType("decimal(18,2)");
            builder.Property(t => t.TimeStamp)
                .HasComputedColumnSql("GetUtcDate()")
                .IsConcurrencyToken();
            //Relationships with my 
            builder.HasMany(fu => fu.FileDetails)
                .WithOne(fd => fd.File)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
