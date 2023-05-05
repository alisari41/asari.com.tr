using asari.com.tr.Application.Features.Skills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.Skills.Queries.GetById;

public class GetByIdSkillQuery : IRequest<GetByIdSkillGetByIdResponse>
{
    public int Id { get; set; }

    public class GetByIdSkillQueryHandler : IRequestHandler<GetByIdSkillQuery, GetByIdSkillGetByIdResponse>
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

        public async Task<GetByIdSkillGetByIdResponse> Handle(GetByIdSkillQuery request, CancellationToken cancellationToken)
        {
            Skill? skill = await _skillRepository.GetAsync(x => x.Id == request.Id);
            _skillBusinessRules.SkillShouldExistWhenRequested(skill);

            GetByIdSkillGetByIdResponse mappedGetByIdSkillGetByIdResponse = _mapper.Map<GetByIdSkillGetByIdResponse>(skill);

            return mappedGetByIdSkillGetByIdResponse;
        }
    }
}