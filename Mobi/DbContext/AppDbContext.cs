using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mobi.Entities;

namespace Mobi.DbContext
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CustomProperty> CustomProperties { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserFavorite> UserFavorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(entity => entity.Id);
            });

            modelBuilder.Entity<Product>(entity =>
            {

                entity.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CustomProperty>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.HasOne(cp => cp.Product)
                .WithMany(p => p.CustomProperties)
                .HasForeignKey(cp=>cp.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<UserFavorite>(entity =>
            {
                entity.HasKey(uf => new { uf.UserId, uf.ProductId, uf.CustomPropertyId});

                entity.HasOne(uf => uf.User)
                .WithMany(u => u.UserFavorites)
                .HasForeignKey(uf => uf.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(uf => uf.Product)
                .WithMany(p => p.UserFavorites)
                .HasForeignKey(uf => uf.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(uf => uf.CustomProperty)
                .WithOne(cp => cp.UserFavorite)
                .HasForeignKey<UserFavorite>(uf => uf.CustomPropertyId)
                .OnDelete(DeleteBehavior.Restrict);
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
