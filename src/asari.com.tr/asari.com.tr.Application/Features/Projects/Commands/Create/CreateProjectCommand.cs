using asari.com.tr.Application.Features.Projects.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.Projects.Commands.Create;

public class CreateProjectCommand : IRequest<CreatedProjectResponse>
{
    // Son kullanıcının bize göndereceği son dataları içeren yapı
    public string Title { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public string Content { get; set; }
    public string? GithubLink { get; set; }
    public string? FolderUrl { get; set; }
    public DateTime? CreateDate { get; set; }

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, CreatedProjectResponse>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly ProjectBusinessRules _projectRules;

        public CreateProjectCommandHandler(IProjectRepository projectRepository, IMapper mapper, ProjectBusinessRules projectRules)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _projectRules = projectRules;
        }

        public async Task<CreatedProjectResponse> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            await _projectRules.ProjectTitleConNotBeDuplicatedWhenInserted(request.Title);

            Project mappedProject = _mapper.Map<Project>(request);
            Project createdProject = await _projectRepository.AddAsync(mappedProject);
            CreatedProjectResponse mappedCreatedProjectDto = _mapper.Map<CreatedProjectResponse>(createdProject);

            return mappedCreatedProjectDto;
        }
    }
}