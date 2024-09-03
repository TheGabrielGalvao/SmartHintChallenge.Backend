using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Customers")]
    public class CustomerEntity
    {
        public long Id { get; set; }
        public Guid Uuid { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool Status { get; set; }

    }
}
