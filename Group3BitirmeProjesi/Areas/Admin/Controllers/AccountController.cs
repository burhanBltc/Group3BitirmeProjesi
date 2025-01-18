using Group3BitirmeProjesi.Areas.Admin.Models.AccountVMs;
using Group3BitirmeProjesi.DAL.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Group3BitirmeProjesi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AccountController> _logger;


        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ILogger<AccountController> logger, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            AppUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                await Console.Out.WriteLineAsync("Kullanıcı adı veya şifre hatalı");
                return View(model);
            }
            var checkPassword = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!checkPassword.Succeeded)
            {
                await Console.Out.WriteLineAsync("Kullanıcı adı veya şifre hatalı");
                return View(model);
            }
            var userRole = await _userManager.GetRolesAsync(user);
            if (userRole == null)
            {
                await Console.Out.WriteLineAsync("Kullanıcı adı veya şifre hatalı");
                return View(model);
            }
            //Area : Admin, Customer
            return RedirectToAction("Index", "Home", new { Area = userRole[0].ToString() });    //rolü admin se admin areasına gitsin
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }




        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Şifreyi kontrol et
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Şifreler uyuşmadı, tekrar girin");
                return View(model);
            }

            // Kullanıcıyı oluştur
            AppUser user = new AppUser
            {
                UserName = model.Email, // Kullanıcı adı olarak e-posta kullanabiliriz
                Email = model.Email,
                NormalizedEmail = model.Email.ToUpperInvariant(),
                NormalizedUserName=model.Email.ToUpperInvariant(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate
            };

            // Kullanıcıyı oluştur ve şifreyi hash'le
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Rolü kontrol et ve varsa ekle
                string roleName = "Admin";
                var roleExist = await _roleManager.RoleExistsAsync(roleName); // Rol var mı kontrol et

                if (!roleExist)
                {
                    var createRoleResult = await _roleManager.CreateAsync(new IdentityRole(roleName)); // Rol oluştur
                    if (!createRoleResult.Succeeded)
                    {
                        // Rol oluşturulamazsa hataları ekle
                        foreach (var error in createRoleResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(model);
                    }
                }

                // Kullanıcıyı role ekle
                var roleResult = await _userManager.AddToRoleAsync(user, roleName);
                if (roleResult.Succeeded)
                {
                    // Hemen oturum aç
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login");
                }

                // Rol ekleme başarısız olursa hata mesajını ekle
                foreach (var error in roleResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Kullanıcı oluşturulamazsa hata mesajlarını ekle
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }




    }
}
