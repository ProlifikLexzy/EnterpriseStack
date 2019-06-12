using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Data.Models;
using System;

namespace MyApp.Data.Models.Map
{
    public class MyAppRoleMap : IEntityTypeConfiguration<MyAppRole>
    {
        public void Configure(EntityTypeBuilder<MyAppRole> builder)
        {
        }
    }
}