using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Org.OpenAPITools.Model;
using RestSharp;
using Common;
using Services.Yodiz.Constants;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Services.Yodiz.Api
{

    class CamelJsonSerializerStrategy : PocoJsonSerializerStrategy
    {
        protected override string MapClrMemberNameToJsonFieldName(string clrPropertyName)
        {
            //PascalCase to camelCase
            return Char.ToLowerInvariant(clrPropertyName[0]) + clrPropertyName.Substring(1);
        }
    }

    public class BaseYodizService
    {        
        private RestClient _client;
        private string _apiToken;
        private readonly IOptions<ServiceSettings> _serviceSettings;

        public BaseYodizService(IOptions<ServiceSettings> serviceSettings)
        {
            _serviceSettings = serviceSettings;
            // _client.AddDefaultHeader("api-key", "e3c9e05a-30a1-47c5-83c3-82b808e4a8e9");            
            _client = new RestClient(_serviceSettings.Value.YodizBaseUrl);
            _client.AddDefaultHeader("api-key", _serviceSettings.Value.Apikey);
            _client.AddDefaultHeader("Content-Type", "application/json");

            SimpleJson.CurrentJsonSerializerStrategy = new CamelJsonSerializerStrategy();

            YodizAdminLogin();
        }

        public void AddApiToken(IRestRequest request)
        {
            request.AddHeader(YodizApiEndpoints.ApiTokenHeader, _apiToken);
        }

        public T ExecuteRequest<T>(IRestRequest request)
        {
            AddApiToken(request);
            var response = IsRequestUnauthorised(_client.Execute(request));
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public void YodizAdminLogin()
        {
            var request = new RestRequest(YodizApiEndpoints.Login);

            var body = new LoginRequest(_serviceSettings.Value.YodizAdminEmail, _serviceSettings.Value.YodizAdminPassword);

            request.AddJsonBody(body);

            var response = _client.Post<LoginResponse>(request);

            _apiToken = response.Data.ApiToken;
        }

        public void RefreshToken()
        {
            YodizAdminLogin();
        }

        public IRestResponse IsRequestUnauthorised(IRestResponse response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                RefreshToken();
                _client.Execute(response.Request);
            }

            return response;
        }
    }
}
