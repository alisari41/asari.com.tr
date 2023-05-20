using asari.com.tr.Application.Features.Projects.Constants;
using asari.com.tr.Application.Features.Projects.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.Projects.Constants.ProjectsOperationClaims;

namespace asari.com.tr.Application.Features.Projects.Commands.Update;

public class UpdateProjectCommand : IRequest<UpdatedProjectResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Content { get; set; }
    public string? GithubLink { get; set; }
    public string? FolderUrl { get; set; }
    public DateTime? CreateDate { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string? CacheGroupKey => CacheGroupKeyValue.ProjectCacheGroupKey;

    public string[] Roles => new[] { Admin, Write, ProjectsOperationClaims.Update };

    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, UpdatedProjectResponse>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly ProjectBusinessRules _projectRules;

        public UpdateProjectCommandHandler(IProjectRepository projectRepository, IMapper mapper, ProjectBusinessRules projectRules)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _projectRules = projectRules;
        }

        public async Task<UpdatedProjectResponse> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            Project? project = await _projectRepository.GetAsync(x => x.Id == request.Id);

            await _projectRules.ProjectShouldExistWhenRequested(request.Id);

            _mapper.Map(request, project);
            await _projectRules.ProjectTitleConNotBeDuplicatedWhenUpdated(project); // Mapleme işleminden sonra kullanılır.

            Project updatedProject = await _projectRepository.UpdateAsync(project);
            UpdatedProjectResponse mappedUpdatedProjectDto = _mapper.Map<UpdatedProjectResponse>(updatedProject);

            return mappedUpdatedProjectDto;
        }
    }
}