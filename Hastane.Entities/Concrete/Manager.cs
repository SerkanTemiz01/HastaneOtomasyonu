using Hastane.Core.Entities.Abstract;
using Hastane.Core.Enums;
using Hastane.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hastane.Entities.Concrete
{
    public class Manager:IUser,IEmployee,IBaseEntity
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public Status Status { get; set; }
        public string? IdentityNumber { get; set; }
        private decimal _Salary;
        public decimal Salary
        {
            get => _Salary; set
            {
                if (value < 8500)
                    _Salary = 8500;
                else
                    _Salary = value;
            }
        }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public List<Personel> personels { get; set; }
        public Manager()
        {
            personels= new List<Personel>();    
        }
    }
}
