using Hastane.Core.Enums;

namespace HastaneOtomasyonu.Models.VMs
{
    public class ListOfManagersVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public Status Status { get; set; }
        public decimal Salary { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
