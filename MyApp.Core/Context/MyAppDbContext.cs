using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using MyApp.Shared;
using MyApp.Shared.EF;
using MyApp.Shared.Models;
using MyApp.Shared.Models.Map;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Shared.Context
{
    public class MyAppDbContext : SharedDbContext
    {
        private IDbContextTransaction _transaction;
        public MyAppDbContext(DbContextOptions options) : base(options)
        {
        }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
