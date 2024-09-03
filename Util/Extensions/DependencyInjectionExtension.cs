using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Extensions
{
    public class DependencyInjectionExtension
    {
       
        public static void MapDependenceInjection(IServiceCollection services, IConfiguration configuration)
        {
            #region Repository
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            #endregion


            #region Service
            services.AddTransient<ICustomerService, CustomerService>();
            #endregion

        }


        #region Entity
        public static IMapper Configure()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {


                cfg.CreateMap<CustomerEntity, CustomerViewModel>();
                cfg.CreateMap<CustomerInputModel, CustomerEntity>();


            });

            IMapper mapper = mapperConfig.CreateMapper();
            return mapper;
        }
        #endregion

    }
}
