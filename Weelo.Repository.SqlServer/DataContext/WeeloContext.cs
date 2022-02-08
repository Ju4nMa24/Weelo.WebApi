using Microsoft.EntityFrameworkCore;
using System;
using Weelo.Common.Types.Owners;
using Weelo.Common.Types.Properties;

namespace Weelo.Repository.SqlServer.DataContext
{
    /// <summary>
    /// Database context.
    /// </summary>
    public class WeeloContext : DbContext
    {
        public WeeloContext(DbContextOptions<WeeloContext> options) : base(options) { }
        /// <summary>
        /// Database tables.
        /// </summary>
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<PropertyTrace> PropertyTraces { get; set; }
        /// <summary>
        /// Create the database model.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Owner settings
            modelBuilder.Entity<Owner>().HasKey(o => o.IdOwner);
            modelBuilder.Entity<Owner>().Property(o => o.Name).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Owner>().Property(o => o.IdentificationNumber).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<Owner>().Property(o => o.Address).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Owner>().Property(o => o.CreationDate).HasDefaultValue(DateTime.UtcNow).IsRequired();
            modelBuilder.Entity<Owner>().Property(o => o.Photo).HasDefaultValue("NO_CONTENT").HasMaxLength(4096).IsRequired();
            modelBuilder.Entity<Owner>().Property(o => o.Birthday).IsRequired();
            //Property Settubgs
            modelBuilder.Entity<Property>().HasKey(p => p.IdProperty);
            //Foreign Key
            modelBuilder.Entity<Property>().HasOne<Owner>().WithMany().HasForeignKey(p => p.IdOwner).IsRequired();
            modelBuilder.Entity<Property>().Property(p => p.Name).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Property>().Property(p => p.Address).HasDefaultValue(100).IsRequired();
            modelBuilder.Entity<Property>().Property(p => p.Price).IsRequired();
            modelBuilder.Entity<Property>().Property(p => p.CodeInternal).IsRequired();
            modelBuilder.Entity<Property>().Property(p => p.CreationDate).HasDefaultValue(DateTime.UtcNow).IsRequired();
            modelBuilder.Entity<Property>().Property(p => p.Year).IsRequired();
            //PropertyImage Settngs
            modelBuilder.Entity<PropertyImage>().HasKey(p => p.IdPropertyImage);
            //Foreign Key
            modelBuilder.Entity<PropertyImage>().HasOne<Property>().WithMany().HasForeignKey(p => p.IdProperty).IsRequired();
            modelBuilder.Entity<PropertyImage>().Property(p => p.File).HasDefaultValue("NO_CONTENT").HasMaxLength(4096).IsRequired();
            modelBuilder.Entity<PropertyImage>().Property(p => p.Enabled).HasDefaultValue(true).IsRequired();
            modelBuilder.Entity<PropertyImage>().Property(p => p.CreationDate).HasDefaultValue(DateTime.UtcNow).IsRequired();
            //PropertyTrace Settngs
            modelBuilder.Entity<PropertyTrace>().HasKey(p => p.IdPropertyTrace);
            //Foreign Key
            modelBuilder.Entity<PropertyTrace>().HasOne<Property>().WithMany().HasForeignKey(p => p.IdProperty);
            modelBuilder.Entity<PropertyTrace>().Property(p => p.DateSale).HasDefaultValue(DateTime.UtcNow).IsRequired();
            modelBuilder.Entity<PropertyTrace>().Property(p => p.Value).IsRequired();
            modelBuilder.Entity<PropertyTrace>().Property(p => p.Tax).HasMaxLength(15).IsRequired();
            modelBuilder.Entity<PropertyTrace>().Property(p => p.Tax).IsRequired();
            //Default Owner
            modelBuilder.Entity<Owner>().HasData(
                new Owner()
                {
                    IdOwner = Guid.NewGuid(),
                    Address = "Calle 120 No. 30-19",
                    Birthday = "07-02-2022",
                    IdentificationNumber = "123456789",
                    Name = "Weelo",
                    Photo = string.Empty,
                    CreationDate = DateTime.UtcNow
                });
        }
    }
}
