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
             services.AddScoped<IDbContext, MyAppDbContext>();
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IRepository<>), typeof(EntityRepository<>));
        }
    }
}
