using asari.com.tr.Application.Features.Experiences.Rules;
using asari.com.tr.Application.Features.ExperienceSkills.Rules;
using asari.com.tr.Application.Features.Skills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.ExperienceSkills.Commands.Update;

public class UpdateExperienceSkillCommand : IRequest<UpdatedExperienceSkillResponse>
{
    public int Id { get; set; }
    public int ExperienceId { get; set; }
    public int SkillId { get; set; }

    public class UpdateExperienceSkillCommandHandler : IRequestHandler<UpdateExperienceSkillCommand, UpdatedExperienceSkillResponse>
    {
        private readonly IExperienceSkillRepository _experienceSkillRepository;
        private readonly IMapper _mapper;
        private readonly ExperienceSkillBusinessRules _experienceSkillBusinessRules;
        private readonly ExperienceBusinessRules _experienceBusinessRules;
        private readonly SkillBusinessRules _skillBusinessRules;

        public UpdateExperienceSkillCommandHandler(IExperienceSkillRepository experienceSkillRepository, IMapper mapper, ExperienceSkillBusinessRules experienceSkillBusinessRules, ExperienceBusinessRules experienceBusinessRules, SkillBusinessRules skillBusinessRules)
        {
            _experienceSkillRepository = experienceSkillRepository;
            _mapper = mapper;
            _experienceSkillBusinessRules = experienceSkillBusinessRules;
            _experienceBusinessRules = experienceBusinessRules;
            _skillBusinessRules = skillBusinessRules;
        }

        public async Task<UpdatedExperienceSkillResponse> Handle(UpdateExperienceSkillCommand request, CancellationToken cancellationToken)
        {
            ExperienceSkill? experienceSkill = await _experienceSkillRepository.GetAsync(x => x.Id == request.Id);

            _experienceSkillBusinessRules.ExperienceSkillShouldExistWhenRequested(experienceSkill);

            _mapper.Map(request, experienceSkill);

            await _experienceSkillBusinessRules.ExperienceSkillConNotBeDuplicatedWhenUpdated(experienceSkill);
            await _experienceBusinessRules.ExperienceShouldExistWhenRequested(request.ExperienceId);
            await _skillBusinessRules.SkillShouldExistWhenRequested(request.SkillId);

            ExperienceSkill updatedExperienceSkill = await _experienceSkillRepository.UpdateAsync(experienceSkill);
            UpdatedExperienceSkillResponse mappedUpdatedExperienceSkillResponse = _mapper.Map<UpdatedExperienceSkillResponse>(updatedExperienceSkill);

            return mappedUpdatedExperienceSkillResponse;
        }
    }
}