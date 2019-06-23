using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Data.Models;

public class CustomerMap : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(p => p.LastName).HasMaxLength(150);
        builder.Property(p=> p.FirstName).HasMaxLength(150);
        builder.Property(p => p.Gender).HasMaxLength(10);
        builder.Property(p => p.PhoneNumber).HasMaxLength(100);
        PopulateData(builder);
    }


    public void PopulateData(EntityTypeBuilder<Customer> builder)
    {
        var customer = new List<Customer>()
        {
            new Customer()
            {
                LastName = "Prolifik",
                FirstName ="Lexzy",
                Id = Guid.Parse("99ae0c45-d682-4542-9ba7-1281e471916b"),
                Gender = "M",
                PhoneNumber = "08062066851",
                CreatedBy = Guid.Parse("016020e3-5c50-40b4-9e66-bba56c9f5bf2"),
                ModifiedBy = Guid.Parse("016020e3-5c50-40b4-9e66-bba56c9f5bf2"),
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow
            }
        };

        builder.HasData(customer);
    }
}