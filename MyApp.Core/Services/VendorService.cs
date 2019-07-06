using System;
using MyApp.Core.Services.Interfaces;
using MyApp.Data.Models;
using MyApp.Shared.EF;
using MyApp.Shared.EF.Services;

namespace MyApp.Core.Services
{
    public class VendorService:Service<Vendor>, ICustomerService
    {
        public VendorService(IUnitOfWork iuow): base(iuow)
        {
        }

        public Customer GetCustomerById(Guid id)
        {
            return this.GetById(id);
        }
    }
}
