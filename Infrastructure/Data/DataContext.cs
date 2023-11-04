using Core;
using Core.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser, Role, string, IdentityUserClaim<string>, IdentityUserRole<string>,
        IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        #region Setup

        public DbSet<Company> Companies { get; set; }
        // public DbSet<Site> Sites { get; set; }
        // public DbSet<Location> Locations { get; set; }
        // public DbSet<Category> Categories { get; set; }
        // public DbSet<Department> departments { get; set; }

        #endregion

        #region Software Licence

        // public DbSet<SoftwareLicence> SoftwareLicences { get; set; }
        // public DbSet<SoftawarePackage> Packages { get; set; }
        // public DbSet<SoftwareVersion> SoftwareVersion { get; set; }
        #endregion

        #region inventory

        // public DbSet<Location> Locations { get; set; }
        // public DbSet<Warehouse> Warehouses { get; set; }
        // public DbSet<QuantityUnit> QuantityUnits { get; set; }
        // public DbSet<Stock> Stocks { get; set; }


        #endregion

        #region AssetManagement

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Event> Events { get; set; }

        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Asset>().Property(x => x.AssetTagId).HasDefaultValueSql("NEWID()");

            // modelBuilder.Entity<Site>()
            // .HasMany(e => e.Assets)
            // .WithOne(s => s.Site)
            // .HasForeignKey(s => s.SiteId)
            // .OnDelete(DeleteBehavior.SetNull);

            // modelBuilder.Entity<Site>()
            // .HasMany(s => s.Assets)
            // .WithOne(u => u.Site)
            // .HasForeignKey(u => u.SiteId)
            // .IsRequired(false)
            // .OnDelete(DeleteBehavior.Restrict);

            // modelBuilder.Entity<Site>()
            // .HasMany(e => e.Locations)
            // .WithOne(s => s.Site)
            // .HasForeignKey(s => s.SiteId)
            // .IsRequired(false)
            // .OnDelete(DeleteBehavior.Restrict);


            //     modelBuilder.Entity<Asset>()       // THIS IS FIRST
            // .HasOne(u => u.Site).WithMany(u => u.Assets).IsRequired(false).OnDelete(DeleteBehavior.SetNull);

            // modelBuilder.Entity<Location>()       // THIS IS FIRST
            // .HasOne(u => u.Site).WithMany(u => u.Locations).IsRequired().OnDelete(DeleteBehavior.Restrict);


            // modelBuilder.Entity<Location>()
            //     .HasOne(pt => pt.Site)
            //     .WithMany(p => p.Locations)
            //     .HasForeignKey(pt => pt.SiteId); 

        //     modelBuilder.Entity<Asset>()
        // .HasOne(b => b.Site)
        // .WithMany(a => a.Assets)
        // .OnDelete(DeleteBehavior.SetNull);
        }
    }
}