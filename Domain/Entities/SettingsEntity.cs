using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Settings")]
    public class SettingsEntity
    {
        public long Id { get; set; }
        public bool StateRegistrationForIndividual { get; set; } = false;
    }
}
