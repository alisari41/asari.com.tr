﻿using asari.com.tr.Application.Features.Experiences.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.Experiences.Constants.ExperiencesOperationClaims;

namespace asari.com.tr.Application.Features.Experiences.Commands.Create;

public class CreateExperienceCommand : IRequest<CreatedExperienceResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public string Title { get; set; }
    public string EmploymentType { get; set; }
    public string CompanyName { get; set; }
    public string Location { get; set; }
    public bool Statu { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Industry { get; set; }
    public string Description { get; set; }
    public string? ProfileHeadline { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { CacheGroupKeyValue.ExperienceCacheGroupKey };

    public string[] Roles => new[] { Admin, Write, Add };

    public class CreateExperienceCommandHandler : IRequestHandler<CreateExperienceCommand, CreatedExperienceResponse>
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IMapper _mapper;
        private readonly ExperienceBusinessRules _experienceBusinessRules;

        public CreateExperienceCommandHandler(IExperienceRepository experienceRepository, IMapper mapper, ExperienceBusinessRules experienceBusinessRules)
        {
            _experienceRepository = experienceRepository;
            _mapper = mapper;
            _experienceBusinessRules = experienceBusinessRules;
        }

        public async Task<CreatedExperienceResponse> Handle(CreateExperienceCommand request, CancellationToken cancellationToken)
        {
            Experience? mappedExperience = _mapper.Map<Experience>(request);
            Experience? createdExperience = await _experienceRepository.AddAsync(mappedExperience);
            CreatedExperienceResponse mappedCreatedExperienceResponse = _mapper.Map<CreatedExperienceResponse>(createdExperience);

            return mappedCreatedExperienceResponse;
        }
    }
}