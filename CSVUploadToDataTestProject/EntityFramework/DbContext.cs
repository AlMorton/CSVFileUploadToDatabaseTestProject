using CSVUploadToDataTestProject.EntityFramework.DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVUploadToDataTestProject.EntityFramework
{
    public class MyDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public DbSet<CSVData> CSVData { get; set; }

        public DbSet<Site> Sites { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options ) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           

            modelBuilder.Entity<CSVData>()
                .HasOne<Client>(p => p.Client)
                .WithMany(p => p.CSVData)
                .HasForeignKey(p => p.ClientId);

            modelBuilder.Entity<CSVData>()
                .HasOne(p => p.Site)
                .WithMany(p => p.CSVData)
                .HasForeignKey(p => p.SiteId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Site>()
                .HasOne<Client>(p => p.Client)
                .WithMany(p => p.Sites)
                .HasForeignKey(p => p.ClientId);
        }

    }
}
