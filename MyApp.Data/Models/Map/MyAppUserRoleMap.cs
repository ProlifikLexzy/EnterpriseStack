using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Data.Models;

namespace MyApp.Data.Models.Map
{
    public class MyAppUserRoleMap : IEntityTypeConfiguration<MyAppUserRole>
    {
        public void Configure(EntityTypeBuilder<MyAppUserRole> builder)
        {
            builder.HasKey(p => new { p.UserId, p.RoleId });

        }
    }
}