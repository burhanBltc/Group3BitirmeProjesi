using Group3BitirmeProjesi.DAL.DbContext;
using Group3BitirmeProjesi.DAL.Entities.Concrete;
using Group3BitirmeProjesi.Repositories.Abstract;
using Group3BitirmeProjesi.Repositories.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BitProjeDbContext>(opt => opt.UseSqlServer("Server=DESKTOP-GIDU27R;Database=Bitproje1;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True; "));

//Identity yönetimi
builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;

    //options.Password.RequireDigit = true;  // En az bir rakam olmalý
    //options.Password.RequireLowercase = true; // En az bir küçük harf olmalý
    //options.Password.RequireUppercase = true; // En az bir büyük harf olmalý
    //options.Password.RequireNonAlphanumeric = true; // En az bir özel karakter olmalý
    //options.Password.RequiredLength = 6; // En az 6 karakter olmalý
    
}).AddEntityFrameworkStores<BitProjeDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));//tek tek yazmak yerine
//builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
//builder.Services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
