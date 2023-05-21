using asari.com.tr.Application.Features.LicensesAndCertifications.Constants;
using asari.com.tr.Application.Features.LicensesAndCertifications.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.LicensesAndCertifications.Constants.LicensesAndCertificationsOperationClaims;

namespace asari.com.tr.Application.Features.LicensesAndCertifications.Commands.Update;

public class UpdateLicenseAndCertificationCommand : IRequest<UpdatedLicenseAndCertificationResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string IssuingOrganization { get; set; }
    public DateTime? IssueDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? ImagegUrl { get; set; }
    public string? CredentialId { get; set; }
    public string? CredentialUrl { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { CacheGroupKeyValue.LicensesAndCertificationCacheGroupKey };

    public string[] Roles => new[] { Admin, Write, LicensesAndCertificationsOperationClaims.Update };

    public class UpdateLicenseAndCertificationCommandHandler : IRequestHandler<UpdateLicenseAndCertificationCommand, UpdatedLicenseAndCertificationResponse>
    {
        private readonly ILicenseAndCertificationRepository _licenseAndCertificationRepository;
        private readonly IMapper _mapper;
        private readonly LicenseAndCertificationBusinessRules _licenseAndCertificationBusinessRules;

        public UpdateLicenseAndCertificationCommandHandler(ILicenseAndCertificationRepository licenseAndCertificationRepository, IMapper mapper, LicenseAndCertificationBusinessRules licenseAndCertificationBusinessRules)
        {
            _licenseAndCertificationRepository = licenseAndCertificationRepository;
            _mapper = mapper;
            _licenseAndCertificationBusinessRules = licenseAndCertificationBusinessRules;
        }

        public async Task<UpdatedLicenseAndCertificationResponse> Handle(UpdateLicenseAndCertificationCommand request, CancellationToken cancellationToken)
        {
            LicenseAndCertification? licenseAndCertification = await _licenseAndCertificationRepository.GetAsync(x => x.Id == request.Id);

            _licenseAndCertificationBusinessRules.LicenseAndCertificationShouldExistWhenRequested(licenseAndCertification);

            _mapper.Map(request, licenseAndCertification);

            await _licenseAndCertificationBusinessRules.LicenseAndCertificationTitleConNotBeDuplicatedWhenUpdated(licenseAndCertification);

            LicenseAndCertification updatedLicenseAndCertification = await _licenseAndCertificationRepository.UpdateAsync(licenseAndCertification);
            UpdatedLicenseAndCertificationResponse mappedUpdatedLicenseAndCertificationResponse = _mapper.Map<UpdatedLicenseAndCertificationResponse>(updatedLicenseAndCertification);

            return mappedUpdatedLicenseAndCertificationResponse;
        }
    }
}