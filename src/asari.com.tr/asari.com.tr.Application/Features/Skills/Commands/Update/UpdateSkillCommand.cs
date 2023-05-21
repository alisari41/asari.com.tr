using asari.com.tr.Application.Features.Skills.Constants;
using asari.com.tr.Application.Features.Skills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.Skills.Constants.SkillsOperationClaims;

namespace asari.com.tr.Application.Features.Skills.Commands.Update;

public class UpdateSkillCommand : IRequest<UpdatedSkillResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double? Degree { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { CacheGroupKeyValue.SkillCacheGroupKey };

    public string[] Roles => new[] { Admin, Write, SkillsOperationClaims.Update };

    public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand, UpdatedSkillResponse>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;
        private readonly SkillBusinessRules _skillBusinessRules;

        public UpdateSkillCommandHandler(ISkillRepository skillRepository, IMapper mapper, SkillBusinessRules skillBusinessRules)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
            _skillBusinessRules = skillBusinessRules;
        }

        public async Task<UpdatedSkillResponse> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
        {
            Skill? skill = await _skillRepository.GetAsync(x => x.Id == request.Id);

            await _skillBusinessRules.SkillShouldExistWhenRequested(request.Id);

            _mapper.Map(request, skill);
            await _skillBusinessRules.SkillTitleConNotBeDuplicatedWhenUpdated(skill);

            Skill updatedSkill = await _skillRepository.UpdateAsync(skill);
            UpdatedSkillResponse mappedUpdatedSkillResponse = _mapper.Map<UpdatedSkillResponse>(updatedSkill);

            return mappedUpdatedSkillResponse;
        }
    }
}