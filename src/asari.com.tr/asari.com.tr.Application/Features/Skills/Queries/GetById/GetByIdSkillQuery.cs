using asari.com.tr.Application.Features.Skills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.Skills.Queries.GetById;

public class GetByIdSkillQuery : IRequest<GetByIdSkillResponse>
{
    public int Id { get; set; }

    public class GetByIdSkillQueryHandler : IRequestHandler<GetByIdSkillQuery, GetByIdSkillResponse>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;
        private readonly SkillBusinessRules _skillBusinessRules;

        public GetByIdSkillQueryHandler(ISkillRepository skillRepository, IMapper mapper, SkillBusinessRules skillBusinessRules)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
            _skillBusinessRules = skillBusinessRules;
        }

        public async Task<GetByIdSkillResponse> Handle(GetByIdSkillQuery request, CancellationToken cancellationToken)
        {
            Skill? skill = await _skillRepository.GetAsync(x => x.Id == request.Id);
            _skillBusinessRules.SkillShouldExistWhenRequested(skill);

            GetByIdSkillResponse mappedGetByIdSkillGetByIdResponse = _mapper.Map<GetByIdSkillResponse>(skill);

            return mappedGetByIdSkillGetByIdResponse;
        }
    }
}