using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;

namespace asari.com.tr.Application.Features.Technologies.Queries.GetList;

public class GetListTechnologyQuery : IRequest<GetListResponse<GetListTechnologyListItemDto>>
{
    public PageRequest PageRequest { get; set; } // Bir listeleme yapılacağı için bir Request üzerinden geçekleştirilecek

    public class GetListTechnologyQueryHandler : IRequestHandler<GetListTechnologyQuery, GetListResponse<GetListTechnologyListItemDto>>
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IMapper _mapper;

        public GetListTechnologyQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper)
        {
            _technologyRepository = technologyRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListTechnologyListItemDto>> Handle(GetListTechnologyQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Technology> technologies = await _technologyRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

            GetListResponse<GetListTechnologyListItemDto> mappedGetListTechnologyListItemDto = _mapper.Map<GetListResponse<GetListTechnologyListItemDto>>(technologies);

            return mappedGetListTechnologyListItemDto;
        }
    }
}