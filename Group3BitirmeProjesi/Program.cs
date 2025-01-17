using Group3BitirmeProjesi.DAL.DbContext;
using Group3BitirmeProjesi.DAL.Entities.Concrete;
using Group3BitirmeProjesi.Repositories.Abstract;
using Group3BitirmeProjesi.Repositories.Concrete;
using Group3BitirmeProjesi.SeedDatas;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Connection string'i appsettings.json'dan aldýk
var connectionString = builder.Configuration.GetConnectionString("AppConnectionString");

builder.Services.AddDbContext<BitProjeDbContext>(options =>
    options.UseSqlServer(connectionString)); // DbContext'i yapýlandýr

//Identity yönetimi, Identity servislerini ekler
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;

    options.Password.RequireDigit = true;  // En az bir rakam olmalý
    options.Password.RequireLowercase = true; // En az bir küçük harf olmalý
    options.Password.RequireUppercase = true; // En az bir büyük harf olmalý
    options.Password.RequireNonAlphanumeric = true; // En az bir özel karakter olmalý
    options.Password.RequiredLength = 6; // En az 6 karakter olmalý

}).AddEntityFrameworkStores<BitProjeDbContext>() //DbContext'i belirledi
.AddDefaultTokenProviders(); // Token saðlayýcýlarý ekle

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));//tek tek yazmak yerine typeof

var app = builder.Build();


// Veritabaný seed iþlemi
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedData.Initialize(services, userManager, roleManager);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute
    (
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=MainPage}/{action=Index}/{id?}");

app.Run();
