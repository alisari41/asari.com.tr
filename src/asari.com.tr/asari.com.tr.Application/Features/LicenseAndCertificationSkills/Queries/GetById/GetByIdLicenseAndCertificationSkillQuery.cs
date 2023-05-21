using asari.com.tr.Application.Features.LicenseAndCertificationSkills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.LicenseAndCertificationSkills.Queries.GetById;

public class GetByIdLicenseAndCertificationSkillQuery : IRequest<GetByIdLicenseAndCertificationSkillResponse>
{
    public int Id { get; set; }

    public class GetByIdLicenseAndCertificationSkillQueryHandler : IRequestHandler<GetByIdLicenseAndCertificationSkillQuery, GetByIdLicenseAndCertificationSkillResponse>
    {
        private readonly ILicenseAndCertificationSkillRepository _licenseAndCertificationSkillRepository;
        private readonly IMapper _mapper;
        private readonly LicenseAndCertificationSkillBusinessRules _licenseAndCertificationSkillBusinessRules;

        public GetByIdLicenseAndCertificationSkillQueryHandler(ILicenseAndCertificationSkillRepository licenseAndCertificationSkillRepository, IMapper mapper, LicenseAndCertificationSkillBusinessRules licenseAndCertificationSkillBusinessRules)
        {
            _licenseAndCertificationSkillRepository = licenseAndCertificationSkillRepository;
            _mapper = mapper;
            _licenseAndCertificationSkillBusinessRules = licenseAndCertificationSkillBusinessRules;
        }

        public async Task<GetByIdLicenseAndCertificationSkillResponse> Handle(GetByIdLicenseAndCertificationSkillQuery request, CancellationToken cancellationToken)
        {
            LicenseAndCertificationSkill? LicenseAndCertificationSkill = await _licenseAndCertificationSkillRepository.GetAsync(x => x.Id == request.Id,
                                                                                       include: i =>
                                                                                            i.Include(c => c.LicenseAndCertification)
                                                                                             .Include(c => c.Skill));

            _licenseAndCertificationSkillBusinessRules.LicenseAndCertificationSkillShouldExistWhenRequested(LicenseAndCertificationSkill);

            GetByIdLicenseAndCertificationSkillResponse mappedGetByIdLicenseAndCertificationSkillGetByIdResponse = _mapper.Map<GetByIdLicenseAndCertificationSkillResponse>(LicenseAndCertificationSkill);

            return mappedGetByIdLicenseAndCertificationSkillGetByIdResponse;
        }
    }
}