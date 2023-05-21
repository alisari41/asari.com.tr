using asari.com.tr.Application.Features.LicenseAndCertificationSkills.Constants;
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

namespace asari.com.tr.Application.Features.LicenseAndCertificationSkills.Command.Update;

public class UpdateLicenseAndCertificationSkillCommand : IRequest<UpdatedLicenseAndCertificationSkillResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }
    public int LicenseAndCertificationId { get; set; }
    public int SkillId { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { CacheGroupKeyValue.LicenseAndCertificationSkillCacheGroupKey };

    public string[] Roles => new[] { Admin, Write, LicenseAndCertificationSkillsOperationClaims.Update };

    public class UpdateLicenseAndCertificationSkillCommandHandler : IRequestHandler<UpdateLicenseAndCertificationSkillCommand, UpdatedLicenseAndCertificationSkillResponse>
    {
        private readonly ILicenseAndCertificationSkillRepository _licenseAndCertificationSkillRepository;
        private readonly IMapper _mapper;
        private readonly LicenseAndCertificationSkillBusinessRules _licenseAndCertificationSkillBusinessRules;
        private readonly LicenseAndCertificationBusinessRules _licenseAndCertificationBusinessRules;
        private readonly SkillBusinessRules _skillBusinessRules;

        public UpdateLicenseAndCertificationSkillCommandHandler(ILicenseAndCertificationSkillRepository licenseAndCertificationSkillRepository, IMapper mapper, LicenseAndCertificationSkillBusinessRules licenseAndCertificationSkillBusinessRules, LicenseAndCertificationBusinessRules licenseAndCertificationBusinessRules, SkillBusinessRules skillBusinessRules)
        {
            _licenseAndCertificationSkillRepository = licenseAndCertificationSkillRepository;
            _mapper = mapper;
            _licenseAndCertificationSkillBusinessRules = licenseAndCertificationSkillBusinessRules;
            _licenseAndCertificationBusinessRules = licenseAndCertificationBusinessRules;
            _skillBusinessRules = skillBusinessRules;
        }

        public async Task<UpdatedLicenseAndCertificationSkillResponse> Handle(UpdateLicenseAndCertificationSkillCommand request, CancellationToken cancellationToken)
        {
            LicenseAndCertificationSkill? licenseAndCertificationSkill = await _licenseAndCertificationSkillRepository.GetAsync(x => x.Id == request.Id);

            _licenseAndCertificationSkillBusinessRules.LicenseAndCertificationSkillShouldExistWhenRequested(licenseAndCertificationSkill);

            _mapper.Map(request, licenseAndCertificationSkill);

            await _licenseAndCertificationSkillBusinessRules.LicenseAndCertificationSkillConNotBeDuplicatedWhenUpdated(licenseAndCertificationSkill);
            await _licenseAndCertificationBusinessRules.LicenseAndCertificationShouldExistWhenRequested(request.LicenseAndCertificationId);
            await _skillBusinessRules.SkillShouldExistWhenRequested(request.SkillId);

            LicenseAndCertificationSkill updatedLicenseAndCertificationSkill = await _licenseAndCertificationSkillRepository.UpdateAsync(licenseAndCertificationSkill);
            UpdatedLicenseAndCertificationSkillResponse mappedUpdatedLicenseAndCertificationSkillResponse = _mapper.Map<UpdatedLicenseAndCertificationSkillResponse>(updatedLicenseAndCertificationSkill);

            return mappedUpdatedLicenseAndCertificationSkillResponse;
        }
    }
}