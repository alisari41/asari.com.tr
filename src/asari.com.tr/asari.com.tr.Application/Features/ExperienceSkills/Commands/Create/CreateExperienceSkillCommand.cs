using asari.com.tr.Application.Features.Experiences.Rules;
using asari.com.tr.Application.Features.ExperienceSkills.Rules;
using asari.com.tr.Application.Features.Skills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.ExperienceSkills.Constants.ExperienceSkillsOperationClaims;

namespace asari.com.tr.Application.Features.ExperienceSkills.Commands.Create;

public class CreateExperienceSkillCommand : IRequest<CreatedExperienceSkillResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int ExperienceId { get; set; }
    public int SkillId { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { CacheGroupKeyValue.EducationSkillCacheGroupKey };

    public string[] Roles => new[] { Admin, Write, Add };

    public class CreateExperienceSkillCommandHandler : IRequestHandler<CreateExperienceSkillCommand, CreatedExperienceSkillResponse>
    {
        private readonly IExperienceSkillRepository _experienceSkillRepository;
        private readonly IMapper _mapper;
        private readonly ExperienceSkillBusinessRules _experienceSkillBusinessRules;
        private readonly ExperienceBusinessRules _experienceBusinessRules;
        private readonly SkillBusinessRules _skillBusinessRules;

        public CreateExperienceSkillCommandHandler(IExperienceSkillRepository experienceSkillRepository, IMapper mapper, ExperienceSkillBusinessRules experienceSkillBusinessRules, ExperienceBusinessRules experienceBusinessRules, SkillBusinessRules skillBusinessRules)
        {
            _experienceSkillRepository = experienceSkillRepository;
            _mapper = mapper;
            _experienceSkillBusinessRules = experienceSkillBusinessRules;
            _experienceBusinessRules = experienceBusinessRules;
            _skillBusinessRules = skillBusinessRules;
        }

        public async Task<CreatedExperienceSkillResponse> Handle(CreateExperienceSkillCommand request, CancellationToken cancellationToken)
        {
            await _experienceSkillBusinessRules.ExperienceSkillConNotBeDuplicatedWhenInserted(request.ExperienceId, request.SkillId);
            await _experienceBusinessRules.ExperienceShouldExistWhenRequested(request.ExperienceId);
            await _skillBusinessRules.SkillShouldExistWhenRequested(request.SkillId);

            ExperienceSkill mappedExperienceSkill = _mapper.Map<ExperienceSkill>(request);
            ExperienceSkill createdExperienceSkill = await _experienceSkillRepository.AddAsync(mappedExperienceSkill);
            CreatedExperienceSkillResponse mappedCreatedExperienceSkillResponse = _mapper.Map<CreatedExperienceSkillResponse>(createdExperienceSkill);

            return mappedCreatedExperienceSkillResponse;
        }
    }
}