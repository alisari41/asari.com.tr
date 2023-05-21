using asari.com.tr.Application.Features.LicensesAndCertifications.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.LicensesAndCertifications.Queries.GetById;

public class GetByIdLicenseAndCertificationQuery : IRequest<GetByIdLicenseAndCertificationResponse>
{
    public int Id { get; set; }

    public class GetByIdLicenseAndCertificationQueryHandler : IRequestHandler<GetByIdLicenseAndCertificationQuery, GetByIdLicenseAndCertificationResponse>
    {
        private readonly ILicenseAndCertificationRepository _licenseAndCertificationRepository;
        private readonly IMapper _mapper;
        private readonly LicenseAndCertificationBusinessRules _licenseAndCertificationBusinessRules;

        public GetByIdLicenseAndCertificationQueryHandler(ILicenseAndCertificationRepository licenseAndCertificationRepository, IMapper mapper, LicenseAndCertificationBusinessRules licenseAndCertificationBusinessRules)
        {
            _licenseAndCertificationRepository = licenseAndCertificationRepository;
            _mapper = mapper;
            _licenseAndCertificationBusinessRules = licenseAndCertificationBusinessRules;
        }

        public async Task<GetByIdLicenseAndCertificationResponse> Handle(GetByIdLicenseAndCertificationQuery request, CancellationToken cancellationToken)
        {
            LicenseAndCertification? licenseAndCertification = await _licenseAndCertificationRepository.GetAsync(x => x.Id == request.Id);
            _licenseAndCertificationBusinessRules.LicenseAndCertificationShouldExistWhenRequested(licenseAndCertification);

            GetByIdLicenseAndCertificationResponse mappedGetByIdLicenseAndCertificationGetByIdResponse = _mapper.Map<GetByIdLicenseAndCertificationResponse>(licenseAndCertification);

            return mappedGetByIdLicenseAndCertificationGetByIdResponse;
        }
    }
}