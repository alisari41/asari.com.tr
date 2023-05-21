using asari.com.tr.Application.Features.LicensesAndCertifications.Constants;
using asari.com.tr.Application.Features.LicensesAndCertifications.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.LicensesAndCertifications.Constants.LicensesAndCertificationsOperationClaims;

namespace asari.com.tr.Application.Features.LicensesAndCertifications.Commands.Delete;

public class DeleteLicenseAndCertificationCommand : IRequest<DeletedLicenseAndCertificationResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { CacheGroupKeyValue.LicensesAndCertificationCacheGroupKey };

    public string[] Roles => new[] { Admin, Write, LicensesAndCertificationsOperationClaims.Delete };

    public class DeleteLicenseAndCertificationCommandHandler : IRequestHandler<DeleteLicenseAndCertificationCommand, DeletedLicenseAndCertificationResponse>
    {
        private readonly ILicenseAndCertificationRepository _licenseAndCertificationRepository;
        private readonly IMapper _mapper;
        private readonly LicenseAndCertificationBusinessRules _licenseAndCertificationBusinessRules;

        public DeleteLicenseAndCertificationCommandHandler(ILicenseAndCertificationRepository licenseAndCertificationRepository, IMapper mapper, LicenseAndCertificationBusinessRules licenseAndCertificationBusinessRules)
        {
            _licenseAndCertificationRepository = licenseAndCertificationRepository;
            _mapper = mapper;
            _licenseAndCertificationBusinessRules = licenseAndCertificationBusinessRules;
        }

        public async Task<DeletedLicenseAndCertificationResponse> Handle(DeleteLicenseAndCertificationCommand request, CancellationToken cancellationToken)
        {
            LicenseAndCertification? licenseAndCertification = await _licenseAndCertificationRepository.GetAsync(x => x.Id == request.Id);

            _licenseAndCertificationBusinessRules.LicenseAndCertificationShouldExistWhenRequested(licenseAndCertification);

            _mapper.Map(request, licenseAndCertification);
            LicenseAndCertification deletedLicenseAndCertification = await _licenseAndCertificationRepository.DeleteAsync(licenseAndCertification);
            DeletedLicenseAndCertificationResponse mappedDeletedLicenseAndCertificationResponse = _mapper.Map<DeletedLicenseAndCertificationResponse>(deletedLicenseAndCertification);

            return mappedDeletedLicenseAndCertificationResponse;
        }
    }
}