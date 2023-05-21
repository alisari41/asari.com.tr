using asari.com.tr.Application.Features.Skills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.Skills.Constants.SkillsOperationClaims;

namespace asari.com.tr.Application.Features.Skills.Commands.Create;

public class CreateSkillCommand : IRequest<CreatedSkillResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public string Name { get; set; }
    public double? Degree { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { CacheGroupKeyValue.SkillCacheGroupKey };

    public string[] Roles => new[] { Admin, Write, Add };

    public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, CreatedSkillResponse>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;
        private readonly SkillBusinessRules _skillBusinessRules;

        public CreateSkillCommandHandler(ISkillRepository skillRepository, IMapper mapper, SkillBusinessRules skillBusinessRules)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
            _skillBusinessRules = skillBusinessRules;
        }

        public async Task<CreatedSkillResponse> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            await _skillBusinessRules.SkillTitleConNotBeDuplicatedWhenInserted(request.Name);

            Skill mappedSkill = _mapper.Map<Skill>(request);
            Skill createdSkill = await _skillRepository.AddAsync(mappedSkill);
            CreatedSkillResponse mappedCreatedSkillResponse = _mapper.Map<CreatedSkillResponse>(createdSkill);

            return mappedCreatedSkillResponse;
        }
    }
}