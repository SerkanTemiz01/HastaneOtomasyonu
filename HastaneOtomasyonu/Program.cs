using Hastane.Business.Services.AdminService;
using Hastane.DataAccess.Abstract;
using Hastane.DataAccess.EntityFramework.Concrete;
using Hastane.DataAccess.EntityFramework.Context;
using HastaneOtomasyonu.Controllers;
using HastaneOtomasyonu.Models.SeedDataFolder;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add Authuntication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
	x.LoginPath = "/Login/Login";
	x.AccessDeniedPath = "/Home/Error";
	x.Cookie = new CookieBuilder
	{
		Name = "NRM1Cookie",
		SecurePolicy = CookieSecurePolicy.Always, // Http istekleri taraf�ndan eri�ilebilir yapt�k
		HttpOnly = true, // Client-Side taraf�ndan cookie'nin eri�ilebilir olmas�n� sa�l�yoruz. D�KKAT !!!! : Sen kendin yay�nlayaca��n bir site yazarken Bunu False olarak i�aretliyorsun. K�t� ki�iler client side taraf�ndaki b�t�n olaylara hakim olabildi�i i�in senin sistemini buradan patlatabilir.
	};
	x.ExpireTimeSpan = TimeSpan.FromMinutes(1);
	x.SlidingExpiration = true;//�stak gelirse cookie'nin s�resinin uzat�l�ca��n� s�yledik.
	x.Cookie.MaxAge = x.ExpireTimeSpan;
});
builder.Services.AddDbContext<HastaneDbContext>(_ =>
{
    _.UseSqlServer(builder.Configuration.GetConnectionString("ConnString"));
});

builder.Services.AddScoped<IAdminRepo, AdminRepo>();
builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
builder.Services.AddScoped<IAdminService, AdminService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
SeedData.Seed(app);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
