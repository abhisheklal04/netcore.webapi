using System;
using System.Collections.Generic;
using System.Text;
using CustomerApi.Models;
using CustomerApi.Models.Request;
using CustomerApi.Models.Response;
using CustomerApi.Repository;
using CustomerApi.Services;
using Xunit;
using CustomerApi.Common;

namespace Tests.Services
{
    public class CustomerServiceTests : BaseServiceTests
    {
        [Fact]
        public void Should_add_a_new_customer_and_returns_created_customer()
        {
            using (var context = CreateFakeDbContext(skipSeed: true))
            {
                var service = new CustomerService(context);
                var customer = new CustomerAddRequest()
                {
                    FirstName = "John",
                    LastName = "Smith",
                    DateOfBirth = DateTime.Now
                };

                var result = service.Add(customer);

                Assert.True(customer.FirstName == result.FirstName);
                Assert.True(customer.LastName == result.LastName);
                Assert.NotNull(result.Id.ToString());
            }
        }

        [Fact]
        public void Should_get_an_existing_customer()
        {
            using (var context = CreateFakeDbContext(skipSeed: true))
            {
                var customer = new Customer()
                {
                    Id = Guid.NewGuid(),
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString()
                };
                context.Customers.Add(customer);

                context.SaveChanges();

                var service = new CustomerService(context);

                var result = service.Get(customer.Id);

                Assert.True(customer.FirstName == result.FirstName);
                Assert.True(customer.LastName == result.LastName);
                Assert.True(result.Id.ToString() == customer.Id.ToString());
            }
        }

        [Fact]
        public void Should_throw_exeption_if_no_customer_found()
        {
            using (var context = CreateFakeDbContext(skipSeed: true))
            {
                var service = new CustomerService(context);

                Assert.Throws<NotFoundException>(() => { service.Get(Guid.NewGuid()); });
            }
        }

        [Fact]
        public void Should_update_a_customer()
        {
            using (var context = CreateFakeDbContext(skipSeed: true))
            {
                var customer = new Customer()
                {
                    Id = Guid.NewGuid(),
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString()
                };
                context.Customers.Add(customer);

                context.SaveChanges();

                var service = new CustomerService(context);

                var request = new CustomerUpdateRequest()
                {
                    FirstName = "fake",
                    LastName = "last fake",
                    DateOfBirth = DateTime.Now.Date
                };

                var result = service.Update(customer.Id, request);

                Assert.True(request.FirstName == result.FirstName);
                Assert.True(request.LastName == result.LastName);
                Assert.True(request.DateOfBirth.Value.Date == result.DateOfBirth.Value.Date);
            }
        }

        [Fact]
        public void Should_throw_exeption_when_updating_nonexisting_customer()
        {
            using (var context = CreateFakeDbContext(skipSeed: true))
            {
                var customer = new Customer()
                {
                    Id = Guid.NewGuid(),
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString()
                };
                context.Customers.Add(customer);

                context.SaveChanges();

                var service = new CustomerService(context);

                var request = new CustomerUpdateRequest()
                {
                    FirstName = "fake",
                    LastName = "last fake",
                    DateOfBirth = DateTime.Now
                };

                Assert.Throws<NotFoundException>(() => { service.Update(Guid.NewGuid(), request); } );
            }
        }

        [Fact]
        public void Should_not_add_customer_with_firstname_or_lastname_greater_than_50()
        {
            using (var context = CreateFakeDbContext(skipSeed: true))
            {
                var service = new CustomerService(context);
                var customerLongFirstName = new CustomerAddRequest()
                {
                    FirstName = @"John is a very long name
                                John is a very long name
                                John is a very long name
                                John is a very long name
                                John is a very long name
                                John is a very long name",
                    LastName = "Smith",
                    DateOfBirth = DateTime.Now
                };

                var customerLongLastName = new CustomerAddRequest()
                {
                    LastName = @"John is a very long name
                                John is a very long name
                                John is a very long name
                                John is a very long name
                                John is a very long name
                                John is a very long name",
                    FirstName = "Smith",
                    DateOfBirth = DateTime.Now
                };

                Assert.Throws<MaxLengthException>(() => { service.Add(customerLongFirstName); });
                Assert.Throws<MaxLengthException>(() => { service.Add(customerLongLastName); });
            }
        }

        [Fact]
        public void Should_not_update_customer_with_firstname_or_lastname_greater_than_50()
        {
            using (var context = CreateFakeDbContext(skipSeed: true))
            {
                var customer = new Customer()
                {
                    Id = Guid.NewGuid(),
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString()
                };

                context.Customers.Add(customer);

                context.SaveChanges();

                var service = new CustomerService(context);

                var longFirstNameRequest = new CustomerUpdateRequest()
                {
                    FirstName = @"John is a very long name
                                John is a very long name
                                John is a very long name
                                John is a very long name
                                John is a very long name
                                John is a very long name",
                    LastName = "Smith",
                    DateOfBirth = DateTime.Now
                };

                var longLastNameRequest = new CustomerUpdateRequest()
                {
                    LastName = @"John is a very long name
                                John is a very long name
                                John is a very long name
                                John is a very long name
                                John is a very long name
                                John is a very long name",
                    FirstName = "Smith",
                    DateOfBirth = DateTime.Now
                };

                Assert.Throws<MaxLengthException>(() => { service.Update(customer.Id, longFirstNameRequest); });
                Assert.Throws<MaxLengthException>(() => { service.Update(customer.Id, longLastNameRequest); });
            }
        }

