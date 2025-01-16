using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Group3BitirmeProjesi.DAL.Entities.Concrete
{
    public class AppUser : IdentityUser<Guid> //admin kullanıcılarının yönetimi için 
    {
  
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime BirthDate { get; set; }

        // İstenirse kullanıcıya özel başka özellikler eklenebilir.

    }
    
    
}
