﻿using asari.com.tr.Application.Features.Experiences.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.Experiences.Commands.Update;

public class UpdateExperienceCommand : IRequest<UpdatedExperienceResponse>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string EmploymentType { get; set; }
    public string CompanyName { get; set; }
    public string Location { get; set; }
    public bool Statu { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Industry { get; set; }
    public string Description { get; set; }
    public string? ProfileHeadline { get; set; }

    public class UpdateExperienceCommandHandler : IRequestHandler<UpdateExperienceCommand, UpdatedExperienceResponse>
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IMapper _mapper;
        private readonly ExperienceBusinessRules _experienceBusinessRules;

        public UpdateExperienceCommandHandler(IExperienceRepository experienceRepository, IMapper mapper, ExperienceBusinessRules experienceBusinessRules)
        {
            _experienceRepository = experienceRepository;
            _mapper = mapper;
            _experienceBusinessRules = experienceBusinessRules;
        }

        public async Task<UpdatedExperienceResponse> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
        {
            Experience? experience = await _experienceRepository.GetAsync(x => x.Id == request.Id);

            _experienceBusinessRules.ExperienceShouldExistWhenRequested(experience);

            _mapper.Map(request, experience);
            await _experienceBusinessRules.ExperienceTitleConNotBeDuplicatedWhenUpdated(experience);

            Experience updatedExperience = await _experienceRepository.UpdateAsync(experience);
            UpdatedExperienceResponse mappedUpdatedExperienceResponse = _mapper.Map<UpdatedExperienceResponse>(updatedExperience);

            return mappedUpdatedExperienceResponse;
        }
    }
}