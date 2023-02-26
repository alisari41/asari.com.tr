using asari.com.tr.Application.Features.Projects.Dtos;
using asari.com.tr.Application.Features.Projects.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.Projects.Commands.CreateProject;

public class CreateProjectCommand : IRequest<CreatedProjectDto>
{
    // Son kullanıcının bize göndereceği son dataları içeren yapı
    public string Title { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public string Content { get; set; }
    public string? GithubLink { get; set; }
    public string? FolderUrl { get; set; }
    public DateTime? CreateDate { get; set; }

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, CreatedProjectDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly ProjectRules _projectRules;

        public CreateProjectCommandHandler(IProjectRepository projectRepository, IMapper mapper, ProjectRules projectRules)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _projectRules = projectRules;
        }

        public async Task<CreatedProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            await _projectRules.ProjectTitleConNotBeDuplicatedWhenInserted(request.Title);

            Project mappedProject = _mapper.Map<Project>(request);
            Project createdProject = await _projectRepository.AddAsync(mappedProject);
            CreatedProjectDto mappedCreatedProjectDto = _mapper.Map<CreatedProjectDto>(createdProject);

            return mappedCreatedProjectDto;
        }
    }
}