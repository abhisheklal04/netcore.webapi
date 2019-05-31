using Microsoft.Extensions.Configuration;
using System;
using WebApi.Repository;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Services;

namespace WebApi
{
    public static class SeedDatabase
    {
        public static void Run(IServiceProvider serviceProvider, IConfiguration config)
        {
            CustomDbContext dbContext = serviceProvider.GetService<CustomDbContext>();

            dbContext.Customers.Add(new Customer()
            {
                Id = new Guid(),
                FirstName = "John",
                LastName = "Smith",
                DateOfBirth = DateTime.Now,
                CreatedDateTimeUtc = DateTime.UtcNow,
                UpdatedDateTimeUtc = DateTime.UtcNow,
                IsArchived = false
            });

            dbContext.Customers.Add(new Customer()
            {
                Id = new Guid(),
                FirstName = "Prince",
                LastName = "Wales",
                DateOfBirth = DateTime.Now,
                CreatedDateTimeUtc = DateTime.UtcNow,
                UpdatedDateTimeUtc = DateTime.UtcNow,
                IsArchived = false
            });

            dbContext.Customers.Add(new Customer()
            {
                Id = new Guid(),
                FirstName = "Robert",
                LastName = "Smith",
                DateOfBirth = DateTime.Now,
                CreatedDateTimeUtc = DateTime.UtcNow,
                UpdatedDateTimeUtc = DateTime.UtcNow,
                IsArchived = false
            });

            dbContext.SaveChanges();
        }
    }
}
