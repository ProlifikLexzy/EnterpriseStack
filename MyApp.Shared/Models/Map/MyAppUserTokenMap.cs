using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Shared.Models.Map
{
    public class MyAppUserTokenMap: IEntityTypeConfiguration<MyAppUserToken>
    {
        public void Configure(EntityTypeBuilder<MyAppUserToken> builder)
        {
            builder.HasKey(p => p.UserId);
        }
    }
}
