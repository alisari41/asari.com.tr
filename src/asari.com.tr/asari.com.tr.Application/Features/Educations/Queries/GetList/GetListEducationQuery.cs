using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;

namespace asari.com.tr.Application.Features.Educations.Queries.GetList;

public class GetListEducationQuery : IRequest<GetListResponse<GetListEducationListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; } // Bir listeleme yapılacağı için bir Request üzerinden geçekleştirilecek

    public bool BypassCache { get; }
    public string CacheKey => $"GetListEducation({PageRequest.Page},{PageRequest.PageSize})";
    public string? CacheGroupKey => CacheGroupKeyValue.EducationCacheGroupKey;

    public TimeSpan? SlidingExpiration { get; }

    public class GetListEducationQueryHandler : IRequestHandler<GetListEducationQuery, GetListResponse<GetListEducationListItemDto>>
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IMapper _mapper;

        public GetListEducationQueryHandler(IEducationRepository educationRepository, IMapper mapper)
        {
            _educationRepository = educationRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListEducationListItemDto>> Handle(GetListEducationQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Education> education = await _educationRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

            GetListResponse<GetListEducationListItemDto> mappedGetListEducationListItemDto = _mapper.Map<GetListResponse<GetListEducationListItemDto>>(education);

            return mappedGetListEducationListItemDto;
        }
    }
}