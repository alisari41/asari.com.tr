using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Rules;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Dtos;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Rules;
using asari.com.tr.Application.Features.Projects.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Commands.CreateProjectProgrammingLanguageTechnology;

public class CreateProjectProgrammingLanguageTechnologyCommand : IRequest<CreatedProjectProgrammingLanguageTechnologyDto>
{
    // Son kullanıcının bize göndereceği son dataları içeren yapı
    public int ProjectId { get; set; }
    public int ProgrammingLanguageTechnologyId { get; set; }

    public class CreateProjectProgrammingLanguageTechnologyCommandHandler : IRequestHandler<CreateProjectProgrammingLanguageTechnologyCommand, CreatedProjectProgrammingLanguageTechnologyDto>
    {
        private readonly IProjectProgrammingLanguageTechnologyRepository _projectProgrammingLanguageTechnologyRepository;
        private readonly IMapper _mapper;
        private readonly ProjectProgrammingLanguageTechnologyRules _projectProgrammingLanguageTechnologyRules;
        private readonly ProjectRules _projectRules;
        private readonly ProgrammingLanguageTechnologyBusinessRules _programmingLanguageTechnologyRules;

        public CreateProjectProgrammingLanguageTechnologyCommandHandler(IProjectProgrammingLanguageTechnologyRepository projectProgrammingLanguageTechnologyRepository, IMapper mapper, ProjectProgrammingLanguageTechnologyRules projectProgrammingLanguageTechnologyRules, ProjectRules projectRules, ProgrammingLanguageTechnologyBusinessRules programmingLanguageTechnologyRules)
        {
            _projectProgrammingLanguageTechnologyRepository = projectProgrammingLanguageTechnologyRepository;
            _mapper = mapper;
            _projectProgrammingLanguageTechnologyRules = projectProgrammingLanguageTechnologyRules;
            _projectRules = projectRules;
            _programmingLanguageTechnologyRules = programmingLanguageTechnologyRules;
        }

        public async Task<CreatedProjectProgrammingLanguageTechnologyDto> Handle(CreateProjectProgrammingLanguageTechnologyCommand request, CancellationToken cancellationToken)
        {
            await _projectProgrammingLanguageTechnologyRules.ProjectProgrammingLanguageTechnologySConNotBeDuplicatedWhenInserted(request.ProgrammingLanguageTechnologyId, request.ProjectId);
            await _projectRules.ProjectShouldExistWhenRequested(request.ProjectId);
            await _programmingLanguageTechnologyRules.ProgrammingLanguageTechnologyShouldExistWhenRequested(request.ProgrammingLanguageTechnologyId);

            ProjectProgrammingLanguageTechnology mappedProjectProgrammingLanguageTechnology = _mapper.Map<ProjectProgrammingLanguageTechnology>(request);
            ProjectProgrammingLanguageTechnology createdProjectProgrammingLanguageTechnology = await _projectProgrammingLanguageTechnologyRepository.AddAsync(mappedProjectProgrammingLanguageTechnology);
            CreatedProjectProgrammingLanguageTechnologyDto mappedCreatedProjectProgrammingLanguageTechnologyDto = _mapper.Map<CreatedProjectProgrammingLanguageTechnologyDto>(createdProjectProgrammingLanguageTechnology);

            return mappedCreatedProjectProgrammingLanguageTechnologyDto;
        }
    }
}