using asari.com.tr.Application.Features.Projects.Queries.GetList;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using MediatR;

namespace asari.com.tr.Application.Features.Projects.Queries.GetListByDynamic;

public class GetListProjectByDynamicQuery : IRequest<GetListResponse<GetListProjectListItemDto>>
{
    public Dynamic Dynamic { get; set; }
    public PageRequest PageRequest { get; set; }

    public class GetListProjectByDynamicQueryHandler : IRequestHandler<GetListProjectByDynamicQuery, GetListResponse<GetListProjectListItemDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetListProjectByDynamicQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListProjectListItemDto>> Handle(GetListProjectByDynamicQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Project> projects = await _projectRepository.GetListByDynamicAsync(
                                                request.Dynamic,
                                                index: request.PageRequest.Page,
                                                size: request.PageRequest.PageSize);

            GetListResponse<GetListProjectListItemDto> mappedProjectListModel = _mapper.Map<GetListResponse<GetListProjectListItemDto>>(projects);

            return mappedProjectListModel;
        }
    }
}