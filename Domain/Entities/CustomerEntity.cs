using Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Customers")]
    public class CustomerEntity
    {
        public long Id { get; set; }
        public Guid Uuid { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; } // Validar se existe
        public string CpfCnpj { get; set; }
        public string? Phone { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ECustomerType Type { get; set; }
        public string StateRegistration { get; set; }
        public bool Free { get; set; }
        public DateTime? Birthday { get; set; }
        public EGender? Gender { get; set; }
        public string? Password { get; set; }
        public ECustomerStatus Status { get; set; } = ECustomerStatus.ACTIVE;
    }
}
