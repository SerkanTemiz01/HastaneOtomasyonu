
using Hastane.Business.Models.DTOs;
using Hastane.Business.Services.AdminService;
using Hastane.Entities.Concrete;
using HastaneOtomasyonu.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HastaneOtomasyonu.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        
        public AdminController(IAdminService adminService)
        {
           _adminService= adminService;
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
            
            if (ModelState.IsValid)
            {
               await _adminService.AddManager(addManagerDTO);
                return RedirectToAction(nameof(ListOfManagers));
            }
            return View(addManagerDTO);
        }

        public async Task<IActionResult> ListOfManagers()
        {
            var managerList = await _adminService.ListOfManager();
            return View(managerList);
        }

        //public async Task<IActionResult> UpdatedManager(Guid id)
        //{


        //    return View(updatedManager);
        //}
        //[HttpPost]
        //public async Task<IActionResult> UpdatedManager(Manager manager)
        //{
        //    await _managerRepo.Update(manager);

        //    return RedirectToAction(nameof(ListOfManagers));
        //}
        //public async Task<IActionResult> DeletedManager(Guid id)
        //{
        //    await _managerRepo.Delete(await _managerRepo.GetById(id));
        //    return RedirectToAction(nameof(ListOfManagers));
    //}
    public IActionResult AddPersonel()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> AddPersonel(AddPersonelDTO addPersonelDTO)
    {
        
        if (ModelState.IsValid)
        {
            await _adminService.AddPersonel(addPersonelDTO);
            return RedirectToAction(nameof(ListOfPersonels));
        }
        else
            return View(addPersonelDTO);
    }
    public async Task<IActionResult> ListOfPersonels()
    {
        return View(await _adminService.ListOfPersonels());
    }
        [HttpGet]
        public async Task<IActionResult> UpdatePersonel(Guid id)
        {
            return View(await _adminService.EmployeeGetByID(id));
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePersonel(Employee employee)
        {
            await _adminService.UpdateEmployee(employee);

            return RedirectToAction(nameof(ListOfPersonels));
        }
        public async Task<IActionResult> DeletePersonel(Guid id)
        {
            await _adminService.DeleteEmployee(id);

            return RedirectToAction(nameof(ListOfPersonels));
        }
        //[HttpGet]
        //public async Task<IActionResult> ShowUsAll()
        //{
        //    ShowUsAllVM showUsAllVM=new ShowUsAllVM();
        //    showUsAllVM.managers = await _managerRepo.GetAll();
        //    showUsAllVM.personels =await _personelRepo.GetAll();
        //    return View(showUsAllVM);
        //}


    }
}
