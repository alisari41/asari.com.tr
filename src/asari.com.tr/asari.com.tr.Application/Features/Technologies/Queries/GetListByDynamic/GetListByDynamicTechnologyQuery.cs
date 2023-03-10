using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using MediatR;

namespace asari.com.tr.Application.Features.Technologies.Queries.GetListByDynamic;

public class GetListByDynamicTechnologyQuery : IRequest<GetListResponse<GetListByDynamicTechnologyListItemDto>>
{
    public Dynamic Dynamic { get; set; } // Dinamik sorgu yazmak için kullanacağız
    public PageRequest PageRequest { get; set; }

    public class GetListByDynamicTechnologyQueryHandler : IRequestHandler<GetListByDynamicTechnologyQuery, GetListResponse<GetListByDynamicTechnologyListItemDto>>
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IMapper _mapper;

        public GetListByDynamicTechnologyQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper)
        {
            _technologyRepository = technologyRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByDynamicTechnologyListItemDto>> Handle(GetListByDynamicTechnologyQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Technology> technology = await _technologyRepository.GetListByDynamicAsync( // Dinamik Sorgu
                                                                                request.Dynamic,
                                                                                index: request.PageRequest.Page,
                                                                                size: request.PageRequest.PageSize);

            GetListResponse<GetListByDynamicTechnologyListItemDto> mappedGetListByDynamicTechnologyListItemDto = _mapper.Map<GetListResponse<GetListByDynamicTechnologyListItemDto>>(technology);

            return mappedGetListByDynamicTechnologyListItemDto;
        }
    }
}