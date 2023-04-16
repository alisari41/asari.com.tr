using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Rules;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Rules;
using asari.com.tr.Application.Features.Projects.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Constants.ProjectProgrammingLanguageTechnologiesOperationClaims;

namespace asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Commands.Create;

public class CreateProjectProgrammingLanguageTechnologyCommand : IRequest<CreatedProjectProgrammingLanguageTechnologyResponse>, ISecuredRequest, ICacheRemoverRequest
{
    // Son kullanıcının bize göndereceği son dataları içeren yapı
    public int ProjectId { get; set; }
    public int ProgrammingLanguageTechnologyId { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string? CacheGroupKey => CacheGroupKeyValue.ProjectProgrammingLanguageTechnologyCacheGroupKey;

    public string[] Roles => new[] { Admin, Write, Add };

    public class CreateProjectProgrammingLanguageTechnologyCommandHandler : IRequestHandler<CreateProjectProgrammingLanguageTechnologyCommand, CreatedProjectProgrammingLanguageTechnologyResponse>
    {
        private readonly IProjectProgrammingLanguageTechnologyRepository _projectProgrammingLanguageTechnologyRepository;
        private readonly IMapper _mapper;
        private readonly ProjectProgrammingLanguageTechnologyBusinessRules _projectProgrammingLanguageTechnologyRules;
        private readonly ProjectBusinessRules _projectRules;
        private readonly ProgrammingLanguageTechnologyBusinessRules _programmingLanguageTechnologyRules;

        public CreateProjectProgrammingLanguageTechnologyCommandHandler(IProjectProgrammingLanguageTechnologyRepository projectProgrammingLanguageTechnologyRepository, IMapper mapper, ProjectProgrammingLanguageTechnologyBusinessRules projectProgrammingLanguageTechnologyRules, ProjectBusinessRules projectRules, ProgrammingLanguageTechnologyBusinessRules programmingLanguageTechnologyRules)
        {
            _projectProgrammingLanguageTechnologyRepository = projectProgrammingLanguageTechnologyRepository;
            _mapper = mapper;
            _projectProgrammingLanguageTechnologyRules = projectProgrammingLanguageTechnologyRules;
            _projectRules = projectRules;
            _programmingLanguageTechnologyRules = programmingLanguageTechnologyRules;
        }

        public async Task<CreatedProjectProgrammingLanguageTechnologyResponse> Handle(CreateProjectProgrammingLanguageTechnologyCommand request, CancellationToken cancellationToken)
        {
            await _projectProgrammingLanguageTechnologyRules.ProjectProgrammingLanguageTechnologySConNotBeDuplicatedWhenInserted(request.ProgrammingLanguageTechnologyId, request.ProjectId);
            await _projectRules.ProjectShouldExistWhenRequested(request.ProjectId);
            await _programmingLanguageTechnologyRules.ProgrammingLanguageTechnologyShouldExistWhenRequested(request.ProgrammingLanguageTechnologyId);

            ProjectProgrammingLanguageTechnology mappedProjectProgrammingLanguageTechnology = _mapper.Map<ProjectProgrammingLanguageTechnology>(request);
            ProjectProgrammingLanguageTechnology createdProjectProgrammingLanguageTechnology = await _projectProgrammingLanguageTechnologyRepository.AddAsync(mappedProjectProgrammingLanguageTechnology);
            CreatedProjectProgrammingLanguageTechnologyResponse mappedCreatedProjectProgrammingLanguageTechnologyResponse = _mapper.Map<CreatedProjectProgrammingLanguageTechnologyResponse>(createdProjectProgrammingLanguageTechnology);

            return mappedCreatedProjectProgrammingLanguageTechnologyResponse;
        }
    }
}