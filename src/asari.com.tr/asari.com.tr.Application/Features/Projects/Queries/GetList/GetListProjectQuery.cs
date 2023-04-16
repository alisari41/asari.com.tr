using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;

namespace asari.com.tr.Application.Features.Projects.Queries.GetList;

public class GetListProjectQuery : IRequest<GetListResponse<GetListProjectListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; } // Bir listeleme yapılacağı için bir Request üzerinden geçekleştirilecek

    public bool BypassCache { get; }
    public string CacheKey => $"GetListProject({PageRequest.Page},{PageRequest.PageSize})";
    public string? CacheGroupKey => CacheGroupKeyValue.ProjectCacheGroupKey;

    public TimeSpan? SlidingExpiration { get; }

    public class GetListProjectQueryHandler : IRequestHandler<GetListProjectQuery, GetListResponse<GetListProjectListItemDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetListProjectQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListProjectListItemDto>> Handle(GetListProjectQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Project> projects = await _projectRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

            GetListResponse<GetListProjectListItemDto> mappedProjectListModel = _mapper.Map<GetListResponse<GetListProjectListItemDto>>(projects);

            return mappedProjectListModel;
        }
    }
}