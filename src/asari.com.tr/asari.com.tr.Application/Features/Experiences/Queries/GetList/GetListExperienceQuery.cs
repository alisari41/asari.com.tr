using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.Experiences.Queries.GetList;

public class GetListExperienceQuery : IRequest<GetListResponse<GetListExperienceListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; } // Bir listeleme yapılacağı için bir Request üzerinden geçekleştirilecek

    public bool BypassCache { get; }
    public string CacheKey => $"GetListExperience({PageRequest.Page},{PageRequest.PageSize})";
    public string? CacheGroupKey => CacheGroupKeyValue.ExperienceCacheGroupKey;

    public TimeSpan? SlidingExpiration { get; }

    public class GetListExperienceQueryHandler : IRequestHandler<GetListExperienceQuery, GetListResponse<GetListExperienceListItemDto>>
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IMapper _mapper;

        public GetListExperienceQueryHandler(IExperienceRepository experienceRepository, IMapper mapper)
        {
            _experienceRepository = experienceRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListExperienceListItemDto>> Handle(GetListExperienceQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Experience> experiences = await _experienceRepository.GetListAsync(orderBy:
                                                                                                x => x.Include(c => c.ExperienceSkills).ThenInclude(d => d.Skill)
                                                                                                       .OrderByDescending(c => c.StartDate),
                                                                                                index: request.PageRequest.Page, size: request.PageRequest.PageSize);

            GetListResponse<GetListExperienceListItemDto> mappedGetListExperienceListItemDto = _mapper.Map<GetListResponse<GetListExperienceListItemDto>>(experiences);

            return mappedGetListExperienceListItemDto;
        }
    }
}