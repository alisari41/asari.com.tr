using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Constants;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Constants.ProjectProgrammingLanguageTechnologiesOperationClaims;

namespace asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Commands.Delete;

public class DeleteProjectProgrammingLanguageTechnologyCommand : IRequest<DeletedProjectProgrammingLanguageTechnologyResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string? CacheGroupKey => CacheGroupKeyValue.ProjectProgrammingLanguageTechnologyCacheGroupKey;

    public string[] Roles => new[] { Admin, Write, ProjectProgrammingLanguageTechnologiesOperationClaims.Delete };

    public class DeleteProjectProgrammingLanguageTechnologyCommandHandler : IRequestHandler<DeleteProjectProgrammingLanguageTechnologyCommand, DeletedProjectProgrammingLanguageTechnologyResponse>
    {

        private readonly IProjectProgrammingLanguageTechnologyRepository _projectProgrammingLanguageTechnologyRepository;
        private readonly IMapper _mapper;
        private readonly ProjectProgrammingLanguageTechnologyBusinessRules _projectProgrammingLanguageTechnologyRules;

        public DeleteProjectProgrammingLanguageTechnologyCommandHandler(IProjectProgrammingLanguageTechnologyRepository projectProgrammingLanguageTechnologyRepository, IMapper mapper, ProjectProgrammingLanguageTechnologyBusinessRules projectProgrammingLanguageTechnologyRules)
        {
            _projectProgrammingLanguageTechnologyRepository = projectProgrammingLanguageTechnologyRepository;
            _mapper = mapper;
            _projectProgrammingLanguageTechnologyRules = projectProgrammingLanguageTechnologyRules;
        }

        public async Task<DeletedProjectProgrammingLanguageTechnologyResponse> Handle(DeleteProjectProgrammingLanguageTechnologyCommand request, CancellationToken cancellationToken)
        {
            ProjectProgrammingLanguageTechnology? projectProgrammingLanguageTechnology = await _projectProgrammingLanguageTechnologyRepository.GetAsync(x => x.Id == request.Id);

            await _projectProgrammingLanguageTechnologyRules.ProjectProgrammingLanguageTechnologyShouldExistWhenRequested(request.Id);

            _mapper.Map(request, projectProgrammingLanguageTechnology);
            ProjectProgrammingLanguageTechnology deletedProjectProgrammingLanguageTechnology = await _projectProgrammingLanguageTechnologyRepository.DeleteAsync(projectProgrammingLanguageTechnology);
            DeletedProjectProgrammingLanguageTechnologyResponse mappedDeletedProjectProgrammingLanguageTechnologyDto = _mapper.Map<DeletedProjectProgrammingLanguageTechnologyResponse>(deletedProjectProgrammingLanguageTechnology);

            return mappedDeletedProjectProgrammingLanguageTechnologyDto;
        }
    }
}