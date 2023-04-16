using asari.com.tr.Application.Features.EducationSkills.Constants;
using asari.com.tr.Application.Features.EducationSkills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.EducationSkills.Constants.EducationSkillsOperationClaims;

namespace asari.com.tr.Application.Features.EducationSkills.Commands.Delete;

public class DeleteEducationSkillCommand : IRequest<DeletedEducationSkillResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string? CacheGroupKey => CacheGroupKeyValue.EducationSkillCacheGroupKey;

    public string[] Roles => new[] { Admin, Write, EducationSkillsOperationClaims.Delete };

    public class DeleteEducationSkillCommandHandler : IRequestHandler<DeleteEducationSkillCommand, DeletedEducationSkillResponse>
    {
        private readonly IEducationSkillRepository _educationSkillRepository;
        private readonly IMapper _mapper;
        private readonly EducationSkillBusinessRules _educationSkillBusinessRules;

        public DeleteEducationSkillCommandHandler(IEducationSkillRepository educationSkillRepository, IMapper mapper, EducationSkillBusinessRules educationSkillBusinessRules)
        {
            _educationSkillRepository = educationSkillRepository;
            _mapper = mapper;
            _educationSkillBusinessRules = educationSkillBusinessRules;
        }

        public async Task<DeletedEducationSkillResponse> Handle(DeleteEducationSkillCommand request, CancellationToken cancellationToken)
        {
            EducationSkill? educationSkill = await _educationSkillRepository.GetAsync(x => x.Id == request.Id);

            _educationSkillBusinessRules.EducationSkillShouldExistWhenRequested(educationSkill);

            _mapper.Map(request, educationSkill);

            EducationSkill deletedEducationSkill = await _educationSkillRepository.DeleteAsync(educationSkill);
            DeletedEducationSkillResponse mappedDeletedEducationSkillReponse = _mapper.Map<DeletedEducationSkillResponse>(deletedEducationSkill);

            return mappedDeletedEducationSkillReponse;
        }
    }
}