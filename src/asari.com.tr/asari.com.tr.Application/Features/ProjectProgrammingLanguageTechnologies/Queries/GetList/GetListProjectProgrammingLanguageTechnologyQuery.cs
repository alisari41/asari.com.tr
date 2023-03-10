using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Queries.GetList;

public class GetListProjectProgrammingLanguageTechnologyQuery : IRequest<GetListResponse<GetListProjectProgrammingLanguageTechnologyListDto>>
{
    // Mediator da IRequest
    public PageRequest PageRequest { get; set; } // Bir listeleme yapılacağı için bir Request üzerinden geçekleştirilecek

    public class GetListProjectProgrammingLanguageTechnologyQueryHandler : IRequestHandler<GetListProjectProgrammingLanguageTechnologyQuery, GetListResponse<GetListProjectProgrammingLanguageTechnologyListDto>>
    {
        public readonly IProjectProgrammingLanguageTechnologyRepository _projectProgrammingLanguageTechnologyRepository;
        public readonly IMapper _mapper;

        public GetListProjectProgrammingLanguageTechnologyQueryHandler(IProjectProgrammingLanguageTechnologyRepository projectProgrammingLanguageTechnologyRepository, IMapper mapper)
        {
            _projectProgrammingLanguageTechnologyRepository = projectProgrammingLanguageTechnologyRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListProjectProgrammingLanguageTechnologyListDto>> Handle(GetListProjectProgrammingLanguageTechnologyQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ProjectProgrammingLanguageTechnology> projectProgrammingLanguageTechnologies = await _projectProgrammingLanguageTechnologyRepository.GetListAsync(include: x =>
                                                                                                                   x.Include(c => c.Project)
                                                                                                                    .Include(c => c.ProgrammingLanguageTechnology)
                                                                                                                    .Include(c => c.ProgrammingLanguageTechnology.ProgrammingLanguage), // Include işlemi ilişkilendirme için
                                                                                                            index: request.PageRequest.Page,
                                                                                                            size: request.PageRequest.PageSize); // Birden fazla ilişkide yapılabilir. Github Projesinden bakılabilir. Linkedinde paylaşıldı.

            GetListResponse<GetListProjectProgrammingLanguageTechnologyListDto> mappedProjectProgrammingLanguageTechnologyListModel = _mapper.Map<GetListResponse<GetListProjectProgrammingLanguageTechnologyListDto>>(projectProgrammingLanguageTechnologies);

            return mappedProjectProgrammingLanguageTechnologyListModel;
        }
    }
}