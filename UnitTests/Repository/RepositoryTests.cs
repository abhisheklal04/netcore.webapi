using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using WebApi.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Tests.Repository
{
    public class RepositoryTests
    {
        public DbContextOptions GetFakeDbOptions()
        {
            return new DbContextOptionsBuilder<CustomerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;            
        }
        
        [Fact]
        public void It_should_initialize_customer_db()
        {
            using (var context = new CustomerContext(GetFakeDbOptions()))
            {
                Assert.NotNull(context.Customers);
                Assert.Empty(context.Customers);
            }
        }

    }
}
