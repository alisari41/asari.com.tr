using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Queries.GetList;

public class GetListProgrammingLanguageTechnologyQuery : IRequest<GetListResponse<GetListProgrammingLanguageTechnologyListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; } // Bir listeleme yapılacağı için bir Request üzerinden geçekleştirilecek

    public bool BypassCache { get; }
    public string CacheKey => $"GetListProgrammingLanguageTechnology({PageRequest.Page},{PageRequest.PageSize})";
    public string? CacheGroupKey => CacheGroupKeyValue.ProgrammingLanguageTechnologyCacheGroupKey;

    public TimeSpan? SlidingExpiration { get; }

    public class GetListProgrammingLanguageTechnologyQueryQueryHandler : IRequestHandler<GetListProgrammingLanguageTechnologyQuery, GetListResponse<GetListProgrammingLanguageTechnologyListItemDto>>
    {
        // IRequestHandler<GetListTechnologyQuery, TechnologyListModel> bu satır amacı GetListTechnologyQuery bunu gönderildiğinde hangi Handler çalışıcak 

        public readonly IProgrammingLanguageTechnologyRepository _programmingLanguageTechnolojyRepository;
        public readonly IMapper _mapper;

        public GetListProgrammingLanguageTechnologyQueryQueryHandler(IProgrammingLanguageTechnologyRepository programmingLanguageTechnolojyRepository, IMapper mapper)
        {
            _programmingLanguageTechnolojyRepository = programmingLanguageTechnolojyRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListProgrammingLanguageTechnologyListItemDto>> Handle(GetListProgrammingLanguageTechnologyQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ProgrammingLanguageTechnology> programmingLanguageTechnologies = await _programmingLanguageTechnolojyRepository.GetListAsync(orderBy:
                                                                                            x => x.Include(c => c.ProgrammingLanguage)
                                                                                                   .OrderBy(c => c.ProgrammingLanguage.Name), // Programlama dili adına göre sırala // Include işlemi ilişkilendirme için
                                                                                            index: request.PageRequest.Page,
                                                                                            size: request.PageRequest.PageSize); // Birden fazla ilişkide yapılabilir. Github Projesinden bakılabilir. Linkedinde paylaşıldı.

            GetListResponse<GetListProgrammingLanguageTechnologyListItemDto> mappedProgrammingLanguageTechnologyListModel = _mapper.Map<GetListResponse<GetListProgrammingLanguageTechnologyListItemDto>>(programmingLanguageTechnologies);

            return mappedProgrammingLanguageTechnologyListModel;
        }
    }
}