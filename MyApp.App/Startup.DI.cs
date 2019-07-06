using System;
using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Core.Services;
using MyApp.Core.Services.Interfaces;
using MyApp.Data.Models;
using MyApp.Shared;
using MyApp.Shared.Context;
using MyApp.Shared.EF;
using MyApp.Shared.EF.Repository;
using MyApp.Shared.EF.Services;

namespace MyApp.App
{
    public partial class Startup
    {
        public void ConfigureDIService(IServiceCollection services)
        {
            services.AddTransient<ICustomerService, VendorService>();
            services.AddTransient<ICustomerService, CustomerService>();
            // services.AddTransient<Consumer>();
            // services.AddTransient<VendorService>();
            // services.AddTransient<CustomerService>();

            // services.AddTransient<Func<string, ICustomerService>>(serviceProvider => key =>
            // {
            //     switch (key)
            //     {
            //         case "A":
            //             return serviceProvider.GetService<VendorService>();
            //         case "B":
            //             return serviceProvider.GetService<CustomerService>();
            //         case "C":
            //         default:
            //             return null;
            //     }
            // });

            services.AddScoped<IDbContext, MyAppDbContext>();
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IRepository<>), typeof(EntityRepository<>));
        }
    }

    public class Consumer
    {
        private readonly Func<string, ICustomerService> serviceAccessor;

        public Consumer(Func<string, ICustomerService> serviceAccessor)
        {
            this.serviceAccessor = serviceAccessor;
        }

        public ICustomerService UseVendorService()
        {
            //use serviceAccessor field to resolve desired type
            return serviceAccessor("A");
        }

        public ICustomerService UseCustomerService()
        {
            //use serviceAccessor field to resolve desired type
            return serviceAccessor("B");
        }
    }
}
