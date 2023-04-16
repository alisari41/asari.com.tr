using asari.com.tr.Application.Features.Experiences.Constants;
using asari.com.tr.Application.Features.ExperienceSkills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.ExperienceSkills.Constants.ExperienceSkillsOperationClaims;

namespace asari.com.tr.Application.Features.ExperienceSkills.Commands.Delete;

public class DeleteExperienceSkillCommand : IRequest<DeletedExperienceSkillResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string? CacheGroupKey => CacheGroupKeyValue.EducationSkillCacheGroupKey;

    public string[] Roles => new[] { Admin, Write, ExperiencesOperationClaims.Delete };

    public class DeleteExperienceSkillCommandHandler : IRequestHandler<DeleteExperienceSkillCommand, DeletedExperienceSkillResponse>
    {
        private readonly IExperienceSkillRepository _experienceSkillRepository;
        private readonly IMapper _mapper;
        private readonly ExperienceSkillBusinessRules _experienceSkillBusinessRules;

        public DeleteExperienceSkillCommandHandler(IExperienceSkillRepository experienceSkillRepository, IMapper mapper, ExperienceSkillBusinessRules experienceSkillBusinessRules)
        {
            _experienceSkillRepository = experienceSkillRepository;
            _mapper = mapper;
            _experienceSkillBusinessRules = experienceSkillBusinessRules;
        }

        public async Task<DeletedExperienceSkillResponse> Handle(DeleteExperienceSkillCommand request, CancellationToken cancellationToken)
        {
            ExperienceSkill? experienceSkill = await _experienceSkillRepository.GetAsync(x => x.Id == request.Id);

            _experienceSkillBusinessRules.ExperienceSkillShouldExistWhenRequested(experienceSkill);

            _mapper.Map(request, experienceSkill);
            ExperienceSkill deletedExperienceSkill = await _experienceSkillRepository.DeleteAsync(experienceSkill);
            DeletedExperienceSkillResponse mappedDeletedExperienceSkillResponse = _mapper.Map<DeletedExperienceSkillResponse>(deletedExperienceSkill);

            return mappedDeletedExperienceSkillResponse;
        }
    }
}