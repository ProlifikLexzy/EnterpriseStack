using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Data.Models.Map
{
    public class MyAppUserClaimMap : IEntityTypeConfiguration<MyAppUserClaim>
    {
        public void Configure(EntityTypeBuilder<MyAppUserClaim> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
}
