using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Queries.GetList;

public class GetListProjectProgrammingLanguageTechnologyQuery : IRequest<GetListResponse<GetListProjectProgrammingLanguageTechnologyListItemDto>>, ICachableRequest
{
    // Mediator da IRequest
    public PageRequest PageRequest { get; set; } // Bir listeleme yapılacağı için bir Request üzerinden geçekleştirilecek

    public bool BypassCache { get; }
    public string CacheKey => $"GetListProjectProgrammingLanguageTechnology({PageRequest.Page},{PageRequest.PageSize})";
    public string? CacheGroupKey => CacheGroupKeyValue.ProjectProgrammingLanguageTechnologyCacheGroupKey;

    public TimeSpan? SlidingExpiration { get; }

    public class GetListProjectProgrammingLanguageTechnologyQueryHandler : IRequestHandler<GetListProjectProgrammingLanguageTechnologyQuery, GetListResponse<GetListProjectProgrammingLanguageTechnologyListItemDto>>
    {
        public readonly IProjectProgrammingLanguageTechnologyRepository _projectProgrammingLanguageTechnologyRepository;
        public readonly IMapper _mapper;

        public GetListProjectProgrammingLanguageTechnologyQueryHandler(IProjectProgrammingLanguageTechnologyRepository projectProgrammingLanguageTechnologyRepository, IMapper mapper)
        {
            _projectProgrammingLanguageTechnologyRepository = projectProgrammingLanguageTechnologyRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListProjectProgrammingLanguageTechnologyListItemDto>> Handle(GetListProjectProgrammingLanguageTechnologyQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ProjectProgrammingLanguageTechnology> projectProgrammingLanguageTechnologies = await _projectProgrammingLanguageTechnologyRepository.GetListAsync(orderBy: x =>
                                                                                                                   x.Include(c => c.Project)
                                                                                                                    .Include(c => c.ProgrammingLanguageTechnology)
                                                                                                                    .Include(c => c.ProgrammingLanguageTechnology.ProgrammingLanguage) 
                                                                                                                    .Include(c => c.Project.ProjectSkills).ThenInclude(d=>d.Skill) // Include işlemi ilişkilendirme için
                                                                                                                    .OrderBy(c => c.Project.Title) 
                                                                                                                    .OrderBy(c => c.ProgrammingLanguageTechnology.ProgrammingLanguage.Name), 
                                                                                                            index: request.PageRequest.Page,
                                                                                                            size: request.PageRequest.PageSize); // Birden fazla ilişkide yapılabilir. Github Projesinden bakılabilir. Linkedinde paylaşıldı.

            GetListResponse<GetListProjectProgrammingLanguageTechnologyListItemDto> mappedProjectProgrammingLanguageTechnologyListModel = _mapper.Map<GetListResponse<GetListProjectProgrammingLanguageTechnologyListItemDto>>(projectProgrammingLanguageTechnologies);

            return mappedProjectProgrammingLanguageTechnologyListModel;
        }
    }
}