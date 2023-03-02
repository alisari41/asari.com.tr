﻿using asari.com.tr.Application.Features.Projects.Dtos;
using asari.com.tr.Application.Features.Projects.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.Projects.Queries.GetByIdProject;

public class GetByIdProjectQuery : IRequest<ProjectGetByIdDto>
{
    public int Id { get; set; }

    public class GetByIdProjectQueryHandler : IRequestHandler<GetByIdProjectQuery, ProjectGetByIdDto>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly ProjectRules _projectRules;

        public GetByIdProjectQueryHandler(IProjectRepository projectRepository, IMapper mapper, ProjectRules projectRules)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _projectRules = projectRules;
        }

        public async Task<ProjectGetByIdDto> Handle(GetByIdProjectQuery request, CancellationToken cancellationToken)
        {
            Project? projects = await _projectRepository.GetAsync(x => x.Id == request.Id);

            await _projectRules.ProjectShouldExistWhenRequested(request.Id);

            ProjectGetByIdDto mappedProjectGetByIdDto=_mapper.Map<ProjectGetByIdDto>(projects);

            return mappedProjectGetByIdDto;
        }
    }
}