        [Fact]
        public void Should_remove_a_customer()
        {
            using (var context = CreateFakeDbContext(skipSeed: true))
            {
                var customer = new Customer()
                {
                    Id = Guid.NewGuid(),
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString()
                };
                context.Customers.Add(customer);

                context.SaveChanges();

                var service = new CustomerService(context);

                service.Remove(customer.Id);

                Assert.Throws<NotFoundException>(() => { service.Get(customer.Id); });

            }
        }

        [Fact]
        public void Should_throw_exception_on_removing_a_nonexisting_or_deleted_customer()
        {
            using (var context = CreateFakeDbContext(skipSeed: true))
            {                
                var service = new CustomerService(context);
                Assert.Throws<NotFoundException>(() => { service.Remove(Guid.NewGuid()); });
            }
        }

        [Fact]
        public void Should_not_add_or_update_two_customers_with_same_firstname_and_lastname()
        {
            using (var context = CreateFakeDbContext(skipSeed: true))
            {
                var service = new CustomerService(context);
                var customer1 = new CustomerAddRequest()
                {
                    FirstName = "John",
                    LastName = "Smith",
                    DateOfBirth = DateTime.Now
                };

                var customer2 = new CustomerAddRequest()
                {
                    FirstName = "Steave",
                    LastName = "well",
                    DateOfBirth = DateTime.Now
                };

                var customer3 = new CustomerUpdateRequest()
                {
                    FirstName = "John",
                    LastName = "Smith",
                    DateOfBirth = DateTime.Now
                };

                var AddedCustomer1 = service.Add(customer1);
                var AddedCustomer2 = service.Add(customer2);

                Assert.Throws<CustomerExistsException>(() => { service.Add(customer1); });
                Assert.Throws<CustomerExistsException>(() => { service.Update(AddedCustomer2.Id, customer3); });
            }
        }

        [Fact]
        public void Should_add_or_update_removed_customer_with_same_name()
        {
            using (var context = CreateFakeDbContext(skipSeed: true))
            {
                var service = new CustomerService(context);
                var customer1 = new CustomerAddRequest()
                {
                    FirstName = "John",
                    LastName = "Smith",
                    DateOfBirth = DateTime.Now
                };

                var AddedCustomer1 = service.Add(customer1);
                service.Remove(AddedCustomer1.Id);

                var AddedCustomer2 = service.Add(customer1);

                Assert.NotEqual(AddedCustomer1.Id, AddedCustomer2.Id);                
                Assert.Equal(AddedCustomer1.FirstName, AddedCustomer2.FirstName);                
                Assert.Equal(AddedCustomer1.LastName, AddedCustomer2.LastName);                
            }
        }

