using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Data.Models
{
    public class MyAppUser : IdentityUser<Guid>
    {
        public MyAppUser()
        {
            Id = Guid.NewGuid();
            CreatedOnUtc = DateTime.UtcNow;
        }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool Activated { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class MyAppUserClaim : IdentityUserClaim<Guid>
    {

    }

    public class MyAppUserLogin : IdentityUserLogin<Guid>
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }

    public class MyAppRole : IdentityRole<Guid>
    {
        public MyAppRole()
        {
            Id = Guid.NewGuid();
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
        }
    }

    public class MyAppUserRole : IdentityUserRole<Guid>
    {

    }

    public class MyAppRoleClaim : IdentityRoleClaim<Guid>
    {

    }

    public class MyAppUserToken : IdentityUserToken<Guid>
    {

    }
}
