using asari.com.tr.Application.Features.LicensesAndCertifications.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.LicensesAndCertifications.Commands.Create;

public class CreateLicenseAndCertificationCommand : IRequest<CreatedLicenseAndCertificationResponse>
{
    public string Name { get; set; }
    public string IssuingOrganization { get; set; }
    public DateTime? IssueDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? ImagegUrl { get; set; }
    public string? CredentialId { get; set; }
    public string? CredentialUrl { get; set; }

    public class CreateLicenseAndCertificationCommandHandler : IRequestHandler<CreateLicenseAndCertificationCommand, CreatedLicenseAndCertificationResponse>
    {
        private readonly ILicenseAndCertificationRepository _licenseAndCertificationRepository;
        private readonly IMapper _mapper;
        private readonly LicenseAndCertificationBusinessRules _licenseAndCertificationBusinessRules;

        public CreateLicenseAndCertificationCommandHandler(ILicenseAndCertificationRepository licenseAndCertificationRepository, IMapper mapper, LicenseAndCertificationBusinessRules licenseAndCertificationBusinessRules)
        {
            _licenseAndCertificationRepository = licenseAndCertificationRepository;
            _mapper = mapper;
            _licenseAndCertificationBusinessRules = licenseAndCertificationBusinessRules;
        }

        public async Task<CreatedLicenseAndCertificationResponse> Handle(CreateLicenseAndCertificationCommand request, CancellationToken cancellationToken)
        {
            await _licenseAndCertificationBusinessRules.LicenseAndCertificationTitleConNotBeDuplicatedWhenInserted(request.Name);

            LicenseAndCertification licenseAndCertification = _mapper.Map<LicenseAndCertification>(request);
            LicenseAndCertification createdLicenseAndCertification=await _licenseAndCertificationRepository.AddAsync(licenseAndCertification);
            CreatedLicenseAndCertificationResponse mappedCreatedLicenseAndCertificationResponse=_mapper.Map<CreatedLicenseAndCertificationResponse>(createdLicenseAndCertification);

            return mappedCreatedLicenseAndCertificationResponse;
        }
    }
}