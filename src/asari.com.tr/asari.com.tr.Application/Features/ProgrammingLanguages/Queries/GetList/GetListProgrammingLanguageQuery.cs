using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;

namespace asari.com.tr.Application.Features.ProgrammingLanguages.Queries.GetList;

public class GetListProgrammingLanguageQuery : IRequest<GetListResponse<GetListProgrammingLanguageListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; } // Bir listeleme yapılacağı için bir Request üzerinden geçekleştirilecek

    public bool BypassCache { get; }
    public string CacheKey => $"GetListProgrammingLanguage({PageRequest.Page},{PageRequest.PageSize})";
    public string? CacheGroupKey => CacheGroupKeyValue.ProgrammingLanguageCacheGroupKey;

    public TimeSpan? SlidingExpiration { get; }

    public class GetListProgrammingLanguageQueryHandler : IRequestHandler<GetListProgrammingLanguageQuery, GetListResponse<GetListProgrammingLanguageListItemDto>>
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
        private readonly IMapper _mapper;

        public GetListProgrammingLanguageQueryHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListProgrammingLanguageListItemDto>> Handle(GetListProgrammingLanguageQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ProgrammingLanguage> programmingLanguages = await _programmingLanguageRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

            GetListResponse<GetListProgrammingLanguageListItemDto> mappedProgrammingLanguageListModel = _mapper.Map<GetListResponse<GetListProgrammingLanguageListItemDto>>(programmingLanguages);

            return mappedProgrammingLanguageListModel;
        }
    }
}