using asari.com.tr.Application.Features.Projects.Rules;
using asari.com.tr.Application.Features.ProjectSkills.Constants;
using asari.com.tr.Application.Features.ProjectSkills.Rules;
using asari.com.tr.Application.Features.Skills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.ProjectSkills.Constants.ProjectSkillsOperationClaims;

namespace asari.com.tr.Application.Features.ProjectSkills.Commands.Update;

public class UpdateProjectSkillCommand : IRequest<UpdatedProjectSkillResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int SkillId { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { CacheGroupKeyValue.ProjectSkillCacheGroupKey };

    public string[] Roles => new[] { Admin, Write, ProjectSkillsOperationClaims.Update };

    public class UpdateProjectSkillCommandHandler : IRequestHandler<UpdateProjectSkillCommand, UpdatedProjectSkillResponse>
    {
        private readonly IProjectSkillRepository _projectSkillRepository;
        private readonly IMapper _mapper;
        private readonly ProjectSkillBusinessRules _projectSkillBusinessRules;
        private readonly ProjectBusinessRules _projectBusinessRules;
        private readonly SkillBusinessRules _skillBusinessRules;

        public UpdateProjectSkillCommandHandler(IProjectSkillRepository projectSkillRepository, IMapper mapper, ProjectSkillBusinessRules projectSkillBusinessRules, ProjectBusinessRules projectRules, SkillBusinessRules skillBusinessRules)
        {
            _projectSkillRepository = projectSkillRepository;
            _mapper = mapper;
            _projectSkillBusinessRules = projectSkillBusinessRules;
            _projectBusinessRules = projectRules;
            _skillBusinessRules = skillBusinessRules;
        }

        public async Task<UpdatedProjectSkillResponse> Handle(UpdateProjectSkillCommand request, CancellationToken cancellationToken)
        {
            ProjectSkill? projectSkill = await _projectSkillRepository.GetAsync(x => x.Id == request.Id);

            await _projectSkillBusinessRules.ProjectSkillShouldExistWhenRequested(request.Id);

            _mapper.Map(request, projectSkill);

            await _projectSkillBusinessRules.ProjectSkillTechnologySConNotBeDuplicatedWhenUpdated(projectSkill);
            await _projectBusinessRules.ProjectShouldExistWhenRequested(request.ProjectId);
            await _skillBusinessRules.SkillShouldExistWhenRequested(request.SkillId);

            ProjectSkill updatedProjectSkill = await _projectSkillRepository.UpdateAsync(projectSkill);
            UpdatedProjectSkillResponse mappedUpdatedProjectSkillResponse = _mapper.Map<UpdatedProjectSkillResponse>(updatedProjectSkill);

            return mappedUpdatedProjectSkillResponse;
        }
    }
}