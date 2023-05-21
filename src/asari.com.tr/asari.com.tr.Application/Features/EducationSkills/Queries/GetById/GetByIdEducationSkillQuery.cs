using asari.com.tr.Application.Features.EducationSkills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.EducationSkills.Queries.GetById;

public class GetByIdEducationSkillQuery : IRequest<GetByIdEducationSkillResponse>
{
    public int Id { get; set; }

    public class GetByIdEducationSkillQueryHandler : IRequestHandler<GetByIdEducationSkillQuery, GetByIdEducationSkillResponse>
    {
        private readonly IEducationSkillRepository _educationSkillRepository;
        private readonly IMapper _mapper;
        private readonly EducationSkillBusinessRules _educationSkillBusinessRules;

        public GetByIdEducationSkillQueryHandler(IEducationSkillRepository educationSkillRepository, IMapper mapper, EducationSkillBusinessRules educationSkillBusinessRules)
        {
            _educationSkillRepository = educationSkillRepository;
            _mapper = mapper;
            _educationSkillBusinessRules = educationSkillBusinessRules;
        }

        public async Task<GetByIdEducationSkillResponse> Handle(GetByIdEducationSkillQuery request, CancellationToken cancellationToken)
        {
            EducationSkill? educationSkill = await _educationSkillRepository.GetAsync(x => x.Id == request.Id,
                                                                                       include: i =>
                                                                                            i.Include(c => c.Education)
                                                                                             .Include(c => c.Skill));

            _educationSkillBusinessRules.EducationSkillShouldExistWhenRequested(educationSkill);

            GetByIdEducationSkillResponse mappedGetByIdEducationSkillGetByIdResponse = _mapper.Map<GetByIdEducationSkillResponse>(educationSkill);

            return mappedGetByIdEducationSkillGetByIdResponse;
        }
    }
}