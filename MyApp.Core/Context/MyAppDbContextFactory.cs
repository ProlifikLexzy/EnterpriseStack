using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MyApp.Shared.Context
{

    public class MyAppDbContextFactory : IDesignTimeDbContextFactory<MyAppDbContext>
    {
        public MyAppDbContext CreateDbContext(string[] args)
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile("appsettings.Development.json", optional: true)
               .Build();

            var builder = new DbContextOptionsBuilder();
            builder.EnableSensitiveDataLogging(true);

            var connectionString = configuration["ConnectionStrings:Default"];
            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly(this.GetType().Assembly.FullName));
            return new MyAppDbContext(builder.Options);
        }
    }
}