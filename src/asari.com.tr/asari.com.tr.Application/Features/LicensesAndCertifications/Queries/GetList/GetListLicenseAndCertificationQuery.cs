using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;

namespace asari.com.tr.Application.Features.LicensesAndCertifications.Queries.GetList;

public class GetListLicenseAndCertificationQuery : IRequest<GetListResponse<GetListLicenseAndCertificationListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; } // Bir listeleme yapılacağı için bir Request üzerinden geçekleştirilecek

    public bool BypassCache { get; }
    public string CacheKey => $"GetListLicensesAndCertification({PageRequest.Page},{PageRequest.PageSize})";
    public string? CacheGroupKey => CacheGroupKeyValue.LicensesAndCertificationCacheGroupKey;

    public TimeSpan? SlidingExpiration { get; }

    public class GetListLicenseAndCertificationQueryHandler : IRequestHandler<GetListLicenseAndCertificationQuery, GetListResponse<GetListLicenseAndCertificationListItemDto>>
    {
        private readonly ILicenseAndCertificationRepository _licenseAndCertificationRepository;
        private readonly IMapper _mapper;

        public GetListLicenseAndCertificationQueryHandler(ILicenseAndCertificationRepository licenseAndCertificationRepository, IMapper mapper)
        {
            _licenseAndCertificationRepository = licenseAndCertificationRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListLicenseAndCertificationListItemDto>> Handle(GetListLicenseAndCertificationQuery request, CancellationToken cancellationToken)
        {
            IPaginate<LicenseAndCertification> licenseAndCertification = await _licenseAndCertificationRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

            GetListResponse<GetListLicenseAndCertificationListItemDto> mappedGetListLicenseAndCertificationListItemDto = _mapper.Map<GetListResponse<GetListLicenseAndCertificationListItemDto>>(licenseAndCertification);

            return mappedGetListLicenseAndCertificationListItemDto;
        }
    }
}