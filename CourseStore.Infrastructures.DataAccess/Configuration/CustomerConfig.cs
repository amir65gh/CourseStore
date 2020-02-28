using CourseStore.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CourseStore.Infrastructures.DataAccess.Configuration
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(c => c.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(c => c.LastName).IsRequired().HasMaxLength(100);
            builder.HasData(
                new Customer { Id = 1, FirstName = "Alex", LastName = "Pardon", Email = "a.pardon@gmail.com", MoneySpent = 200_000, CustomerStatus = CustomerStatus.Advanced, StatusExpirationDate = DateTime.UtcNow.AddDays(15) },
                new Customer { Id = 2, FirstName = "Nico", LastName = "Eickle", Email = "n.eickle@outlook.com", MoneySpent = 250_000, CustomerStatus = CustomerStatus.Regular }
                );
        }
    }
}
