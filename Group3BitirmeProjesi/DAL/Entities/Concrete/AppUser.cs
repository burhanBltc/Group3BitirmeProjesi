using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Group3BitirmeProjesi.DAL.Entities.Concrete
{
    public class AppUser : IdentityUser //admin kullanıcılarının yönetimi için 
    {
  
        public string FirstName { get; set; }
        public string LastName { get; set; }

        ////public string Email { get; set; } Identity'den geleni kullanıyoruz
        ////public string PasswordHash { get; set; } Identity'den geleni kullanıyoruz
        public DateTime BirthDate { get; set; }

        // İstenirse kullanıcıya özel başka özellikler eklenebilir.

    }
    
    
}
