using AutoMapper;
using Domain.Entities;
using Domain.Enum;
using Domain.Interface;
using Domain.Models;

namespace Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly ISettingsRepository _settingsRepository;
private readonly IMapper _mapper;
        
        private static string MSG_EMAIL = "O e-mail já está vinculado a outro Comprador!";
        private static string MSG_CPFCNPJ = "O CPF/CNPJ já está vinculado a outro Comprador!";
        private static string MSG_STATEREGISTRATION = "O campo Inscrição Estadual é obrigatório!";
        public CustomerService(ICustomerRepository customerRepository, ISettingsRepository settingsRepository, IMapper mapper)
        {
            _repository = customerRepository;
            _settingsRepository = settingsRepository;
            _mapper = mapper;
        }

        private async Task<bool> CheckStateRegistration(CustomerInputModel customer)
        {
            var settings = await _settingsRepository.GetSettingsAsync();
            
            if(customer.Type == ECustomerType.NATURAL && settings.StateRegistrationForIndividual)
                return true;

            if(customer.Type == ECustomerType.LEGAL && !customer.Free)
                return true;

            return false;
        }

        private async Task<bool> EmailExistsAsync(string email, Guid? uuid)
        {
            return await _repository.EmailExistsAsync(email, uuid);
        }

        private async Task<bool> CpfCnpjExistsAsync(string cpfCnpj, Guid? uuid)
        {
            return await _repository.CpfCnpjExistsAsync(cpfCnpj, uuid);
        }

        public async Task<CustomerViewModel> AddAsync(CustomerInputModel customer)
        {
            if (await EmailExistsAsync(customer.Email!, null))
                throw new ArgumentException(MSG_EMAIL);
            
            if (await CpfCnpjExistsAsync(customer.CpfCnpj!, null))
                throw new ArgumentException(MSG_CPFCNPJ);
            
            if (await CheckStateRegistration(customer))
                throw new ArgumentException(MSG_STATEREGISTRATION);
            


            var customerEntity = _mapper.Map<CustomerEntity>(customer);
            var inserted = await _repository.AddAsync(customerEntity);
            return _mapper.Map<CustomerViewModel>(inserted);
        }

        public async Task DeleteAsync(Guid uuid)
        {
            var existingCustomer = await _repository.GetByUuidAsync(uuid);

            if (existingCustomer != null) 
            {
                await _repository.DeleteAsync(uuid);
            }
        }

        public async Task<List<CustomerViewModel>> GetAllAsync()
        {
            var response = await _repository.GetAllAsync();

            return _mapper.Map<List<CustomerViewModel>>(response);
        }

        public async Task<CustomerViewModel> GetByUuidAsync(Guid uuid)
        {
            var response = await _repository.GetByUuidAsync(uuid);

            return _mapper.Map<CustomerViewModel>(response);
        }

        public async Task<CustomerViewModel> UpdateAsync(Guid uuid, CustomerInputModel customer)
        {
            if (await EmailExistsAsync(customer.Email!, customer.Uuid))
                throw new ArgumentException(MSG_EMAIL);
        
            if (await CpfCnpjExistsAsync(customer.CpfCnpj!, customer.Uuid))
                throw new ArgumentException(MSG_CPFCNPJ);
            
            if (await CheckStateRegistration(customer))
                throw new ArgumentException(MSG_STATEREGISTRATION);


            var existingCustomer = await _repository.GetByUuidAsync(customer.Uuid!.Value);

            if (existingCustomer != null)
            {
                var customerNormalized = _mapper.Map<CustomerEntity>(customer);
                customerNormalized.Id = existingCustomer.Id;    
                var response = await _repository.UpdateAsync(uuid, customerNormalized);
                return _mapper.Map<CustomerViewModel>(response);
            }

            return _mapper.Map<CustomerViewModel>(customer);
            
        }
    }
}
