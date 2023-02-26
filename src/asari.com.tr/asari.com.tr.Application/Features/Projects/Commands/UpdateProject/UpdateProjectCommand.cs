using asari.com.tr.Application.Features.Projects.Dtos;
using asari.com.tr.Application.Features.Projects.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.Projects.Commands.UpdateProject;

public class UpdateProjectCommand : IRequest<UpdatedProjectDto>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Content { get; set; }
    public string? GithubLink { get; set; }
    public string? FolderUrl { get; set; }
    public DateTime? CreateDate { get; set; }

    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, UpdatedProjectDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly ProjectRules _projectRules;

        public UpdateProjectCommandHandler(IProjectRepository projectRepository, IMapper mapper, ProjectRules projectRules)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _projectRules = projectRules;
        }

        public async Task<UpdatedProjectDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            Project? project = await _projectRepository.GetAsync(x => x.Id == request.Id);

            await _projectRules.ProjectShouldExistWhenRequested(request.Id);

            _mapper.Map(request, project);
            await _projectRules.ProjectTitleConNotBeDuplicatedWhenUpdated(project); // Mapleme işleminden sonra kullanılır.

            Project updatedProject = await _projectRepository.UpdateAsync(project);
            UpdatedProjectDto mappedUpdatedProjectDto = _mapper.Map<UpdatedProjectDto>(updatedProject);

            return mappedUpdatedProjectDto;
        }
    }
}