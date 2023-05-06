﻿using asari.com.tr.Application.Features.Experiences.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.Experiences.Queries.GetById;

public class GetByIdExperienceQuery : IRequest<GetByIdExperienceGetByIdResponse>
{
    public int Id { get; set; }

    public class GetByIdExperienceQueryHandler : IRequestHandler<GetByIdExperienceQuery, GetByIdExperienceGetByIdResponse>
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

        public async Task<GetByIdExperienceGetByIdResponse> Handle(GetByIdExperienceQuery request, CancellationToken cancellationToken)
        {
            Experience? experience = await _experienceRepository.GetAsync(x => x.Id == request.Id);
            _experienceBusinessRules.ExperienceShouldExistWhenRequested(experience);

            GetByIdExperienceGetByIdResponse mappedGetByIdExperienceGetByIdResponse = _mapper.Map<GetByIdExperienceGetByIdResponse>(experience);

            return mappedGetByIdExperienceGetByIdResponse;
        }
    }
}