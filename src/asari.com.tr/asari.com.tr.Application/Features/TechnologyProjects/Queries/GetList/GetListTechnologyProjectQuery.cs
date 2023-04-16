using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.TechnologyProjects.Queries.GetList;

public class GetListTechnologyProjectQuery : IRequest<GetListResponse<GetListTechnologyProjectListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; } // Bir listeleme yapılacağı için bir Request üzerinden geçekleştirilecek

    public bool BypassCache { get; }
    public string CacheKey => $"GetListTechnologyProject({PageRequest.Page},{PageRequest.PageSize})";
    public string? CacheGroupKey => CacheGroupKeyValue.TechnologyProjectCacheGroupKey;

    public TimeSpan? SlidingExpiration { get; }

    public class GetListTechnologyProjectQueryHandler : IRequestHandler<GetListTechnologyProjectQuery, GetListResponse<GetListTechnologyProjectListItemDto>>
    {
        private readonly ITechnologyProjectRepository _tecgnologyProjectRepository;
        private readonly IMapper _mapper;

        public GetListTechnologyProjectQueryHandler(ITechnologyProjectRepository tecgnologyProjectRepository, IMapper mapper)
        {
            _tecgnologyProjectRepository = tecgnologyProjectRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListTechnologyProjectListItemDto>> Handle(GetListTechnologyProjectQuery request, CancellationToken cancellationToken)
        {
            IPaginate<TechnologyProject> technologyProjects = await _tecgnologyProjectRepository.GetListAsync(include: x =>
                                                                                 x.Include(c => c.Technology)
                                                                                  .Include(c => c.Project)
                                                                                  .Include(c => c.Project.ProjectProgrammingLanguageTechnologies).ThenInclude(d => d.ProgrammingLanguageTechnology)
                                                                                  .Include(c => c.Project.ProjectProgrammingLanguageTechnologies).ThenInclude(d => d.ProgrammingLanguageTechnology.ProgrammingLanguage),
                                                                                  index: request.PageRequest.Page,
                                                                                  size: request.PageRequest.PageSize);

            GetListResponse<GetListTechnologyProjectListItemDto> mappedGetListTechnologyProjectListItemDto = _mapper.Map<GetListResponse<GetListTechnologyProjectListItemDto>>(technologyProjects);

            return mappedGetListTechnologyProjectListItemDto;
        }
    }
}