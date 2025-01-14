using Group3BitirmeProjesi.DAL.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Group3BitirmeProjesi.DAL.DbContext
{
 
    public class BitProjeDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public BitProjeDbContext(DbContextOptions<BitProjeDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API for Product
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            // Fluent API for Category
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            // Password validation rule (6 character minimum, 1 uppercase, 1 lowercase, 1 number, 1 special character)
            modelBuilder.Entity<AppUser>()
                .Property(u => u.PasswordHash)
                .HasMaxLength(256);
        }
    }
}
