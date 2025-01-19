using Group3BitirmeProjesi.DAL.Entities.Concrete;
using Microsoft.AspNetCore.Identity;

namespace Group3BitirmeProjesi.SeedDatas
{
    public static class AdminSeed
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            try
            {
                // Admin rolü mevcut mu?
                var roleExist = await roleManager.RoleExistsAsync("Admin");
                if (!roleExist)
                {
                    var roleResult = await roleManager.CreateAsync(new IdentityRole("Admin"));
                    if (!roleResult.Succeeded)
                    {
                        Console.WriteLine("Role creation failed. Errors:");
                        foreach (var error in roleResult.Errors)
                        {
                            Console.WriteLine($"- {error.Description}");
                        }
                        return;
                    }
                }

                // Kullanıcı mevcut mu?
                var userExist = await userManager.FindByEmailAsync("admin@admin.com");
                if (userExist == null)
                {
                    var user = new AppUser
                    {
                        UserName = "admin@admin.com",
                        Email = "admin@admin.com",
                                  
                        NormalizedEmail = "admin@admin.com".ToUpperInvariant(),
                        NormalizedUserName = "admin@admin.com".ToUpperInvariant(),
                        FirstName = "Admin",
                        LastName = "Yönetici",
                        BirthDate = new DateTime(1983, 09, 20),
                        EmailConfirmed = false // Opsiyonel: Eğer doğrulama yapıyorsanız, değiştirin.

                    };

                    // Kullanıcı oluştur
                    var createUserResult = await userManager.CreateAsync(user, "Admin123!");
                    if (!createUserResult.Succeeded)
                    {
                        Console.WriteLine("User creation failed. Errors:");
                        foreach (var error in createUserResult.Errors)
                        {
                            Console.WriteLine($"- {error.Description}");
                        }
                        return;
                    }

                    // Kullanıcıyı role ekle
                    var addToRoleResult = await userManager.AddToRoleAsync(user, "Admin");
                    if (!addToRoleResult.Succeeded)
                    {
                        Console.WriteLine("Adding user to role failed. Errors:");
                        foreach (var error in addToRoleResult.Errors)
                        {
                            Console.WriteLine($"- {error.Description}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Admin user already exists.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred during initialization:");
                Console.WriteLine(ex.Message);

                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception:");
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
        }

    }

}


