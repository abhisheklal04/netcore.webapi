using Microsoft.EntityFrameworkCore;
using System;
using WebApi;
using WebApi.Models;
using WebApi.Repository;
using Xunit;

namespace Tests
{
    public class BaseTests
    {
        public SortPageModel ALL = new SortPageModel() { PageNumber = 1, PageSize = 999999, SortCol = "id", SortDesc = false };

        public CustomDbContext CreateDbContext(bool skipSeed = false)
        {
            var options = new DbContextOptionsBuilder<CustomDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new CustomDbContext(options);

            if (!skipSeed)
            {
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
            

            return dbContext;
        }
    }
}
