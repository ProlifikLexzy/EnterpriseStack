using MyApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Data.Models
{
    public class Customer: BaseEntity
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
