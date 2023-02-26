using asari.com.tr.Application.Features.Projects.Dtos;
using asari.com.tr.Application.Features.Projects.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.Projects.Commands.DeleteProject;

public class DeleteProjectCommand : IRequest<DeletedProjectDto>
{
    // Kullanıcının bize göndereceği son dataları içeren yapı 
    public int Id { get; set; }

    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, DeletedProjectDto>
    {

        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly ProjectRules _projectRules;

        public DeleteProjectCommandHandler(IProjectRepository projectRepository, IMapper mapper, ProjectRules projectRules)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _projectRules = projectRules;
        }

        public async Task<DeletedProjectDto> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            Project? project = await _projectRepository.GetAsync(x => x.Id == request.Id); // Geriye hangi verileri sildiğimi görmek için kullandım

            await _projectRules.ProjectShouldExistWhenRequested(request.Id);

            _mapper.Map(request,project);
            Project deletedProject=await _projectRepository.DeleteAsync(project);
            DeletedProjectDto mappedDeletedProjectDto = _mapper.Map<DeletedProjectDto>(deletedProject);

            return mappedDeletedProjectDto;
        }
    }
}