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
		SecurePolicy = CookieSecurePolicy.Always, // Http istekleri tarafýndan eriþilebilir yaptýk
		HttpOnly = true, // Client-Side tarafýndan cookie'nin eriþilebilir olmasýný saðlýyoruz. DÝKKAT !!!! : Sen kendin yayýnlayacaðýn bir site yazarken Bunu False olarak iþaretliyorsun. Kötü kiþiler client side tarafýndaki bütün olaylara hakim olabildiði için senin sistemini buradan patlatabilir.
	};
	x.ExpireTimeSpan = TimeSpan.FromMinutes(1);
	x.SlidingExpiration = true;//Ýstak gelirse cookie'nin süresinin uzatýlýcaðýný söyledik.
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
