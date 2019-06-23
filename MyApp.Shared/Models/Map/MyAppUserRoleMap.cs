using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyApp.Shared.Models.Map
{
    public class MyAppUserRoleMap : IEntityTypeConfiguration<MyAppUserRole>
    {
        public void Configure(EntityTypeBuilder<MyAppUserRole> builder)
        {
            builder.HasKey(p => new { p.UserId, p.RoleId });

        }
    }
}