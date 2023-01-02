using Hastane.Business.Models.DTOs;
using Hastane.Business.Models.VMs;
using Hastane.Core.Enums;
using Hastane.DataAccess.Abstract;
using Hastane.Entities.Abstract;
using Hastane.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hastane.Business.Services.AdminService
{
	public class AdminService : IAdminService
	{
		private readonly IEmployeeRepo _employeeRepo;
		public AdminService(IEmployeeRepo employeeRepo)
		{
			_employeeRepo = employeeRepo;
		}

		public async Task AddManager(AddManagerDTO addManagerDTO)
		{
			Employee employee= new Employee();
			employee.ID= addManagerDTO.ID;
			employee.Name= addManagerDTO.Name;
			employee.Salary= addManagerDTO.Salary;
			employee.Surname= addManagerDTO.Surname;
			employee.CreatedDate= addManagerDTO.CreatedDate;
			employee.EmailAddress= addManagerDTO.EmailAddress;
			employee.Status= addManagerDTO.Status;
			employee.Password = GivePassword();
			employee.Roles= addManagerDTO.Roles;	
			await _employeeRepo.Add(employee);	
		}

		public async Task<List<ListOfManagersVM>> ListOfManager()
		{
			var managers = await _employeeRepo.GetFilteredList(select: x => new ListOfManagersVM
			{
				ID=x.ID,
				Name = x.Name,
				Surname = x.Surname,
				EmailAddress = x.EmailAddress,
				Salary = x.Salary,
				Roles=x.Roles
			}, where: x => (x.Status == Status.Active || x.Status == Status.Modified)&&x.Roles==Roles.Manager, orderBy: x => x.OrderBy(x => x.Name));
			return managers;
		}
		
		public async Task AddPersonel(AddPersonelDTO addPersonelDTO)
		{
			Employee employee = new Employee();
			employee.ID= addPersonelDTO.ID;
			employee.Name = addPersonelDTO.Name;
			employee.Surname = addPersonelDTO.Surname;
			employee.Salary = addPersonelDTO.Salary;
			employee.Status = addPersonelDTO.Status;
			employee.CreatedDate = addPersonelDTO.CreatedDate;
			employee.EmailAddress = addPersonelDTO.EmailAddress;
			employee.Roles= addPersonelDTO.Roles;
			employee.Password = GivePassword();
			await _employeeRepo.Add(employee);
		}

        public async Task<List<ListOfPersonelVM>> ListOfPersonels()
        {
            var personels = await _employeeRepo.GetFilteredList(select: x => new ListOfPersonelVM
            {
                ID = x.ID,
                Name = x.Name,
                Surname = x.Surname,
                EmailAddress = x.EmailAddress,
                Salary = x.Salary,
                Roles = x.Roles
            }, where: x => (x.Status == Status.Active || x.Status == Status.Modified) && x.Roles == Roles.Personel, orderBy: x => x.OrderBy(x => x.Name));
            return personels;
        }

		public async Task<Employee> EmployeeGetByID(Guid id)
		{
			return await _employeeRepo.GetById(id);
			
		}
		public async Task UpdateEmployee(Employee employee)
		{
			await _employeeRepo.Update(employee);
		}
		public async Task DeleteEmployee(Guid id)
		{
			await _employeeRepo.Delete(await _employeeRepo.GetById(id));
		}

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
