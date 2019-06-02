using Microsoft.Extensions.Configuration;
using System;
using WebApi.Repository;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Services;
using WebApi;

namespace Tests.Integration
{
    public static class SeedTestDatabase
    {
        public static void Run(IServiceProvider serviceProvider, IConfiguration config)
        {
            CustomDbContext dbContext = serviceProvider.GetService<CustomDbContext>();
            dbContext.Database.EnsureDeleted();

            dbContext.Customers.Add(new Customer()
            {
                Id = new Guid(),
                FirstName = "John",
                LastName = "Smith",
                DateOfBirth = DateTimeHelper.ParseIsoDate("1985-02-10"),
                CreatedDateTimeUtc = DateTime.UtcNow,
                UpdatedDateTimeUtc = DateTime.UtcNow,
                IsArchived = false
            });

            dbContext.Customers.Add(new Customer()
            {
                Id = new Guid(),
                FirstName = "Prince",
                LastName = "Wales",
                DateOfBirth = DateTimeHelper.ParseIsoDate("1990-07-11"),
                CreatedDateTimeUtc = DateTime.UtcNow,
                UpdatedDateTimeUtc = DateTime.UtcNow,
                IsArchived = false
            });

            dbContext.Customers.Add(new Customer()
            {
                Id = new Guid(),
                FirstName = "Robert",
                LastName = "Smith",
                DateOfBirth = DateTimeHelper.ParseIsoDate("1980-01-01"),
                CreatedDateTimeUtc = DateTime.UtcNow,
                UpdatedDateTimeUtc = DateTime.UtcNow,
                IsArchived = false
            });

            dbContext.SaveChanges();
        }
    }
}
