using asari.com.tr.Application.Features.Skills.Constants;
using asari.com.tr.Application.Features.Skills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.Skills.Constants.SkillsOperationClaims;

namespace asari.com.tr.Application.Features.Skills.Commands.Delete;

public class DeleteSkillCommand : IRequest<DeletedSkillResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int? Id { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { CacheGroupKeyValue.SkillCacheGroupKey };

    public string[] Roles => new[] { Admin, Write, SkillsOperationClaims.Delete };

    public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, DeletedSkillResponse>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;
        private readonly SkillBusinessRules _skillBusinessRules;

        public DeleteSkillCommandHandler(ISkillRepository skillRepository, IMapper mapper, SkillBusinessRules skillBusinessRules)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
            _skillBusinessRules = skillBusinessRules;
        }

        public async Task<DeletedSkillResponse> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
        {
            Skill? skill = await _skillRepository.GetAsync(x => x.Id == request.Id);

            await _skillBusinessRules.SkillShouldExistWhenRequested(request.Id);

            _mapper.Map(request, skill);

            Skill deletedSkill = await _skillRepository.DeleteAsync(skill);
            DeletedSkillResponse mappedDeletedSkillResponse = _mapper.Map<DeletedSkillResponse>(deletedSkill);

            return mappedDeletedSkillResponse;
        }
    }
}