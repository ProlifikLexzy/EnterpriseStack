using System;
using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Data.Models;
using MyApp.Shared.Dapper.Interfaces;
using MyApp.Shared.Dapper.Repository;
using MyApp.Shared.EF.Services;

namespace MyApp.App
{
    public partial class Startup
    {
        public void ConfigureDIService(IServiceCollection services)
        {
            services.AddTransient<Service<Customer>, Service<Customer>>();
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IDapperRepository<>), typeof(DapperRepository<>));
        }
    }
}
