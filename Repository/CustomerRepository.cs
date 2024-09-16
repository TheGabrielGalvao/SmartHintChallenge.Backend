using Database;
using Domain.Entities;
using Domain.Interface;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerEntity> AddAsync(CustomerEntity customer)
        {
            
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task DeleteAsync(Guid uuid)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Uuid == uuid);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CustomerEntity>> GetAllAsync()
        {
            var customers = await _context.Customers.ToListAsync();
            return customers;
        }

        public async Task<CustomerEntity> GetByUuidAsync(Guid uuid)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Uuid == uuid);
            return customer;
        }

        public async Task<CustomerEntity> UpdateAsync(Guid uuid, CustomerEntity customer)
        {
            try
            {
                var trackedEntity = _context.ChangeTracker.Entries<CustomerEntity>()
                    .FirstOrDefault(e => e.Entity.Id == customer.Id);

                if (trackedEntity != null)
                {
                    _context.Entry(trackedEntity.Entity).State = EntityState.Detached;
                }

                _context.Entry(customer).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return customer;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<bool> EmailExistsAsync(string email, Guid? uuid)
        {
            return await _context.Customers.AnyAsync(c => c.Email == email && !string.IsNullOrEmpty(uuid.ToString()) && uuid != c.Uuid);
        }

        public async Task<bool> CpfCnpjExistsAsync(string cpfCnpj, Guid? uuid)
        {
            return await _context.Customers.AnyAsync(c => c.CpfCnpj == cpfCnpj && !string.IsNullOrEmpty(uuid.ToString()) && uuid != c.Uuid);
        }
    }
}
