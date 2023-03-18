using asari.com.tr.Application.Features.ExperienceSkills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.ExperienceSkills.Commands.Delete;

public class DeleteExperienceSkillCommand : IRequest<DeletedExperienceSkillResponse>
{
    public int Id { get; set; }

    public class DeleteExperienceSkillCommandHandler : IRequestHandler<DeleteExperienceSkillCommand, DeletedExperienceSkillResponse>
    {
        private readonly IExperienceSkillRepository _experienceSkillRepository;
        private readonly IMapper _mapper;
        private readonly ExperienceSkillBusinessRules _experienceSkillBusinessRules;

        public DeleteExperienceSkillCommandHandler(IExperienceSkillRepository experienceSkillRepository, IMapper mapper, ExperienceSkillBusinessRules experienceSkillBusinessRules)
        {
            _experienceSkillRepository = experienceSkillRepository;
            _mapper = mapper;
            _experienceSkillBusinessRules = experienceSkillBusinessRules;
        }

        public async Task<DeletedExperienceSkillResponse> Handle(DeleteExperienceSkillCommand request, CancellationToken cancellationToken)
        {
            ExperienceSkill? experienceSkill = await _experienceSkillRepository.GetAsync(x => x.Id == request.Id);

            _experienceSkillBusinessRules.ExperienceSkillShouldExistWhenRequested(experienceSkill);

            _mapper.Map(request, experienceSkill);
            ExperienceSkill deletedExperienceSkill = await _experienceSkillRepository.DeleteAsync(experienceSkill);
            DeletedExperienceSkillResponse mappedDeletedExperienceSkillResponse = _mapper.Map<DeletedExperienceSkillResponse>(deletedExperienceSkill);

            return mappedDeletedExperienceSkillResponse;
        }
    }
}