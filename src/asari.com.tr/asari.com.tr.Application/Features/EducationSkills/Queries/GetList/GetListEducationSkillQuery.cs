using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.EducationSkills.Queries.GetList;

public class GetListEducationSkillQuery : IRequest<GetListResponse<GetListEducationSkillListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; } // Bir listeleme yapılacağı için bir Request üzerinden geçekleştirilecek

    public bool BypassCache { get; }
    public string CacheKey => $"GetListEducationSkill({PageRequest.Page},{PageRequest.PageSize})";
    public string? CacheGroupKey => CacheGroupKeyValue.EducationSkillCacheGroupKey;

    public TimeSpan? SlidingExpiration { get; }

    public class GetListEducationSkillQueryHandler : IRequestHandler<GetListEducationSkillQuery, GetListResponse<GetListEducationSkillListItemDto>>
    {
        private readonly IEducationSkillRepository _educationSkillRepository;
        private readonly IMapper _mapper;

        public GetListEducationSkillQueryHandler(IEducationSkillRepository educationSkillRepository, IMapper mapper)
        {
            _educationSkillRepository = educationSkillRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListEducationSkillListItemDto>> Handle(GetListEducationSkillQuery request, CancellationToken cancellationToken)
        {
            IPaginate<EducationSkill> educationSkill = await _educationSkillRepository.GetListAsync(orderBy: o =>
                                                                                                            o.Include(c => c.Education)
                                                                                                             .Include(c => c.Skill)
                                                                                                             .OrderByDescending(c => c.Education.Name),
                                                                                                            index: request.PageRequest.Page,
                                                                                                            size: request.PageRequest.PageSize);

            GetListResponse<GetListEducationSkillListItemDto> mappedGetListEducationSkillListItemDto = _mapper.Map<GetListResponse<GetListEducationSkillListItemDto>>(educationSkill);

            return mappedGetListEducationSkillListItemDto;
        }
    }
}