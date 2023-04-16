using asari.com.tr.Application.Features.Projects.Constants;
using asari.com.tr.Application.Features.Projects.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static asari.com.tr.Application.Features.Projects.Constants.ProjectsOperationClaims;

namespace asari.com.tr.Application.Features.Projects.Commands.Delete;

public class DeleteProjectCommand : IRequest<DeletedProjectResponse>, ISecuredRequest
{
    // Kullanıcının bize göndereceği son dataları içeren yapı 
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ProjectsOperationClaims.Delete };

    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, DeletedProjectResponse>
    {

        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly ProjectBusinessRules _projectRules;

        public DeleteProjectCommandHandler(IProjectRepository projectRepository, IMapper mapper, ProjectBusinessRules projectRules)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _projectRules = projectRules;
        }

        public async Task<DeletedProjectResponse> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            Project? project = await _projectRepository.GetAsync(x => x.Id == request.Id); // Geriye hangi verileri sildiğimi görmek için kullandım

            await _projectRules.ProjectShouldExistWhenRequested(request.Id);

            _mapper.Map(request, project);
            Project deletedProject = await _projectRepository.DeleteAsync(project);
            DeletedProjectResponse mappedDeletedProjectDto = _mapper.Map<DeletedProjectResponse>(deletedProject);

            return mappedDeletedProjectDto;
        }
    }
}