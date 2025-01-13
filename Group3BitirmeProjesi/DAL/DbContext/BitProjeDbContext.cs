using Group3BitirmeProjesi.DAL.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Group3BitirmeProjesi.DAL.DbContext
{
  
    public class BitProjeDbContext : IdentityDbContext<AppUser>
    {
        public BitProjeDbContext(DbContextOptions<BitProjeDbContext> options)
            : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }


}
