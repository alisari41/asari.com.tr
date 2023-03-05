using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Rules;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Rules;
using asari.com.tr.Application.Features.Projects.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Commands.Update;

public class UpdateProjectProgrammingLanguageTechnologyCommand : IRequest<UpdatedProjectProgrammingLanguageTechnologyResponse>
{
    // Son kullanıcının bize göndereceği son dataları içeren yapı
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int ProgrammingLanguageTechnologyId { get; set; }

    public class UpdateProjectProgrammingLanguageTechnologyCommandHandler : IRequestHandler<UpdateProjectProgrammingLanguageTechnologyCommand, UpdatedProjectProgrammingLanguageTechnologyResponse>
    {
        private readonly IProjectProgrammingLanguageTechnologyRepository _projectProgrammingLanguageTechnologyRepository;
        private readonly IMapper _mapper;
        private readonly ProjectProgrammingLanguageTechnologyBusinessRules _projectProgrammingLanguageTechnologyRules;
        private readonly ProjectRules _projectRules;
        private readonly ProgrammingLanguageTechnologyBusinessRules _programmingLanguageTechnologyRules;

        public UpdateProjectProgrammingLanguageTechnologyCommandHandler(IProjectProgrammingLanguageTechnologyRepository projectProgrammingLanguageTechnologyRepository, IMapper mapper, ProjectProgrammingLanguageTechnologyBusinessRules projectProgrammingLanguageTechnologyRules, ProjectRules projectRules, ProgrammingLanguageTechnologyBusinessRules programmingLanguageTechnologyRules)
        {
            _projectProgrammingLanguageTechnologyRepository = projectProgrammingLanguageTechnologyRepository;
            _mapper = mapper;
            _projectProgrammingLanguageTechnologyRules = projectProgrammingLanguageTechnologyRules;
            _projectRules = projectRules;
            _programmingLanguageTechnologyRules = programmingLanguageTechnologyRules;
        }

        public async Task<UpdatedProjectProgrammingLanguageTechnologyResponse> Handle(UpdateProjectProgrammingLanguageTechnologyCommand request, CancellationToken cancellationToken)
        {
            ProjectProgrammingLanguageTechnology? projectProgrammingLanguageTechnology = await _projectProgrammingLanguageTechnologyRepository.GetAsync(x => x.Id == request.Id);

            await _projectProgrammingLanguageTechnologyRules.ProjectProgrammingLanguageTechnologyShouldExistWhenRequested(request.Id);

            _mapper.Map(request, projectProgrammingLanguageTechnology);

            await _projectProgrammingLanguageTechnologyRules.ProjectProgrammingLanguageTechnologySConNotBeDuplicatedWhenUpdated(projectProgrammingLanguageTechnology);
            await _projectRules.ProjectShouldExistWhenRequested(request.ProjectId);
            await _programmingLanguageTechnologyRules.ProgrammingLanguageTechnologyShouldExistWhenRequested(request.ProgrammingLanguageTechnologyId);

            ProjectProgrammingLanguageTechnology updatedProjectProgrammingLanguageTechnology = await _projectProgrammingLanguageTechnologyRepository.UpdateAsync(projectProgrammingLanguageTechnology);
            UpdatedProjectProgrammingLanguageTechnologyResponse mappedUpdatedProjectProgrammingLanguageTechnologyDto = _mapper.Map<UpdatedProjectProgrammingLanguageTechnologyResponse>(updatedProjectProgrammingLanguageTechnology);

            return mappedUpdatedProjectProgrammingLanguageTechnologyDto;
        }
    }
}