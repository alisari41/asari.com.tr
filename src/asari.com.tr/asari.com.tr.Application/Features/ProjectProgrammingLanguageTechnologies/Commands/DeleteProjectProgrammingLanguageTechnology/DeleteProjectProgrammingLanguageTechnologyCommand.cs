using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Dtos;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Commands.DeleteProjectProgrammingLanguageTechnology;

public class DeleteProjectProgrammingLanguageTechnologyCommand : IRequest<DeletedProjectProgrammingLanguageTechnologyDto>
{
    public int Id { get; set; }

    public class DeleteProjectProgrammingLanguageTechnologyCommandHandler : IRequestHandler<DeleteProjectProgrammingLanguageTechnologyCommand, DeletedProjectProgrammingLanguageTechnologyDto>
    {

        private readonly IProjectProgrammingLanguageTechnologyRepository _projectProgrammingLanguageTechnologyRepository;
        private readonly IMapper _mapper;
        private readonly ProjectProgrammingLanguageTechnologyRules _projectProgrammingLanguageTechnologyRules;

        public DeleteProjectProgrammingLanguageTechnologyCommandHandler(IProjectProgrammingLanguageTechnologyRepository projectProgrammingLanguageTechnologyRepository, IMapper mapper, ProjectProgrammingLanguageTechnologyRules projectProgrammingLanguageTechnologyRules)
        {
            _projectProgrammingLanguageTechnologyRepository = projectProgrammingLanguageTechnologyRepository;
            _mapper = mapper;
            _projectProgrammingLanguageTechnologyRules = projectProgrammingLanguageTechnologyRules;
        }

        public async Task<DeletedProjectProgrammingLanguageTechnologyDto> Handle(DeleteProjectProgrammingLanguageTechnologyCommand request, CancellationToken cancellationToken)
        {
            ProjectProgrammingLanguageTechnology? projectProgrammingLanguageTechnology = await _projectProgrammingLanguageTechnologyRepository.GetAsync(x => x.Id == request.Id);

            await _projectProgrammingLanguageTechnologyRules.ProjectProgrammingLanguageTechnologyShouldExistWhenRequested(request.Id);

            _mapper.Map(request, projectProgrammingLanguageTechnology);
            ProjectProgrammingLanguageTechnology deletedProjectProgrammingLanguageTechnology = await _projectProgrammingLanguageTechnologyRepository.DeleteAsync(projectProgrammingLanguageTechnology);
            DeletedProjectProgrammingLanguageTechnologyDto mappedDeletedProjectProgrammingLanguageTechnologyDto = _mapper.Map<DeletedProjectProgrammingLanguageTechnologyDto>(deletedProjectProgrammingLanguageTechnology);

            return mappedDeletedProjectProgrammingLanguageTechnologyDto;
        }
    }
}