using Hastane.Core.Enums;
using Hastane.DataAccess.Abstract;
using Hastane.Entities.Concrete;
using HastaneOtomasyonu.Models;
using HastaneOtomasyonu.Models.DTobj;
using HastaneOtomasyonu.Models.VMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HastaneOtomasyonu.Controllers
{
    public class AdminController : Controller
    {
        private readonly IManagerRepo _managerRepo;
        private readonly IPersonelRepo _personelRepo;
        public AdminController(IManagerRepo managerRepo, IPersonelRepo personelRepo)
        {
            _managerRepo = managerRepo;
            _personelRepo = personelRepo;
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

        public async Task<IActionResult> UpdatedManager(Guid id)
        {
            var updatedManager =await _managerRepo.GetById(id);
            
            return View(updatedManager);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatedManager(Manager manager)
        {
            await _managerRepo.Update(manager);
         
            return RedirectToAction(nameof(ListOfManagers));
        }
        public async Task<IActionResult> DeletedManager(Guid id)
        {
            await _managerRepo.Delete(await _managerRepo.GetById(id));
            return RedirectToAction(nameof(ListOfManagers));
        }
        public async Task<IActionResult> AddPersonel()
        {
            IEnumerable<Manager> managerList =(IEnumerable<Manager>)(await _managerRepo.GetAll());
            ViewBag.ManagerID = new SelectList(managerList, "ID", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddPersonel(AddPersonelDTO addPersonelDTO)
        {
            Personel personel=new Personel();
            if (ModelState.IsValid)
            {
                personel.Name= addPersonelDTO.Name;
                personel.Surname= addPersonelDTO.Surname;
                personel.ID= addPersonelDTO.ID;
                personel.Status= addPersonelDTO.Status;
                personel.CreatedDate= addPersonelDTO.CreatedDate;
                personel.EmailAddress= addPersonelDTO.EmailAddress;
                personel.Salary= addPersonelDTO.Salary;
                personel.ManagerID = addPersonelDTO.ManagerID;
                personel.Password = GivePassword();
                await _personelRepo.Add(personel);
                return RedirectToAction(nameof(ListOfPersonels));
            }
            else
                return View(addPersonelDTO);
        }
        public async Task<IActionResult> ListOfPersonels()
        {
            return View(await _personelRepo.GetAll());
        }
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
