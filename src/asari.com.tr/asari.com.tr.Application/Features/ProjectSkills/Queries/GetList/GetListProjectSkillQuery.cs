using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.ProjectSkills.Queries.GetList;

public class GetListProjectSkillQuery : IRequest<GetListResponse<GetListProjectSkillListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; } // Bir listeleme yapılacağı için bir Request üzerinden geçekleştirilecek

    public bool BypassCache { get; }
    public string CacheKey => $"GetListProjectSkill({PageRequest.Page},{PageRequest.PageSize})";
    public string? CacheGroupKey => CacheGroupKeyValue.ProjectSkillCacheGroupKey;

    public TimeSpan? SlidingExpiration { get; }

    public class GetListProjectSkillQueryHandler : IRequestHandler<GetListProjectSkillQuery, GetListResponse<GetListProjectSkillListItemDto>>
    {
        private readonly IProjectSkillRepository _projectSkillRepository;
        private readonly IMapper _mapper;

        public GetListProjectSkillQueryHandler(IProjectSkillRepository projectSkillRepository, IMapper mapper)
        {
            _projectSkillRepository = projectSkillRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListProjectSkillListItemDto>> Handle(GetListProjectSkillQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ProjectSkill> projectSkills = await _projectSkillRepository.GetListAsync(include: x =>
                                                                    x.Include(c => c.Project)
                                                                     .Include(c => c.Skill),
                                                                    index: request.PageRequest.Page,
                                                                    size: request.PageRequest.PageSize);

            GetListResponse<GetListProjectSkillListItemDto> mappedGetListProjectSkillListItemDto = _mapper.Map<GetListResponse<GetListProjectSkillListItemDto>>(projectSkills);

            return mappedGetListProjectSkillListItemDto;
        }
    }
}