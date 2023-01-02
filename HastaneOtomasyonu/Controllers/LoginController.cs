using Hastane.DataAccess.Abstract;
using Hastane.Entities.Concrete;
using HastaneOtomasyonu.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Security.Claims;

namespace HastaneOtomasyonu.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAdminRepo _adminRepo;
        private readonly IEmployeeRepo _employeeRepo;
       
        public LoginController(IAdminRepo adminRepo, IEmployeeRepo employeeRepo)
        {
            _adminRepo = adminRepo;
            _employeeRepo= employeeRepo;
           
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string emailAddress, string password)
        {

            //Admin,Manager,Personnel aslında tek bir tablodan yönetilir ve bu tablodan sorgu yapılır.Anlık çözüm üretmek için böyle bir davranış sergiledim.

            //BaseRepo'da yazabilirdim bu GetByEmail metotlarını yazmamamın nedeni ise BaseRepo'daki T kısıtlamasında emailAdress ve Password bilgilerinin bulunmamasından kaynaklanmaktadır!!


            var adminUser =await _adminRepo.GetByEmail(emailAddress, password);
          
         

            var claims = new List<Claim>();

            if (adminUser != null) 
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
      

            var userIdentity=new ClaimsIdentity(claims,"Login");

            ClaimsPrincipal claimsPrincipal=new ClaimsPrincipal(userIdentity);

			HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

			if (adminUser != null) { return RedirectToAction("Index", "Admin"); }
		

			return View();


        }

        public async Task<IActionResult> Logout() 
        {
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login");
		}
    }
}
