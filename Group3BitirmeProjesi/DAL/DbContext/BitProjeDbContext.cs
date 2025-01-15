using Group3BitirmeProjesi.DAL.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Group3BitirmeProjesi.DAL.DbContext
{
    public class BitProjeDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
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

            
            modelBuilder.Entity<AppUser>()
                .Property(u => u.PasswordHash)
                .HasMaxLength(256);

            // Combined Password validation rule (min 6, at least 1-uppercase,lowcase,digit,alphanumeric)
            modelBuilder.Entity<AppUser>()
                .Property(u => u.PasswordHash)
                .IsRequired().HasMaxLength(256)
                .HasAnnotation("MinLength", 6)
                .HasAnnotation("RegularExpression", @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z\d]).+$");

            //Email validation rule (required, unique, email format)
            modelBuilder.Entity<AppUser>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256)
                .HasAnnotation("RegularExpression", @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            modelBuilder.Entity<AppUser>()
                .HasIndex(u => u.Email)
                .IsUnique();



        }
    }
}
