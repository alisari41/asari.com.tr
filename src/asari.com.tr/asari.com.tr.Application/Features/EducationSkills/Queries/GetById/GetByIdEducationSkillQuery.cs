using asari.com.tr.Application.Features.EducationSkills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.EducationSkills.Queries.GetById;

public class GetByIdEducationSkillQuery : IRequest<GetByIdEducationSkillGetByIdResponse>
{
    public int Id { get; set; }

    public class GetByIdEducationSkillQueryHandler : IRequestHandler<GetByIdEducationSkillQuery, GetByIdEducationSkillGetByIdResponse>
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

        public async Task<GetByIdEducationSkillGetByIdResponse> Handle(GetByIdEducationSkillQuery request, CancellationToken cancellationToken)
        {
            EducationSkill? educationSkill = await _educationSkillRepository.GetAsync(x => x.Id == request.Id,
                                                                                       include: i =>
                                                                                            i.Include(c => c.Education)
                                                                                             .Include(c => c.Skill));

            _educationSkillBusinessRules.EducationSkillShouldExistWhenRequested(educationSkill);

            GetByIdEducationSkillGetByIdResponse mappedGetByIdEducationSkillGetByIdResponse = _mapper.Map<GetByIdEducationSkillGetByIdResponse>(educationSkill);

            return mappedGetByIdEducationSkillGetByIdResponse;
        }
    }
}