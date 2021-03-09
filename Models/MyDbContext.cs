using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestAPT.Configurations;

namespace TestAPT.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        public DbSet<FileUploaded> FileUploads { get; set; }
        public DbSet<FileDetail> FileDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FileDetailConfiguration());
            modelBuilder.ApplyConfiguration(new FileUploadedConfiguration());
        }

        public override int SaveChanges()
        {
            var entities = (from entry in ChangeTracker.Entries()
                            where entry.State == EntityState.Modified || entry.State == EntityState.Added
                            select entry.Entity);

            var validationResults = new List<ValidationResult>();
            foreach (var entity in entities)
            {
                if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults))
                {
                    // throw new ValidationException() or do whatever you want
                    throw new ValidationException(message: "Unable to validate object" + entity.ToString());
                    //foreach (var item in validationResults)
                    //{

                    //}
                }
            }
            return base.SaveChanges();
        }
    }
}
