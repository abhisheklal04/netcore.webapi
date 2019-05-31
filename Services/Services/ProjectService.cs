using System;
using System.Collections.Generic;
using AutoMapper;
using Common.ViewModels;
using Services.Yodiz.Api;

namespace Services
{
    public class ProjectService
    {
        private readonly ProjectYodizService _projectYodizService;
        private readonly IMapper _mapper;

        public ProjectService(
            ProjectYodizService projectYodizService,
            IMapper mapper
            )
        {
            _projectYodizService = projectYodizService;
            _mapper = mapper;
        }

        public List<ProjectResponse> GetProjects()
        {
            var response = _mapper.Map<List<ProjectResponse>>(_projectYodizService.GetProjects());
            return response;
        }

        public UserStoryModel GetUserStories(int projectId, int limit, int offset)
        {
            var response = _mapper.Map<UserStoryModel>(_projectYodizService.GetUserStories(projectId, limit, offset));
            return response;
        }
    }
}
