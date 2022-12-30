using Hastane.DataAccess.Abstract;
using Hastane.Entities.Concrete;
using HastaneOtomasyonu.Models;
using HastaneOtomasyonu.Models.DTobj;
using HastaneOtomasyonu.Models.VMs;
using Microsoft.AspNetCore.Mvc;

namespace HastaneOtomasyonu.Controllers
{
    public class AdminController : Controller
    {
        private readonly IManagerRepo _managerRepo;
        public AdminController(IManagerRepo managerRepo)
        {
            _managerRepo= managerRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddManager()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddManager(AddManagerDTO addManagerDTO)
        {
            Manager manager=new Manager();
            if (ModelState.IsValid)
            {
                manager.ID= addManagerDTO.ID;
                manager.Name= addManagerDTO.Name;
                manager.Salary= addManagerDTO.Salary;
                manager.Surname= addManagerDTO.Surname;
                manager.Status= addManagerDTO.Status;
                manager.CreatedDate= addManagerDTO.CreatedDate;
                manager.EmailAddress= addManagerDTO.EmailAddress;
                manager.Password = GivePassword();
                await _managerRepo.Add(manager);
                return RedirectToAction(nameof(ListOfManagers));
            }
            return View(addManagerDTO);
        }

        public async Task<IActionResult> ListOfManagers()
        {
            var managerList=await _managerRepo.GetAll();
            return View(managerList);
        }

        //public IActionResult UpdatedManager(Guid id)
        //{
        //    var updatedManager=HomeController._myUser.Find(i=>i.ID==id);
        //    return View(updatedManager);    
        //}
        //[HttpPost]
        //public IActionResult UpdatedManager(Manager manager)
        //{
        //    HomeController._myUser.Remove(HomeController._myUser.Find(i => i.ID == manager.ID));
        //    HomeController._myUser.Add( manager);
        //    return RedirectToAction(nameof(ListOfManagers));
        //}
        //public IActionResult DeletedManager(Guid id)
        //{
        //    HomeController._myUser.Remove(HomeController._myUser.Find(i => i.ID == id));
        //    return RedirectToAction(nameof(ListOfManagers));
        //}
        //public IActionResult AddPersonel()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult AddPersonel(Personel personel)
        //{
        //    if(ModelState.IsValid) 
        //    {
        //        HomeController._myUser.Add(personel);
        //        return RedirectToAction(nameof(ListOfPersonels));
        //    }
        //    else
        //    return View(personel);
        //}
        //public IActionResult ListOfPersonels()
        //{
        //    List<Personel> personelList = new List<Personel>();
        //    foreach (var item in HomeController._myUser)
        //    {
        //        if (item is Personel)
        //        {
        //            personelList.Add((Personel)item);
        //        }
        //    }
        //    return View(personelList);
        //}
        //public IActionResult ShowUsAll()
        //{
        //    PersonelManagerVM personelManagerVM=new PersonelManagerVM();
        //    List<Personel> myPersonels= new List<Personel>();
        //    List<Manager> myManagers=new List<Manager>();

        //    foreach(var item in HomeController._myUser)
        //    {
        //        if(item is Personel) 
        //        {
        //            myPersonels.Add((Personel)item);
        //        }
        //        if(item is Manager) 
        //        {
        //            myManagers.Add((Manager)item);
        //        }
        //    }
        //    personelManagerVM.myManagers = myManagers;
        //    personelManagerVM.myPersonels = myPersonels;
        //    return View(personelManagerVM);
        //}

        [NonAction]
        private string GivePassword()
        {
            Random rastgele = new Random();
            string sifre = String.Empty;

            for (int i = 1; i <= 6; i++)
            {
                int sayi1 = rastgele.Next(65, 91);
                //65 dahil, 91 dahil değil A ile Z arasında
                sifre += (char)sayi1;
            }
            return sifre;
        }
    }
}
