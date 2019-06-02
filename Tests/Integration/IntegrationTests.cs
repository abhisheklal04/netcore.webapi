﻿
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using WebApi;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using WebApi.Models.Response;
using Newtonsoft.Json;

namespace Tests.Integration
{
    public class IntegrationTests :
        IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup>
            _factory;

        public IntegrationTests(
            CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
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

    }
}