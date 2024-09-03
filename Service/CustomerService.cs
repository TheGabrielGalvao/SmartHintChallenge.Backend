using AutoMapper;
using Domain.Interface;

namespace Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _repository = customerRepository;
            _mapper = mapper;
        }
    }
}
