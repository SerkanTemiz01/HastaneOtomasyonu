using Hastane.Core.Enums;
using Hastane.DataAccess.EntityFramework.Context;
using Hastane.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace HastaneOtomasyonu.Models.SeedDataFolder
{
    public static class SeedData
    {
        public static void Seed(IApplicationBuilder app)
        {
            var scope=app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<HastaneDbContext>();
            dbContext.Database.Migrate();

            if(dbContext.Admins.Count() == 0)
            {
                dbContext.Admins.Add(new Admin()
                {
                    ID = Guid.NewGuid(),
                    Name = "Serkan",
                    Surname = "Temiz",
                    EmailAddress = "serkan@gmail.com",
                    Status = Status.Active,
                    Password = "1234",
                    CreatedDate = DateTime.Now,
                    Roles=Roles.Admin
                });
              
            }
            if(dbContext.Employees.Count() == 0) 
            {
				dbContext.Employees.Add(new Employee()
				{
					ID = Guid.NewGuid(),
					Name = "Ayşenur",
					Surname = "AltınSoy",
					EmailAddress = "Ayşenur@gmail.com",
					Status = Status.Active,
					Password = "1234",
                    IdentityNumber="2324242",
                    Salary=12500,
					CreatedDate = DateTime.Now,
					Roles = Roles.Manager
				});
			}
			
			dbContext.SaveChanges();
		}
    }
}
