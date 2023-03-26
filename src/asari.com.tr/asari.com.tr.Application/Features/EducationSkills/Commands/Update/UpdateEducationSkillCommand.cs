using asari.com.tr.Application.Features.Educations.Rules;
using asari.com.tr.Application.Features.EducationSkills.Rules;
using asari.com.tr.Application.Features.Skills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.EducationSkills.Commands.Update;

public class UpdateEducationSkillCommand : IRequest<UpdatedEducationSkillResponse>
{
    public int Id { get; set; }
    public int EducationId { get; set; }
    public int SkillId { get; set; }

    public class UpdateEducationSkillCommandHandler : IRequestHandler<UpdateEducationSkillCommand, UpdatedEducationSkillResponse>
    {
        private readonly IEducationSkillRepository _educationSkillRepository;
        private readonly IMapper _mapper;
        private readonly EducationSkillBusinessRules _educationSkillBusinessRules;
        private readonly EducationBusinessRules _educationBusinessRules;
        private readonly SkillBusinessRules _skillBusinessRules;

        public UpdateEducationSkillCommandHandler(IEducationSkillRepository educationSkillRepository, IMapper mapper, EducationSkillBusinessRules educationSkillBusinessRules, EducationBusinessRules educationBusinessRules, SkillBusinessRules skillBusinessRules)
        {
            _educationSkillRepository = educationSkillRepository;
            _mapper = mapper;
            _educationSkillBusinessRules = educationSkillBusinessRules;
            _educationBusinessRules = educationBusinessRules;
            _skillBusinessRules = skillBusinessRules;
        }

        public async Task<UpdatedEducationSkillResponse> Handle(UpdateEducationSkillCommand request, CancellationToken cancellationToken)
        {
            EducationSkill? educationSkill = await _educationSkillRepository.GetAsync(x => x.Id == request.Id);

            _educationSkillBusinessRules.EducationSkillShouldExistWhenRequested(educationSkill);

            _mapper.Map(request, educationSkill);

            await _educationSkillBusinessRules.EducationSkillConNotBeDuplicatedWhenUpdated(educationSkill);
            await _educationBusinessRules.EducationShouldExistWhenRequested(request.EducationId);
            await _skillBusinessRules.SkillShouldExistWhenRequested(request.SkillId);

            EducationSkill updatedEducationSkill = await _educationSkillRepository.UpdateAsync(educationSkill);
            UpdatedEducationSkillResponse mappedUpdatedEducationSkillResponse=_mapper.Map<UpdatedEducationSkillResponse>(updatedEducationSkill);

            return mappedUpdatedEducationSkillResponse;
        }
    }
}