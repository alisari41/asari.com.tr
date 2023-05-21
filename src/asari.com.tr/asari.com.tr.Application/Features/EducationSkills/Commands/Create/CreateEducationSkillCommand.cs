using asari.com.tr.Application.Features.Educations.Rules;
using asari.com.tr.Application.Features.EducationSkills.Rules;
using asari.com.tr.Application.Features.Skills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.EducationSkills.Constants.EducationSkillsOperationClaims;


namespace asari.com.tr.Application.Features.EducationSkills.Commands.Create;

public class CreateEducationSkillCommand : IRequest<CreatedEducationSkillResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int EducationId { get; set; }
    public int SkillId { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { CacheGroupKeyValue.EducationSkillCacheGroupKey };

    public string[] Roles => new[] { Admin, Write, Add };

    public class CreateEducationSkillCommandHandler : IRequestHandler<CreateEducationSkillCommand, CreatedEducationSkillResponse>
    {
        private readonly IEducationSkillRepository _educationSkillRepository;
        private readonly IMapper _mapper;
        private readonly EducationSkillBusinessRules _educationSkillBusinessRules;
        private readonly EducationBusinessRules _educationBusinessRules;
        private readonly SkillBusinessRules _skillBusinessRules;

        public CreateEducationSkillCommandHandler(IEducationSkillRepository educationSkillRepository, IMapper mapper, EducationSkillBusinessRules educationSkillBusinessRules, EducationBusinessRules educationBusinessRules, SkillBusinessRules skillBusinessRules)
        {
            _educationSkillRepository = educationSkillRepository;
            _mapper = mapper;
            _educationSkillBusinessRules = educationSkillBusinessRules;
            _educationBusinessRules = educationBusinessRules;
            _skillBusinessRules = skillBusinessRules;
        }

        public async Task<CreatedEducationSkillResponse> Handle(CreateEducationSkillCommand request, CancellationToken cancellationToken)
        {
            await _educationSkillBusinessRules.EducationSkillConNotBeDuplicatedWhenInserted(request.EducationId, request.SkillId);
            await _educationBusinessRules.EducationShouldExistWhenRequested(request.EducationId);
            await _skillBusinessRules.SkillShouldExistWhenRequested(request.SkillId);

            EducationSkill mappedEducationSkill = _mapper.Map<EducationSkill>(request);
            EducationSkill createdEducationSkill = await _educationSkillRepository.AddAsync(mappedEducationSkill);
            CreatedEducationSkillResponse mappedCreatedEducationSkillResponse = _mapper.Map<CreatedEducationSkillResponse>(createdEducationSkill);

            return mappedCreatedEducationSkillResponse;
        }
    }
}