using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Models;
using WebApi.Models.Request;
using WebApi.Models.Response;
using WebApi.Repository;
using WebApi.Services;
using Xunit;

namespace Tests.Services
{
    public class CustomerServiceTests : BaseServiceTests
    {
        [Fact]
        public void Should_add_a_new_customer() { }

        [Fact]
        public void Should_get_an_existing_customer() { }

        [Fact]
        public void Should_throw_exeption_if_no_customer_found() { }

        [Fact]
        public void Should_update_a_customer() { }

        [Fact]
        public void Should_throw_exeption_when_updating_nonexisting_customer() { }

        [Fact]
        public void Should_not_add_customer_with_firstname_or_lastname_greater_than_50() { }

        [Fact]
        public void Should_not_update_customer_with_firstname_or_lastname_greater_than_50() { }

        [Fact]
        public void Should_remove_a_customer() { }

        [Fact]
        public void Should_throw_exception_on_removing_a_nonexisting_or_deleted_customer() { }
        
        [Fact]
        public void Should_not_add_two_customers_with_same_firstname_and_lastname() { }

        [Fact]
        public void Should_not_add_a_customer_without_firstname_and_lastname() { }

        [Fact]
        public void Should_search_for_paged_customers()
        {
            using (var context = CreateFakeDbContext(skipSeed: true))
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

        [Fact]
        public void Should_search_customers_containing_any_partial_id_or_firstname_or_lastname() { }

        [Fact]
        public void Should_search_customers_with_case_insensitive_keyword() { }

        [Fact]
        public void Should_search_customers_sorted_by_id_firstname_or_lastname() { }

    }
}
