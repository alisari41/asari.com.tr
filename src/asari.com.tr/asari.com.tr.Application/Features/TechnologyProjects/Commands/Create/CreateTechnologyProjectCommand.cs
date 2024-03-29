﻿using asari.com.tr.Application.Features.Projects.Rules;
using asari.com.tr.Application.Features.Technologies.Rules;
using asari.com.tr.Application.Features.TechnologyProjects.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.TechnologyProjects.Constants.TechnologyProjectsOperationClaims;

namespace asari.com.tr.Application.Features.TechnologyProjects.Commands.Create;

public class CreateTechnologyProjectCommand : IRequest<CreatedTechnologyProjectResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int TechnologyId { get; set; }
    public int ProjectId { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { CacheGroupKeyValue.TechnologyProjectCacheGroupKey };

    public string[] Roles => new[] { Admin, Write, Add };

    public class CreateTechnologyProjectCommandHandler : IRequestHandler<CreateTechnologyProjectCommand, CreatedTechnologyProjectResponse>
    {
        private readonly ITechnologyProjectRepository _technologyProjectRepository;
        private readonly IMapper _mapper;
        private readonly TechnologyProjectBusinessRules _technologyProjectBusinessRules;
        private readonly TechnologyBusinessRules _technologyBusinessRules;
        private readonly ProjectBusinessRules _projectBusinessRules;

        public CreateTechnologyProjectCommandHandler(ITechnologyProjectRepository technologyProjectRepository, IMapper mapper, TechnologyProjectBusinessRules technologyProjectBusinessRules, TechnologyBusinessRules technologyBusinessRules, ProjectBusinessRules projectBusinessRules)
        {
            _technologyProjectRepository = technologyProjectRepository;
            _mapper = mapper;
            _technologyProjectBusinessRules = technologyProjectBusinessRules;
            _technologyBusinessRules = technologyBusinessRules;
            _projectBusinessRules = projectBusinessRules;
        }

        public async Task<CreatedTechnologyProjectResponse> Handle(CreateTechnologyProjectCommand request, CancellationToken cancellationToken)
        {
            await _technologyProjectBusinessRules.TechnologyProjectSConNotBeDuplicatedWhenInserted(request.TechnologyId, request.ProjectId);
            await _technologyBusinessRules.TechnologyShouldExistWhenRequested(request.TechnologyId);
            await _projectBusinessRules.ProjectShouldExistWhenRequested(request.ProjectId);

            TechnologyProject mappedTechnologyProject = _mapper.Map<TechnologyProject>(request);
            TechnologyProject createdTechnologyProject = await _technologyProjectRepository.AddAsync(mappedTechnologyProject);
            CreatedTechnologyProjectResponse mappedCreatedTechnologyProjectResponse = _mapper.Map<CreatedTechnologyProjectResponse>(createdTechnologyProject);

            return mappedCreatedTechnologyProjectResponse;
        }
    }
}