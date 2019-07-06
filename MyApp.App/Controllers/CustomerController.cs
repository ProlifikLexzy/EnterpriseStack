using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyApp.Core.Services;
using MyApp.Core.Services.Interfaces;
using MyApp.Data.Models;
using MyApp.Shared.EF.Services;

namespace MyApp.App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly ICustomerService vendorService;
        
        public CustomerController(IEnumerable<ICustomerService> collectionService)
        {
            collectionService.FirstOrDefault(c => c is ICustomerService customerService);
            this.customerService = collectionService.LastOrDefault() as CustomerService;
        }


        // public CustomerController(Consumer consumer)
        // {
        //     this.vendorService = consumer.UseVendorService();
        //     this.customerService = consumer.UseCustomerService();
        // }

        // public CustomerController(ICustomerService customerService, ICustomerService vendorService)
        // {
        //     this.customerService = customerService;
        //     this.vendorService = vendorService;
        // }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(Guid id)
        {
            this.customerService.GetCustomerById(id);
            return "value";
        }

    }
}
