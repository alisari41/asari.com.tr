using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.LicenseAndCertificationSkills.Queries.GetList;

public class GetListLicenseAndCertificationSkillQuery : IRequest<GetListResponse<GetListLicenseAndCertificationSkillListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; } // Bir listeleme yapılacağı için bir Request üzerinden geçekleştirilecek

    public bool BypassCache { get; }
    public string CacheKey => $"GetListLicenseAndCertificationSkill({PageRequest.Page},{PageRequest.PageSize})";
    public string? CacheGroupKey => CacheGroupKeyValue.LicenseAndCertificationSkillCacheGroupKey;

    public TimeSpan? SlidingExpiration { get; }

    public class GetListLicenseAndCertificationSkillQueryHandler : IRequestHandler<GetListLicenseAndCertificationSkillQuery, GetListResponse<GetListLicenseAndCertificationSkillListItemDto>>
    {
        private readonly ILicenseAndCertificationSkillRepository _licenseAndCertificationSkillRepository;
        private readonly IMapper _mapper;

        public GetListLicenseAndCertificationSkillQueryHandler(ILicenseAndCertificationSkillRepository licenseAndCertificationSkillRepository, IMapper mapper)
        {
            _licenseAndCertificationSkillRepository = licenseAndCertificationSkillRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListLicenseAndCertificationSkillListItemDto>> Handle(GetListLicenseAndCertificationSkillQuery request, CancellationToken cancellationToken)
        {
            IPaginate<LicenseAndCertificationSkill> licenseAndCertificationSkill = await _licenseAndCertificationSkillRepository.GetListAsync(orderBy: x =>
                                                                                                        x.Include(c => c.LicenseAndCertification)
                                                                                                         .Include(c => c.Skill)
                                                                                                         .OrderBy(c => c.LicenseAndCertification.Name),
                                                                                                        index: request.PageRequest.Page,
                                                                                                        size: request.PageRequest.PageSize);

            GetListResponse<GetListLicenseAndCertificationSkillListItemDto> mappedGetListLicenseAndCertificationSkillListItemDto = _mapper.Map<GetListResponse<GetListLicenseAndCertificationSkillListItemDto>>(licenseAndCertificationSkill);

            return mappedGetListLicenseAndCertificationSkillListItemDto;
        }
    }
}