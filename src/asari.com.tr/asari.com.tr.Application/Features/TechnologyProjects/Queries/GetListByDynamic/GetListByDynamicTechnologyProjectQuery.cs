using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.TechnologyProjects.Queries.GetListByDynamic;

public class GetListByDynamicTechnologyProjectQuery : IRequest<GetListResponse<GetListByDynamicTechnologyProjectListItemDto>>
{
    public Dynamic Dynamic { get; set; }
    public PageRequest PageRequest { get; set; }

    public class GetListByDynamicTechnologyProjectQueryHandler : IRequestHandler<GetListByDynamicTechnologyProjectQuery, GetListResponse<GetListByDynamicTechnologyProjectListItemDto>>
    {
        private readonly ITechnologyProjectRepository _technologyProjectRepository;
        private readonly IMapper _mapper;

        public GetListByDynamicTechnologyProjectQueryHandler(ITechnologyProjectRepository technologyProjectRepository, IMapper mapper)
        {
            _technologyProjectRepository = technologyProjectRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByDynamicTechnologyProjectListItemDto>> Handle(GetListByDynamicTechnologyProjectQuery request, CancellationToken cancellationToken)
        {
            IPaginate<TechnologyProject> technologyProject = await _technologyProjectRepository.GetListByDynamicAsync( // Dinamik Sorgu
                                                                dynamic: request.Dynamic,
                                                                    include: x =>
                                                                    x.Include(c => c.Technology)
                                                                     .Include(c => c.Project)
                                                                     .Include(c => c.Project.ProjectProgrammingLanguageTechnologies).ThenInclude(d => d.ProgrammingLanguageTechnology)
                                                                     .Include(c => c.Project.ProjectProgrammingLanguageTechnologies).ThenInclude(d => d.ProgrammingLanguageTechnology.ProgrammingLanguage),
                                                                    index: request.PageRequest.Page,
                                                                    size: request.PageRequest.PageSize);

            GetListResponse<GetListByDynamicTechnologyProjectListItemDto> mappedGetListByDynamicTechnologyProjectListItemDto = _mapper.Map<GetListResponse<GetListByDynamicTechnologyProjectListItemDto>>(technologyProject);

            return mappedGetListByDynamicTechnologyProjectListItemDto;
        }
    }
}