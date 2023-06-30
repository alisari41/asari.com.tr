using asari.com.tr.Application.Features.Projects.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.Projects.Queries.GetById;

public class GetByIdProjectQuery : IRequest<GetByIdProjectResponse>
{
    public int Id { get; set; }

    public class GetByIdProjectQueryHandler : IRequestHandler<GetByIdProjectQuery, GetByIdProjectResponse>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly ProjectBusinessRules _projectRules;

        public GetByIdProjectQueryHandler(IProjectRepository projectRepository, IMapper mapper, ProjectBusinessRules projectRules)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _projectRules = projectRules;
        }

        public async Task<GetByIdProjectResponse> Handle(GetByIdProjectQuery request, CancellationToken cancellationToken)
        {
            Project? projects = await _projectRepository.GetAsync(x => x.Id == request.Id,
                                                                        include: y => y
                                                                            .Include(c => c.TecgnologyProjects).ThenInclude(d => d.Technology)
                                                                            .Include(c => c.ProjectSkills).ThenInclude(d => d.Skill));

            await _projectRules.ProjectShouldExistWhenRequested(request.Id);

            GetByIdProjectResponse mappedProjectGetByIdDto = _mapper.Map<GetByIdProjectResponse>(projects);

            return mappedProjectGetByIdDto;
        }
    }
}