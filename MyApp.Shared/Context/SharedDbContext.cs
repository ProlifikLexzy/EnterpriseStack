using System;
using Microsoft.EntityFrameworkCore;
using MyApp.Shared.Models;

namespace MyApp.Shared.Context
{
    public class SharedDbContext:AuthDbContext
    {
        public SharedDbContext(DbContextOptions options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore(typeof(MyAppUser));
            modelBuilder.Ignore(typeof(MyAppRole));
            modelBuilder.Ignore(typeof(MyAppUserClaim));
            modelBuilder.Ignore(typeof(MyAppUserRole));
            modelBuilder.Ignore(typeof(MyAppUserLogin));
            modelBuilder.Ignore(typeof(MyAppRoleClaim));
            modelBuilder.Ignore(typeof(MyAppUserToken));
        }
    }
}
