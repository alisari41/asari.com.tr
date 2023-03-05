using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Queries.GetList;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Queries.GetListByDynamic;

public class GetListProgrammingLanuguageTechnologyByDynamicQuery : IRequest<GetListResponse<GetListProgrammingLanguageTechnologyListItemDto>>
{
    public Dynamic Dynamic { get; set; } // Dinamik sorgu yazmak için kullanacağız
    public PageRequest PageRequest { get; set; }

    public class GetListProgrammingLanuguageTechnologyByDynamicQueryJandler : IRequestHandler<GetListProgrammingLanuguageTechnologyByDynamicQuery, GetListResponse<GetListProgrammingLanguageTechnologyListItemDto>>
    {
        private readonly IProgrammingLanguageTechnologyRepository _programmingLanguageTechnologyRepository;
        private readonly IMapper _mapper;

        public GetListProgrammingLanuguageTechnologyByDynamicQueryJandler(IProgrammingLanguageTechnologyRepository programmingLanguageTechnologyRepository, IMapper mapper)
        {
            _programmingLanguageTechnologyRepository = programmingLanguageTechnologyRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListProgrammingLanguageTechnologyListItemDto>> Handle(GetListProgrammingLanuguageTechnologyByDynamicQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ProgrammingLanguageTechnology> programmingLanguageTechnologies = await _programmingLanguageTechnologyRepository.GetListByDynamicAsync( // Dinamik Sorgu
                                                                                                    request.Dynamic,
                                                                                                    include: x => x.Include(c => c.ProgrammingLanguage), // Hangi tablodan veri alınacaksa
                                                                                                    index: request.PageRequest.Page,
                                                                                                    size: request.PageRequest.PageSize);

            GetListResponse<GetListProgrammingLanguageTechnologyListItemDto> mappedProgrammingLanguageTechnologyListModel = _mapper.Map<GetListResponse<GetListProgrammingLanguageTechnologyListItemDto>>(programmingLanguageTechnologies);

            return mappedProgrammingLanguageTechnologyListModel;
        }
    }
}