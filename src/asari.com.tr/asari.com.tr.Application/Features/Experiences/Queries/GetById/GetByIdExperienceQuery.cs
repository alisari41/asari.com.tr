using asari.com.tr.Application.Features.Experiences.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.Experiences.Queries.GetById;

public class GetByIdExperienceQuery : IRequest<GetByIdExperienceResponse>
{
    public int Id { get; set; }

    public class GetByIdExperienceQueryHandler : IRequestHandler<GetByIdExperienceQuery, GetByIdExperienceResponse>
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IMapper _mapper;
        private readonly ExperienceBusinessRules _experienceBusinessRules;

        public GetByIdExperienceQueryHandler(IExperienceRepository experienceRepository, IMapper mapper, ExperienceBusinessRules experienceBusinessRules)
        {
            _experienceRepository = experienceRepository;
            _mapper = mapper;
            _experienceBusinessRules = experienceBusinessRules;
        }

        public async Task<GetByIdExperienceResponse> Handle(GetByIdExperienceQuery request, CancellationToken cancellationToken)
        {
            Experience? experience = await _experienceRepository.GetAsync(x => x.Id == request.Id);
            _experienceBusinessRules.ExperienceShouldExistWhenRequested(experience);

            GetByIdExperienceResponse mappedGetByIdExperienceGetByIdResponse = _mapper.Map<GetByIdExperienceResponse>(experience);

            return mappedGetByIdExperienceGetByIdResponse;
        }
    }
}