using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using CustomerApi.Controllers;
using CustomerApi.Models;
using CustomerApi.Models.Request;
using CustomerApi.Models.Response;
using CustomerApi.Repository;
using CustomerApi.Services;
using CustomerApi.Services.Interface;
using Xunit;

namespace Tests.Controller
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerService> _customerServiceMock;

        public CustomerControllerTests()
        {
            _customerServiceMock = new Mock<ICustomerService>();
        }

        public CustomerController GetCustomerController(ICustomerService customerService)
        {
            return new CustomerController(customerService);
        }

        [Fact]
        public void Should_get_a_customer_details()
        {
            var expectedCustomer = new CustomerResponse()
            {
                Id = Guid.NewGuid(),
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                DateOfBirth = DateTime.Now
            };

            _customerServiceMock.Setup(foo => foo.Get(expectedCustomer.Id)).Returns(expectedCustomer);

            var controller = GetCustomerController(_customerServiceMock.Object);

            var actionResult = controller.Get(expectedCustomer.Id);

            _customerServiceMock.Verify(foo => foo.Get(It.IsAny<Guid>()), Times.Once());


            //Assert
            Assert.Equal(actionResult.Id, expectedCustomer.Id);
            Assert.Equal(actionResult.FirstName, expectedCustomer.FirstName);
            Assert.Equal(actionResult.LastName, expectedCustomer.LastName);
        }

        [Fact]
        public void Should_update_customer()
        {
            _customerServiceMock.Setup(foo => foo.Update(It.IsAny<Guid>(), It.IsAny<CustomerUpdateRequest>())).Returns(It.IsAny<Customer>());

            var controller = GetCustomerController(_customerServiceMock.Object);

            controller.Update(It.IsAny<Guid>(), It.IsAny<CustomerUpdateRequest>());

            _customerServiceMock.Verify(foo => foo.Update(It.IsAny<Guid>(), It.IsAny<CustomerUpdateRequest>()), Times.Once());

            Assert.True(true);
        }

        [Fact]
        public void Should_add_new_customer_and_returns_a_new_customer_id()
        {
            var expectedCustomer = new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                DateOfBirth = DateTime.Now
            };

            _customerServiceMock.Setup(foo => foo.Add(It.IsAny<CustomerAddRequest>())).Returns(expectedCustomer);

            var controller = GetCustomerController(_customerServiceMock.Object);

            var result = controller.Add(It.IsAny<CustomerAddRequest>());

            _customerServiceMock.Verify(foo => foo.Add(It.IsAny<CustomerAddRequest>()), Times.Once());

            Assert.Equal(expectedCustomer.Id, result);
        }    

        [Fact]
        public void Should_remove_a_customer()
        {
            _customerServiceMock.Setup(foo => foo.Remove(It.IsAny<Guid>()));

            var controller = GetCustomerController(_customerServiceMock.Object);

            controller.Delete(It.IsAny<Guid>());

            _customerServiceMock.Verify(foo => foo.Remove(It.IsAny<Guid>()), Times.Once());

            Assert.True(true);
        }

        [Fact]
        public void Should_search_for_customers_in_paged_format()
        {
            SortPageModel pageModel = new SortPageModel
            {
                PageNumber = 1,
                PageSize = 1,
                SortCol = It.IsAny<string>(),
                SortDesc = It.IsAny<bool>(),
            };

            _customerServiceMock.Setup(foo => foo.GetPaged(
                It.IsAny<string>(),
                It.IsAny<CustomerListItemResponseSortCol>(),
                It.IsAny<bool>(),
                pageModel
                ))
                .Returns(It.IsAny<CustomerListResponse>());

            var controller = GetCustomerController(_customerServiceMock.Object);
            var actionResult = controller.GetAll(
                keyword: It.IsAny<string>(),
                isArchived: It.IsAny<bool>(),
                pageNumber: 1,
                sortCol: It.IsAny<CustomerListItemResponseSortCol>(),
                pageSize: 1,
                sortDesc: It.IsAny<bool>()
                );

            _customerServiceMock.Verify(foo => foo.GetPaged(
                It.IsAny<string>(),
                It.IsAny<CustomerListItemResponseSortCol>(),
                It.IsAny<bool>(),
                It.IsAny<SortPageModel>()
                ), Times.Once());

        }
    }
}
