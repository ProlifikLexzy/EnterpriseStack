using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Data.Models;
using System;

namespace MyApp.Data.Models.Map
{
    public class MyAppUserMap : IEntityTypeConfiguration<MyAppUser>
    {
        public MyAppUserMap()
        {
        }

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MyAppUser> builder)
        {
        }
    }
}