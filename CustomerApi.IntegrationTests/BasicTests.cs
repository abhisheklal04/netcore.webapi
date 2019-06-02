
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using CustomerApi;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using CustomerApi.Models.Response;
using CustomerApi.Models.Request;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace IntegrationTests
{
    public class BasicTests :
        IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup>
            _factory;

        public BasicTests(
            CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Fact]
        public async Task Get_AllCustomers()
        {
            // Arrange
            var response = await _client.GetAsync("v1/Customer?isArchived=false&pageNumber=1&pageSize=20&sortCol=Id&sortDesc=false");

            Assert.True(response.StatusCode == HttpStatusCode.OK);

            if (response.Content != null)
            {
                var customerResponse = JsonConvert.DeserializeObject<CustomerListResponse>(
                    response.Content.ReadAsStringAsync().Result);
                Assert.NotEmpty(customerResponse.Items);
            }
        }

        [Fact]
        public async Task Get_SingleCustomerDetails()
        {
            var responseAllCustomers = await _client.GetAsync("v1/Customer?isArchived=false&pageNumber=1&pageSize=20&sortCol=Id&sortDesc=false");

            var customerAllResponse = JsonConvert.DeserializeObject<CustomerListResponse>(
                    responseAllCustomers.Content.ReadAsStringAsync().Result);
            var singleCustomer = customerAllResponse.Items[0];

            // Arrange
            var response = await _client.GetAsync("v1/Customer/" + singleCustomer.Id);

            Assert.True(response.StatusCode == HttpStatusCode.OK);

            if (response.Content != null)
            {
                var customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(
                    response.Content.ReadAsStringAsync().Result);

                Assert.NotNull(customerResponse);
                Assert.NotNull(customerResponse.Id.ToString());
                Assert.NotNull(customerResponse.FirstName.ToString());
                Assert.NotNull(customerResponse.LastName.ToString());
            }
        }

        [Fact]
        public async Task Delete_a_Customer()
        {
            var responseAllCustomers = await _client.GetAsync("v1/Customer?isArchived=false&pageNumber=1&pageSize=20&sortCol=Id&sortDesc=false");

            var customerAllResponse = JsonConvert.DeserializeObject<CustomerListResponse>(
                    responseAllCustomers.Content.ReadAsStringAsync().Result);
            var singleCustomer = customerAllResponse.Items[0];

            // Arrange
            var response = await _client.DeleteAsync("v1/Customer/" + singleCustomer.Id);

            Assert.True(response.StatusCode == HttpStatusCode.OK);

        }

        [Fact]
        public async Task Update_a_Customer()
        {
            var responseAllCustomers = await _client.GetAsync("v1/Customer?isArchived=false&pageNumber=1&pageSize=20&sortCol=Id&sortDesc=false");

            var customerAllResponse = JsonConvert.DeserializeObject<CustomerListResponse>(
                    responseAllCustomers.Content.ReadAsStringAsync().Result);
            var singleCustomer = customerAllResponse.Items[0];

            var updateRequest = new CustomerUpdateRequest()
            {
                FirstName = singleCustomer.FirstName,
                LastName = singleCustomer.LastName,
                DateOfBirth = singleCustomer.DateOfBirth
            };

            var jsonString = JsonConvert.SerializeObject(updateRequest);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("v1/Customer/" + singleCustomer.Id, content);

            Assert.True(response.StatusCode == HttpStatusCode.OK);

        }

        [Fact]
        public async Task Add_a_Customer()
        {
            var updateRequest = new CustomerUpdateRequest()
            {
                FirstName = "New",
                LastName = "singleCustomer",
                DateOfBirth = DateTime.Now.Date
            };

            var jsonString = JsonConvert.SerializeObject(updateRequest);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("v1/Customer/", content);

            Assert.True(response.StatusCode == HttpStatusCode.OK);

        }

    }
}