        [Fact]
        public void Should_not_add_update_a_customer_without_firstname_and_lastname()
        {
            using (var context = CreateFakeDbContext(skipSeed: true))
            {
                var customer = new Customer()
                {
                    Id = Guid.NewGuid(),
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString()
                };
                context.Customers.Add(customer);

                context.SaveChanges();

                var service = new CustomerService(context);

                var customerNullFirstName = new CustomerAddRequest()
                {
                    FirstName = null,
                    LastName = "Smith",
                    DateOfBirth = DateTime.Now
                };

                var customerNullLastName = new CustomerAddRequest()
                {
                    LastName = null,
                    DateOfBirth = DateTime.Now
                };

                var customerEmptyFirstName = new CustomerAddRequest()
                {
                    FirstName = null,
                    LastName = "Smith",
                    DateOfBirth = DateTime.Now
                };

                var customerEmptyLastName = new CustomerAddRequest()
                {
                    LastName = null,
                    DateOfBirth = DateTime.Now
                };

                var updateNullFirstName = new CustomerUpdateRequest()
                {
                    FirstName = null,
                    LastName = "Smith",
                    DateOfBirth = DateTime.Now
                };

                var updateNullLastName = new CustomerUpdateRequest()
                {
                    LastName = null,
                    DateOfBirth = DateTime.Now
                };

                var updateEmptyFirstName = new CustomerUpdateRequest()
                {
                    FirstName = null,
                    LastName = "Smith",
                    DateOfBirth = DateTime.Now
                };

                var updateEmptyLastName = new CustomerUpdateRequest()
                {
                    LastName = null,
                    DateOfBirth = DateTime.Now
                };

                Assert.Throws<RequiredException>(() => { service.Add(customerNullFirstName); });
                Assert.Throws<RequiredException>(() => { service.Add(customerNullLastName); });
                Assert.Throws<RequiredException>(() => { service.Add(customerEmptyLastName); });
                Assert.Throws<RequiredException>(() => { service.Add(customerEmptyLastName); });

                Assert.Throws<RequiredException>(() => { service.Update(customer.Id, updateNullFirstName); });
                Assert.Throws<RequiredException>(() => { service.Update(customer.Id, updateNullLastName); });
                Assert.Throws<RequiredException>(() => { service.Update(customer.Id, updateEmptyLastName); });
                Assert.Throws<RequiredException>(() => { service.Update(customer.Id, updateEmptyLastName); });
            }
        }

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
        public void Should_search_caseinsensitive_customers_containing_any_partial_id_or_firstname_or_lastname()
        {
            using (var context = CreateFakeDbContext(skipSeed: true))
            {
                var customer1 = new Customer()
                {
                    Id = Guid.NewGuid(),
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString(),
                    IsArchived = false,
                };

                var customer2 = new Customer()
                {
                    Id = Guid.NewGuid(),
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString(),
                    IsArchived = false,
                };

                var customer3 = new Customer()
                {
                    Id = Guid.NewGuid(),
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString(),
                    IsArchived = false,
                };

                var customer4 = new Customer()
                {
                    Id = Guid.NewGuid(),
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString(),
                    IsArchived = false,
                };

                context.Customers.Add(customer1);
                context.Customers.Add(customer2);
                context.Customers.Add(customer3);
                context.Customers.Add(customer4);
                
                context.SaveChanges();

                var service = new CustomerService(context);

                var resultWithFirstName = service.GetPaged(customer1.FirstName.Substring(0,4)
                        , sortPage: SinglePageResult
                        , sortCol: CustomerListItemResponseSortCol.Id
                        , isArchived: false);

                var resultWithLastName = service.GetPaged(customer1.LastName.Substring(0, 7)
                        , sortPage: SinglePageResult
                        , sortCol: CustomerListItemResponseSortCol.Id
                        , isArchived: false);

                var resultWithId = service.GetPaged(customer1.Id.ToString().Substring(0, 2)
                        , sortPage: SinglePageResult
                        , sortCol: CustomerListItemResponseSortCol.Id
                        , isArchived: false);

                // case sensitive cases
                var resultWithFirstName2 = service.GetPaged(customer1.FirstName.Substring(0, 4).ToUpper()
                        , sortPage: SinglePageResult
                        , sortCol: CustomerListItemResponseSortCol.Id
                        , isArchived: false);

                var resultWithLastName2 = service.GetPaged(customer1.LastName.Substring(0, 7).ToUpper()
                        , sortPage: SinglePageResult
                        , sortCol: CustomerListItemResponseSortCol.Id
                        , isArchived: false);

                var resultWithId2 = service.GetPaged(customer1.Id.ToString().Substring(0, 2).ToUpper()
                        , sortPage: SinglePageResult
                        , sortCol: CustomerListItemResponseSortCol.Id
                        , isArchived: false);

                Assert.NotEmpty(resultWithFirstName?.Items);
                Assert.NotEmpty(resultWithLastName?.Items);
                Assert.NotEmpty(resultWithId?.Items);

                // case insensitive searching
                Assert.NotEmpty(resultWithFirstName2?.Items);
                Assert.NotEmpty(resultWithLastName2?.Items);
                Assert.NotEmpty(resultWithId2?.Items);
            }
        }

        [Fact]
        public void Should_search_customers_sorted_by_firstname_or_lastname()
        {
            using (var context = CreateFakeDbContext(skipSeed: true))
            {
                var customer1 = new Customer()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "A",
                    LastName = "A",
                    IsArchived = false,
                };

                var customer2 = new Customer()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "B",
                    LastName = "B",
                    IsArchived = false,
                };

                var customer3 = new Customer()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "C",
                    LastName = "C",
                    IsArchived = false,
                };

                var customer4 = new Customer()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "D",
                    LastName = "D",
                    IsArchived = false,
                };

                // adding items in decending order
                context.Customers.Add(customer4);
                context.Customers.Add(customer3);
                context.Customers.Add(customer2);
                context.Customers.Add(customer1);

                context.SaveChanges();

                var service = new CustomerService(context);

                var resultWithFirstName = service.GetPaged(null
                        , sortPage: new SortPageModel{ PageNumber = 1, PageSize = 10, SortDesc = false }
                        , sortCol: CustomerListItemResponseSortCol.FirstName
                        , isArchived: false);

                var resultWithLastName = service.GetPaged(null
                        , sortPage: new SortPageModel { PageNumber = 1, PageSize = 10, SortDesc = true }
                        , sortCol: CustomerListItemResponseSortCol.LastName
                        , isArchived: false);

                Assert.Equal(resultWithFirstName?.Items[0].FirstName, customer1.FirstName);
                Assert.Equal(resultWithLastName?.Items[0].LastName, customer4.LastName);
            }
        }

    }
}
