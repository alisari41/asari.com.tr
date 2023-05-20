using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Queries.GetById;

public class GetByIdProjectProgrammingLanguageTechnologyQuery : IRequest<GetByIdProjectProgrammingLanguageTechnologyGetByIdResponse>
{
    public int Id { get; set; }

    public class GetByIdProjectProgrammingLanguageTechnologyQueryHandler : IRequestHandler<GetByIdProjectProgrammingLanguageTechnologyQuery, GetByIdProjectProgrammingLanguageTechnologyGetByIdResponse>
    {
        private readonly IProjectProgrammingLanguageTechnologyRepository _projectProgrammingLanguageTechnologyRepository;
        private readonly IMapper _mapper;
        private readonly ProjectProgrammingLanguageTechnologyBusinessRules _projectProgrammingLanguageTechnologyBusinessRules;

        public GetByIdProjectProgrammingLanguageTechnologyQueryHandler(IProjectProgrammingLanguageTechnologyRepository projectProgrammingLanguageTechnologyRepository, IMapper mapper, ProjectProgrammingLanguageTechnologyBusinessRules projectProgrammingLanguageTechnologyRules)
        {
            _projectProgrammingLanguageTechnologyRepository = projectProgrammingLanguageTechnologyRepository;
            _mapper = mapper;
            _projectProgrammingLanguageTechnologyBusinessRules = projectProgrammingLanguageTechnologyRules;
        }

        public async Task<GetByIdProjectProgrammingLanguageTechnologyGetByIdResponse> Handle(GetByIdProjectProgrammingLanguageTechnologyQuery request, CancellationToken cancellationToken)
        {
            ProjectProgrammingLanguageTechnology? ProjectProgrammingLanguageTechnology = await _projectProgrammingLanguageTechnologyRepository.GetAsync(x => x.Id == request.Id,
                                                                                                                   include: i =>
                                                                                                                   i.Include(c => c.Project)
                                                                                                                    .Include(c => c.ProgrammingLanguageTechnology)
                                                                                                                    .Include(c => c.ProgrammingLanguageTechnology.ProgrammingLanguage));

            _projectProgrammingLanguageTechnologyBusinessRules.ProjectProgrammingLanguageTechnologyShouldExistWhenRequested(ProjectProgrammingLanguageTechnology);

            GetByIdProjectProgrammingLanguageTechnologyGetByIdResponse mappedGetByIdProjectProgrammingLanguageTechnologyGetByIdResponse = _mapper.Map<GetByIdProjectProgrammingLanguageTechnologyGetByIdResponse>(ProjectProgrammingLanguageTechnology);

            return mappedGetByIdProjectProgrammingLanguageTechnologyGetByIdResponse;
        }
    }
}