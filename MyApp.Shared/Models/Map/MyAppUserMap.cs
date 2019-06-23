using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace MyApp.Shared.Models.Map
{
    public class MyAppUserMap : IEntityTypeConfiguration<MyAppUser>
    {
        public MyAppUserMap()
        {
        }

        public PasswordHasher<MyAppUser> Hasher { get; set; }
        = new PasswordHasher<MyAppUser>();

        public void Configure(EntityTypeBuilder<MyAppUser> builder)
        {
            PopulateData(builder);
        }

        public void PopulateData(EntityTypeBuilder<MyAppUser> builder)
        {
            var customer = new List<MyAppUser>()
        {
            new MyAppUser()
            {
                 Activated = true,
                CreatedOnUtc = DateTime.Now,
                Id = Guid.Parse("016020e3-5c50-40b4-9e66-bba56c9f5bf2"),
                FullName = "Smartware Solutions",
                FirstName = "Smartware",
                LastName = "Solutions",
                LastLoginDate = DateTime.Now,
                Email = "mitserver.devapp@gmail.com",
                EmailConfirmed = true,
                NormalizedEmail = "mitserver.devapp@gmail.com".ToUpper(),
                PhoneNumber = "08062066851",
                UserName = "mitserver.devapp@gmail.com",
                NormalizedUserName = "mitserver.devapp@gmail.com".ToUpper(),
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = true,
                PasswordHash = Hasher.HashPassword(null, "micr0s0ft_"),
                SecurityStamp = "016020e3-5c50-40b4-9e66-bba56c9f5bf2",
            }
        };

            builder.HasData(customer);
        }
    }
}