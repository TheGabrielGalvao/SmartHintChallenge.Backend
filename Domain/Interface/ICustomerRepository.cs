using Domain.Entities;
using Domain.Models;

namespace Domain.Interface
{
    public interface ICustomerRepository
    {
        Task<List<CustomerEntity>> GetAllAsync();
        Task<CustomerEntity> GetByUuidAsync(Guid uuid);
        Task<CustomerEntity> AddAsync(CustomerEntity customer);
        Task<CustomerEntity> UpdateAsync(Guid uuid, CustomerEntity customer);
        Task DeleteAsync(Guid uuid);
        Task<bool> CpfCnpjExistsAsync(string cpfCnpj, Guid? uuid);
        Task<bool> EmailExistsAsync(string email, Guid? uuid);
    }
}
