using System;
using MyApp.Data.Models;

namespace MyApp.Core.Services.Interfaces
{
    public interface ICustomerService
    {
         Customer GetCustomerById(Guid id);
    }
}
