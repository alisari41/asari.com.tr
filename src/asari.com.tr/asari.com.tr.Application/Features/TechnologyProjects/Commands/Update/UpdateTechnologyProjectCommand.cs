using asari.com.tr.Application.Features.Projects.Rules;
using asari.com.tr.Application.Features.Technologies.Rules;
using asari.com.tr.Application.Features.TechnologyProjects.Constants;
using asari.com.tr.Application.Features.TechnologyProjects.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.TechnologyProjects.Constants.TechnologyProjectsOperationClaims;

namespace asari.com.tr.Application.Features.TechnologyProjects.Commands.Update;

public class UpdateTechnologyProjectCommand : IRequest<UpdatedTechnologyProjectResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }
    public int TechnologyId { get; set; }
    public int ProjectId { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string? CacheGroupKey => CacheGroupKeyValue.TechnologyProjectCacheGroupKey;

    public string[] Roles => new[] { Admin, Write, TechnologyProjectsOperationClaims.Update };

    public class UpdateTechnologyProjectCommandHandler : IRequestHandler<UpdateTechnologyProjectCommand, UpdatedTechnologyProjectResponse>
    {
        private readonly ITechnologyProjectRepository _technologyProjectRepository;
        private readonly IMapper _mapper;
        private readonly TechnologyProjectBusinessRules _technologyProjectBusinessRules;
        private readonly TechnologyBusinessRules _technologyBusinessRules;
        private readonly ProjectBusinessRules _projectBusinessRules;

        public UpdateTechnologyProjectCommandHandler(ITechnologyProjectRepository technologyProjectRepository, IMapper mapper, TechnologyProjectBusinessRules technologyProjectBusinessRules, TechnologyBusinessRules technologyBusinessRules, ProjectBusinessRules projectBusinessRules)
        {
            _technologyProjectRepository = technologyProjectRepository;
            _mapper = mapper;
            _technologyProjectBusinessRules = technologyProjectBusinessRules;
            _technologyBusinessRules = technologyBusinessRules;
            _projectBusinessRules = projectBusinessRules;
        }

        public async Task<UpdatedTechnologyProjectResponse> Handle(UpdateTechnologyProjectCommand request, CancellationToken cancellationToken)
        {
            TechnologyProject? technologyProject = await _technologyProjectRepository.GetAsync(x => x.Id == request.Id);

            await _technologyProjectBusinessRules.TechnologyProjectShouldExistWhenRequested(request.Id);

            _mapper.Map(request, technologyProject);

            await _technologyProjectBusinessRules.TechnologyProjectSConNotBeDuplicatedWhenUpdated(technologyProject);
            await _technologyBusinessRules.TechnologyShouldExistWhenRequested(request.TechnologyId);
            await _projectBusinessRules.ProjectShouldExistWhenRequested(request.ProjectId);

            TechnologyProject updatedTechnologyProject = await _technologyProjectRepository.UpdateAsync(technologyProject);
            UpdatedTechnologyProjectResponse mappedUpdatedTechnologyProjectResponse = _mapper.Map<UpdatedTechnologyProjectResponse>(updatedTechnologyProject);

            return mappedUpdatedTechnologyProjectResponse;
        }
    }
}