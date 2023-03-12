using asari.com.tr.Application.Features.TechnologyProjects.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.TechnologyProjects.Queries.GetById;

public class GetByIdTechnologyProjectQuery : IRequest<GetByIdTechnologyProjectResponse>
{
    public int Id { get; set; }

    public class GetByIdTechnologyProjectQueryHandler : IRequestHandler<GetByIdTechnologyProjectQuery, GetByIdTechnologyProjectResponse>
    {
        private readonly ITechnologyProjectRepository _technologyProjectRepository;
        private readonly IMapper _mapper;
        private readonly TechnologyProjectBusinessRules _technologyProjectBusinessRules;

        public GetByIdTechnologyProjectQueryHandler(ITechnologyProjectRepository technologyProjectRepository, IMapper mapper, TechnologyProjectBusinessRules technologyProjectBusinessRules)
        {
            _technologyProjectRepository = technologyProjectRepository;
            _mapper = mapper;
            _technologyProjectBusinessRules = technologyProjectBusinessRules;
        }

        public async Task<GetByIdTechnologyProjectResponse> Handle(GetByIdTechnologyProjectQuery request, CancellationToken cancellationToken)
        {
            TechnologyProject? technologyProject = await _technologyProjectRepository.GetAsync(x => x.Id == request.Id,include: x =>
                                                                                 x.Include(c => c.Technology)
                                                                                  .Include(c => c.Project)
                                                                                  .Include(c => c.Project.ProjectProgrammingLanguageTechnologies).ThenInclude(d => d.ProgrammingLanguageTechnology)
                                                                                  .Include(c => c.Project.ProjectProgrammingLanguageTechnologies).ThenInclude(d => d.ProgrammingLanguageTechnology.ProgrammingLanguage));

            _technologyProjectBusinessRules.TechnologyProjectShouldExistWhenRequested(technologyProject);

            GetByIdTechnologyProjectResponse mappedGetByIdTechnologyProjectResponse = _mapper.Map<GetByIdTechnologyProjectResponse>(technologyProject);

            return mappedGetByIdTechnologyProjectResponse;
        }
    }
}