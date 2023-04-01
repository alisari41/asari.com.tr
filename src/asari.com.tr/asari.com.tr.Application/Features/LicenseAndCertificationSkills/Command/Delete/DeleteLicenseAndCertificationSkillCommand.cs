using asari.com.tr.Application.Features.LicenseAndCertificationSkills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.LicenseAndCertificationSkills.Command.Delete;

public class DeleteLicenseAndCertificationSkillCommand : IRequest<DeletedLicenseAndCertificationSkillResponse>
{
    public int Id { get; set; }

    public class DeleteLicenseAndCertificationSkillCommandHandler : IRequestHandler<DeleteLicenseAndCertificationSkillCommand, DeletedLicenseAndCertificationSkillResponse>
    {
        private readonly ILicenseAndCertificationSkillRepository _licenseAndCertificationSkillRepository;
        private readonly IMapper _mapper;
        private readonly LicenseAndCertificationSkillBusinessRules _licenseAndCertificationSkillBusinessRules;

        public DeleteLicenseAndCertificationSkillCommandHandler(ILicenseAndCertificationSkillRepository licenseAndCertificationSkillRepository, IMapper mapper, LicenseAndCertificationSkillBusinessRules licenseAndCertificationSkillBusinessRules)
        {
            _licenseAndCertificationSkillRepository = licenseAndCertificationSkillRepository;
            _mapper = mapper;
            _licenseAndCertificationSkillBusinessRules = licenseAndCertificationSkillBusinessRules;
        }

        public async Task<DeletedLicenseAndCertificationSkillResponse> Handle(DeleteLicenseAndCertificationSkillCommand request, CancellationToken cancellationToken)
        {
            LicenseAndCertificationSkill? licenseAndCertificationSkill = await _licenseAndCertificationSkillRepository.GetAsync(x => x.Id == request.Id);

            _licenseAndCertificationSkillBusinessRules.LicenseAndCertificationSkillShouldExistWhenRequested(licenseAndCertificationSkill);

            _mapper.Map(request, licenseAndCertificationSkill);
            LicenseAndCertificationSkill deletedLicenseAndCertificationSkill = await _licenseAndCertificationSkillRepository.DeleteAsync(licenseAndCertificationSkill);
            DeletedLicenseAndCertificationSkillResponse mappedDeletedLicenseAndCertificationSkillResponse = _mapper.Map<DeletedLicenseAndCertificationSkillResponse>(deletedLicenseAndCertificationSkill);

            return mappedDeletedLicenseAndCertificationSkillResponse;
        }
    }
}