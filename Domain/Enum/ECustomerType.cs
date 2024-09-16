using System.ComponentModel;

namespace Domain.Enum
{
    public enum ECustomerType
    {
        [Description("Pessoa Física")]
        NATURAL = 1,
        [Description("Pessoa Jurídica")]
        LEGAL = 2,
    }
}
