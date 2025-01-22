using Group3BitirmeProjesi.DAL.Entities.Abstract;
using Group3BitirmeProjesi.DAL.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Group3BitirmeProjesi.DAL.DbContext
{
    public class BitProjeDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public BitProjeDbContext(DbContextOptions<BitProjeDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //Fluent API ile ilişki, Product sınıf içindekini EF algıladığı için gerek yok fakat ilişkili veri silme yönetimi için yazdık
            modelBuilder.Entity<Product>()
               .HasOne(p => p.Category)
               .WithMany(c => c.Products)
               .HasForeignKey(p => p.CategoryId)
            //.OnDelete(DeleteBehavior.SetNull);
.OnDelete(DeleteBehavior.Restrict);  // ilişkili veri Silme davranışını özelleştirmek için eklemek gerekiyor, yazmazsan varsayılan cascade


            // AddedDate'nin sadece veritabanına kayıt yapıldığında doldurulmasını sağlamak
            modelBuilder.Entity<Product>()
                .Property(b => b.AddedDate)
                .ValueGeneratedOnAdd()  // Sadece ilk ekleme işleminde değer alınır
                .HasDefaultValueSql("GETDATE()");  //Bu, veritabanında bir satır eklendiğinde AddedDate sütununa, geçerli tarih ve saati otomatik olarak koyar. Varsayılan tarih veritabanında otomatik olarak eklenir
            modelBuilder.Entity<Category>()
            .Property(b => b.AddedDate)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("GETDATE()");


        

            // ModifiedDate'nin sadece güncelleme işlemi sırasında değişmesini sağlamak için
            modelBuilder.Entity<Product>()
                .Property(p => p.ModdifiedDate)
                .ValueGeneratedOnUpdate()  // Sadece güncellenme işlemi sırasında tarih atama
                .HasDefaultValueSql("GETDATE()");  // Veritabanı tarafında varsayılan tarih

            modelBuilder.Entity<Category>()
            .Property(p => p.ModdifiedDate)
          .ValueGeneratedOnUpdate()  // Sadece güncellenme işlemi sırasında tarih atama
            .HasDefaultValueSql("GETDATE()");  // Veritabanı tarafında varsayılan tarih



     

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(u => u.Currency)
                      .HasMaxLength(20);

                entity.Property(u => u.ImagePath)
                      .HasMaxLength(500);

                entity.Property(u => u.Name)
                 .HasMaxLength(200);

                entity.Property(u => u.Price)
                .HasColumnType("decimal(18,2)"); // decimal(18,2) ifadesi SQL Server'da  sayının 18 basamağa kadar tam sayı ve 2 basamağa kadar ondalıklı olmasına izin verir.

            });


            modelBuilder.Entity<Category>()
             .Property(u => u.Name)
                .HasMaxLength(200);


            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.Property(u => u.FirstName)
                      .HasMaxLength(30);

                entity.Property(u => u.LastName)
                      .HasMaxLength(30);
            });

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
