﻿using asari.com.tr.Application.Features.ProjectSkills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.ProjectSkills.Commands.Delete;

public class DeleteProjectSkillCommand : IRequest<DeletedProjectSkillResponse>
{
    public int Id { get; set; }

    public class DeleteProjectSkillCommandHandler : IRequestHandler<DeleteProjectSkillCommand, DeletedProjectSkillResponse>
    {
        private readonly IProjectSkillRepository _projectSkillRepository;
        private readonly IMapper _mapper;
        private readonly ProjectSkillBusinessRules _projectSkillBusinessRules;

        public DeleteProjectSkillCommandHandler(IProjectSkillRepository projectSkillRepository, IMapper mapper, ProjectSkillBusinessRules projectSkillBusinessRules)
        {
            _projectSkillRepository = projectSkillRepository;
            _mapper = mapper;
            _projectSkillBusinessRules = projectSkillBusinessRules;
        }

        public async Task<DeletedProjectSkillResponse> Handle(DeleteProjectSkillCommand request, CancellationToken cancellationToken)
        {
            ProjectSkill? projectSkill = await _projectSkillRepository.GetAsync(x => x.Id == request.Id);

            await _projectSkillBusinessRules.ProjectSkillShouldExistWhenRequested(request.Id);

            _mapper.Map(request, projectSkill);
            ProjectSkill deletedProjectSkill = await _projectSkillRepository.DeleteAsync(projectSkill);
            DeletedProjectSkillResponse mappedDeletedProjectSkillResponse = _mapper.Map<DeletedProjectSkillResponse>(deletedProjectSkill);

            return mappedDeletedProjectSkillResponse;
        }
    }
}