﻿using Domain.Enum;

namespace Domain.Models
{
    public class CustomerInputModel
    {
        public Guid? Uuid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CpfCnpj { get; set; }
        public string? Phone { get; set; }
        public ECustomerType Type { get; set; }
        public string? StateRegistration { get; set; }
        public bool Free { get; set; }
        public DateTime? Birthday { get; set; }
        public EGender? Gender { get; set; }
        public string? Password { get; set; }
        public ECustomerStatus Status { get; set; } = ECustomerStatus.ACTIVE;
    }
}
