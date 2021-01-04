using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Writely.Models;

namespace Writely.Data
{
    public class AppDbContext : ApiAuthorizationDbContext<AppUser>
    {
        public DbSet<Journal> Journals { get; set; }
        public DbSet<Entry> Entries { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Journal>(entity =>
            {
                entity.HasKey(j => j.Id);
                
                entity.Property(j => j.Title)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(j => j.UserId)
                    .IsRequired();

                entity.HasMany(j => j.Entries)
                    .WithOne(e => e.Journal);
            });

            builder.Entity<Entry>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.Tags)
                    .HasMaxLength(255);

                entity.Property(e => e.Body)
                    .HasMaxLength(3000)
                    .IsRequired();

                entity.HasOne(e => e.Journal)
                    .WithMany(j => j.Entries);
            });
        }
    }
}