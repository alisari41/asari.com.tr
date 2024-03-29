﻿using asari.com.tr.Application.Features.Projects.Rules;
using asari.com.tr.Application.Features.ProjectSkills.Rules;
using asari.com.tr.Application.Features.Skills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.ProjectSkills.Constants.ProjectSkillsOperationClaims;

namespace asari.com.tr.Application.Features.ProjectSkills.Commands.Create;

public class CreateProjectSkillCommand : IRequest<CreatedProjectSkillResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int ProjectId { get; set; }
    public int SkillId { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { CacheGroupKeyValue.ProjectSkillCacheGroupKey };

    public string[] Roles => new[] { Admin, Write, Add };

    public class CreateProjectSkillCommandHandler : IRequestHandler<CreateProjectSkillCommand, CreatedProjectSkillResponse>
    {
        private readonly IProjectSkillRepository _projectSkillRepository;
        private readonly IMapper _mapper;
        private readonly ProjectSkillBusinessRules _projectSkillBusinessRules;
        private readonly ProjectBusinessRules _projectBusinessRules;
        private readonly SkillBusinessRules _skillBusinessRules;

        public CreateProjectSkillCommandHandler(IProjectSkillRepository projectSkillRepository, IMapper mapper, ProjectSkillBusinessRules projectSkillBusinessRules, ProjectBusinessRules projectRules, SkillBusinessRules skillBusinessRules)
        {
            _projectSkillRepository = projectSkillRepository;
            _mapper = mapper;
            _projectSkillBusinessRules = projectSkillBusinessRules;
            _projectBusinessRules = projectRules;
            _skillBusinessRules = skillBusinessRules;
        }

        public async Task<CreatedProjectSkillResponse> Handle(CreateProjectSkillCommand request, CancellationToken cancellationToken)
        {
            await _projectSkillBusinessRules.ProjectSkillSConNotBeDuplicatedWhenInserted(request.ProjectId, request.SkillId);
            await _projectBusinessRules.ProjectShouldExistWhenRequested(request.ProjectId);
            await _skillBusinessRules.SkillShouldExistWhenRequested(request.SkillId);

            ProjectSkill mappedProjectSkill = _mapper.Map<ProjectSkill>(request);
            ProjectSkill createdProjectSkill = await _projectSkillRepository.AddAsync(mappedProjectSkill);
            CreatedProjectSkillResponse mappedCreatedProjectSkillResponse = _mapper.Map<CreatedProjectSkillResponse>(createdProjectSkill);

            return mappedCreatedProjectSkillResponse;
        }
    }
}