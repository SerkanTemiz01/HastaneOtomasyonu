﻿using Hastane.Core.Enums;

namespace HastaneOtomasyonu.Models.DTobj
{
    public class AddManagerDTO
    {
        public Guid ID { get; set; }=Guid.NewGuid();
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public decimal Salary { get; set; }
        public Status Status { get; set; } = Status.Active;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
