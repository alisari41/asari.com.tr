using asari.com.tr.Application.Features.LicenseAndCertificationSkills.Rules;
using asari.com.tr.Application.Features.LicensesAndCertifications.Rules;
using asari.com.tr.Application.Features.Skills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.LicenseAndCertificationSkills.Constants.LicenseAndCertificationSkillsOperationClaims;

namespace asari.com.tr.Application.Features.LicenseAndCertificationSkills.Command.Create;

public class CreateLicenseAndCertificationSkillCommand : IRequest<CreatedLicenseAndCertificationSkillResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int LicenseAndCertificationId { get; set; }
    public int SkillId { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { CacheGroupKeyValue.LicenseAndCertificationSkillCacheGroupKey };

    public string[] Roles => new[] { Admin, Write, Add };

    public class CreateLicenseAndCertificationSkillCommandHandler : IRequestHandler<CreateLicenseAndCertificationSkillCommand, CreatedLicenseAndCertificationSkillResponse>
    {
        private readonly ILicenseAndCertificationSkillRepository _licenseAndCertificationSkillRepository;
        private readonly IMapper _mapper;
        private readonly LicenseAndCertificationSkillBusinessRules _licenseAndCertificationSkillBusinessRules;
        private readonly LicenseAndCertificationBusinessRules _licenseAndCertificationBusinessRules;
        private readonly SkillBusinessRules _skillBusinessRules;

        public CreateLicenseAndCertificationSkillCommandHandler(ILicenseAndCertificationSkillRepository licenseAndCertificationSkillRepository, IMapper mapper, LicenseAndCertificationSkillBusinessRules licenseAndCertificationSkillBusinessRules, LicenseAndCertificationBusinessRules licenseAndCertificationBusinessRules, SkillBusinessRules skillBusinessRules)
        {
            _licenseAndCertificationSkillRepository = licenseAndCertificationSkillRepository;
            _mapper = mapper;
            _licenseAndCertificationSkillBusinessRules = licenseAndCertificationSkillBusinessRules;
            _licenseAndCertificationBusinessRules = licenseAndCertificationBusinessRules;
            _skillBusinessRules = skillBusinessRules;
        }

        public async Task<CreatedLicenseAndCertificationSkillResponse> Handle(CreateLicenseAndCertificationSkillCommand request, CancellationToken cancellationToken)
        {
            await _licenseAndCertificationSkillBusinessRules.LicenseAndCertificationSkillConNotBeDuplicatedWhenInserted(request.LicenseAndCertificationId, request.SkillId);
            await _licenseAndCertificationBusinessRules.LicenseAndCertificationShouldExistWhenRequested(request.LicenseAndCertificationId);
            await _skillBusinessRules.SkillShouldExistWhenRequested(request.SkillId);

            LicenseAndCertificationSkill mappedLicenseAndCertificationSkill = _mapper.Map<LicenseAndCertificationSkill>(request);
            LicenseAndCertificationSkill createdLicenseAndCertificationSkill = await _licenseAndCertificationSkillRepository.AddAsync(mappedLicenseAndCertificationSkill);
            CreatedLicenseAndCertificationSkillResponse mappedCreatedLicenseAndCertificationSkillResponse = _mapper.Map<CreatedLicenseAndCertificationSkillResponse>(createdLicenseAndCertificationSkill);

            return mappedCreatedLicenseAndCertificationSkillResponse;
        }
    }
}