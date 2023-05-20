using asari.com.tr.Application.Features.ProjectSkills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.ProjectSkills.Queries.GetById;

public class GetByIdProjectSkillQuery : IRequest<GetByIdProjectSkillGetByIdResponse>
{
    public int Id { get; set; }

    public class GetByIdProjectSkillQueryHandler : IRequestHandler<GetByIdProjectSkillQuery, GetByIdProjectSkillGetByIdResponse>
    {
        private readonly IProjectSkillRepository _projectSkillRepository;
        private readonly IMapper _mapper;
        private readonly ProjectSkillBusinessRules _projectSkillBusinessRules;

        public GetByIdProjectSkillQueryHandler(IProjectSkillRepository projectSkillRepository, IMapper mapper, ProjectSkillBusinessRules projectSkillBusinessRules)
        {
            _projectSkillRepository = projectSkillRepository;
            _mapper = mapper;
            _projectSkillBusinessRules = projectSkillBusinessRules;
        }

        public async Task<GetByIdProjectSkillGetByIdResponse> Handle(GetByIdProjectSkillQuery request, CancellationToken cancellationToken)
        {
            ProjectSkill? ProjectSkill = await _projectSkillRepository.GetAsync(x => x.Id == request.Id,
                                                                                       include: x =>
                                                                                                x.Include(c => c.Project)
                                                                                                 .Include(c => c.Skill));

            _projectSkillBusinessRules.ProjectSkillShouldExistWhenRequested(ProjectSkill);

            GetByIdProjectSkillGetByIdResponse mappedGetByIdProjectSkillGetByIdResponse = _mapper.Map<GetByIdProjectSkillGetByIdResponse>(ProjectSkill);

            return mappedGetByIdProjectSkillGetByIdResponse;

        }
    }
}