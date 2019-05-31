using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Models;
using WebApi.Models.Response;
using WebApi.Repository;
using WebApi.Services;
using Xunit;

namespace Tests.Services
{
    public class CustomerServiceTests : BaseTests
    {
        [Fact]
        public void should_support_paging()
        {
            using (var context = CreateDbContext(skipSeed: true))
            {
                // Seed data.
                for (int i = 0; i < 22; i++)
                    context.Customers.Add(new Customer()
                    {
                        Id = Guid.NewGuid(),
                        FirstName = Guid.NewGuid().ToString(),
                        LastName = Guid.NewGuid().ToString()
                    });

                context.SaveChanges();

                var service = new CustomerService(context);

                // Assert query all 3 pages.
                for (int pageNumber = 1; pageNumber <= 3; pageNumber++)
                {
                    var pagedResults = service.GetPaged(keyword: null
                        , sortPage: new SortPageModel() { PageNumber = pageNumber, PageSize = 10 }
                        , sortCol: CustomerListItemResponseSortCol.Id
                        , isArchived: false);

                    Assert.Equal(pageNumber, pagedResults.CurrentPage);
                    Assert.Equal(10 * (pageNumber - 1) >= 20 ? 2 : 10, pagedResults.Items.Count);
                    Assert.Equal(10 * (pageNumber - 1) >= 20 ? 2 : 10, pagedResults.ItemsPerPage);
                    Assert.Equal(22, pagedResults.TotalItems);
                    Assert.Equal(3, pagedResults.TotalPages);
                }
            }
        }
    }
}
