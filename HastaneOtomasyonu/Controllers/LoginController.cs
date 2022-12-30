using Hastane.DataAccess.Abstract;
using Hastane.Entities.Concrete;
using HastaneOtomasyonu.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace HastaneOtomasyonu.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAdminRepo _adminRepo;
        private readonly IManagerRepo _managerRepo;
        private readonly IPersonelRepo _personelRepo;
        public LoginController(IAdminRepo adminRepo, IManagerRepo managerRepo, IPersonelRepo personelRepo)
        {
            _adminRepo = adminRepo;
            _managerRepo = managerRepo;
            _personelRepo = personelRepo;
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
        public IActionResult Login(string emailAddress, string password)
        {

            //Admin,Manager,Personnel aslında tek bir tablodan yönetilir ve bu tablodan sorgu yapılır.Anlık çözüm üretmek için böyle bir davranış sergiledim.

            //BaseRepo'da yazabilirdim bu GetByEmail metotlarını yazmamamın nedeni ise BaseRepo'daki T kısıtlamasında emailAdress ve Password bilgilerinin bulunmamasından kaynaklanmaktadır!!


            var adminUser = _adminRepo.GetByEmail(emailAddress, password);
            var managerUser = _managerRepo.GetByEmail(emailAddress, password);
            var personelUser = _personelRepo.GetByEmail(emailAddress, password);

            if (adminUser != null) { return RedirectToAction("Index", "Admin"); }
            if (managerUser != null) { return RedirectToAction("Index", "Manager"); }
            if (personelUser != null) { return RedirectToAction("Index", "Personel"); }
            return View();


        }
    }
}
