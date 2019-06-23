using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MyApp.Shared.Models.Map
{
    public class MyAppRoleMap : IEntityTypeConfiguration<MyAppRole>
    {
        public void Configure(EntityTypeBuilder<MyAppRole> builder)
        {
        }
    }
}