using Hastane.Business.Models.DTOs;
using Hastane.Business.Models.VMs;
using Hastane.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hastane.Business.Services.AdminService
{
	public interface IAdminService
	{
		Task AddManager(AddManagerDTO addManagerDTO);

		Task<List<ListOfManagersVM>> ListOfManager();
		Task<List<ListOfPersonelVM>> ListOfPersonels();

		Task AddPersonel(AddPersonelDTO addPersonelDTO);

		Task<Employee> EmployeeGetByID(Guid id);
		Task UpdateEmployee(Employee employee);
		Task DeleteEmployee(Guid id);
    }
}
