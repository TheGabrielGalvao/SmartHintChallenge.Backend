using Domain.Models;

namespace Domain.Interface
{
    public interface ICustomerService
    {
        Task<List<CustomerViewModel>> GetAllAsync();
        Task<CustomerViewModel> GetByUuidAsync(Guid uuid);
        Task<CustomerViewModel> AddAsync(CustomerInputModel customer);
        Task<CustomerViewModel> UpdateAsync(Guid uuid, CustomerInputModel customer);
        Task DeleteAsync(Guid uuid);
    }
}
