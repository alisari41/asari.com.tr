using asari.com.tr.Application.Features.Experiences.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.Experiences.Commands.Delete;

public class DeleteExperienceCommand : IRequest<DeletedExperienceResponse>
{
    public int Id { get; set; }

    public class DeleteExperienceCommandHandler : IRequestHandler<DeleteExperienceCommand, DeletedExperienceResponse>
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IMapper _mapper;
        private readonly ExperienceBusinessRules _experienceBusinessRules;

        public DeleteExperienceCommandHandler(IExperienceRepository experienceRepository, IMapper mapper, ExperienceBusinessRules experienceBusinessRules)
        {
            _experienceRepository = experienceRepository;
            _mapper = mapper;
            _experienceBusinessRules = experienceBusinessRules;
        }

        public async Task<DeletedExperienceResponse> Handle(DeleteExperienceCommand request, CancellationToken cancellationToken)
        {
            Experience? experience = await _experienceRepository.GetAsync(x => x.Id == request.Id);

            _experienceBusinessRules.ExperienceShouldExistWhenRequested(experience);

            _mapper.Map(request, experience);
            Experience deletedExperience = await _experienceRepository.DeleteAsync(experience);
            DeletedExperienceResponse mappedDeletedExperienceResponse=_mapper.Map<DeletedExperienceResponse>(deletedExperience);

            return mappedDeletedExperienceResponse;
        }
    }
}