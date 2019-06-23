using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }
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
