using asari.com.tr.Application.Features.ExperienceSkills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.ExperienceSkills.Queries.GetById;

public class GetByIdExperienceSkillQuery : IRequest<GetByIdExperienceSkillResponse>
{
    public int Id { get; set; }

    public class GetByIdExperienceSkillQueryHandler : IRequestHandler<GetByIdExperienceSkillQuery, GetByIdExperienceSkillResponse>
    {
        private readonly IExperienceSkillRepository _experienceSkillRepository;
        private readonly IMapper _mapper;
        private readonly ExperienceSkillBusinessRules _experienceSkillBusinessRules;

        public GetByIdExperienceSkillQueryHandler(IExperienceSkillRepository experienceSkillRepository, IMapper mapper, ExperienceSkillBusinessRules experienceSkillBusinessRules)
        {
            _experienceSkillRepository = experienceSkillRepository;
            _mapper = mapper;
            _experienceSkillBusinessRules = experienceSkillBusinessRules;
        }

        public async Task<GetByIdExperienceSkillResponse> Handle(GetByIdExperienceSkillQuery request, CancellationToken cancellationToken)
        {
            ExperienceSkill? experienceSkill = await _experienceSkillRepository.GetAsync(x => x.Id == request.Id,
                                                                                       include: i =>
                                                                                            i.Include(c => c.Experience)
                                                                                             .Include(c => c.Skill));

            _experienceSkillBusinessRules.ExperienceSkillShouldExistWhenRequested(experienceSkill);

            GetByIdExperienceSkillResponse mappedGetByIdExperienceSkillGetByIdResponse = _mapper.Map<GetByIdExperienceSkillResponse>(experienceSkill);

            return mappedGetByIdExperienceSkillGetByIdResponse;
        }
    }
}