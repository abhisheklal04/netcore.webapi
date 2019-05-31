using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using RestSharp;
using Services.Yodiz.Constants;
using Org.OpenAPITools.Model;
using Newtonsoft.Json;
using System.Net;

namespace Services.Yodiz.Api
{
    public class ProjectYodizService
    {
        private readonly IConfiguration _config;
        private readonly BaseYodizService _baseService;        

        public ProjectYodizService(IConfiguration config, BaseYodizService baseService)
        {
            _config = config;
            _baseService = baseService;
        }

        public List<Project> GetProjects(string fields="all")
        {
            var request = new RestRequest(YodizApiEndpoints.Projects, Method.GET);

            request.AddQueryParameter(nameof(fields), fields);

            return _baseService.ExecuteRequest<List<Project>>(request);
        }

        public UserStoryModel GetUserStories(int projectId, int limit = 1, int offset = 0, string fields = "all")
        {
            var url = YodizApiEndpoints.Projects + "/" + projectId + "/" + YodizApiEndpoints.UserStories;
            var request = new RestRequest(url, Method.GET);

            request.AddQueryParameter(nameof(fields), fields);
            request.AddQueryParameter(nameof(limit), limit.ToString());
            request.AddQueryParameter(nameof(offset), offset.ToString());
            var response = _baseService.ExecuteRequest<dynamic[]>(request);

            var userStories = new List<UserStory>();
            var paging = new Paging();
            
            if (response != null)
            {
                userStories = JsonConvert.DeserializeObject<List<UserStory>>(JsonConvert.SerializeObject(response[0]));
                paging = JsonConvert.DeserializeObject<Paging>(JsonConvert.SerializeObject(response[1]));
            }

            UserStoryModel result = new UserStoryModel(userStories, paging);

            return result;
        }
    }
}
