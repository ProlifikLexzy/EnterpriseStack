using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Shared.Models.Map
{
    public class MyAppUserLoginMap : IEntityTypeConfiguration<MyAppUserLogin>
    {
        public void Configure(EntityTypeBuilder<MyAppUserLogin> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.HasKey(u => new { u.LoginProvider, u.ProviderKey });

        }
    }
}
