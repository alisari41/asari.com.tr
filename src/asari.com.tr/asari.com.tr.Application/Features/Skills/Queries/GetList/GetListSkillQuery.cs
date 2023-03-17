using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;

namespace asari.com.tr.Application.Features.Skills.Queries.GetList;

public class GetListSkillQuery : IRequest<GetListResponse<GetListSkillListItemDto>>
{
    public PageRequest PageRequest { get; set; } // Bir listeleme yapılacağı için bir Request üzerinden geçekleştirilecek

    public class GetListSkillQueryHandler : IRequestHandler<GetListSkillQuery, GetListResponse<GetListSkillListItemDto>>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;

        public GetListSkillQueryHandler(ISkillRepository skillRepository, IMapper mapper)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListSkillListItemDto>> Handle(GetListSkillQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Skill> skills = await _skillRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

            GetListResponse<GetListSkillListItemDto> mappedGetListSkillListItemDto = _mapper.Map<GetListResponse<GetListSkillListItemDto>>(skills);

            return mappedGetListSkillListItemDto;
        }
    }
}