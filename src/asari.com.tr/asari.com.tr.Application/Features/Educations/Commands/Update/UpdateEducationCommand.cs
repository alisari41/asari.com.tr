using asari.com.tr.Application.Features.Educations.Constants;
using asari.com.tr.Application.Features.Educations.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.Educations.Constants.EducationsOperationClaims;

namespace asari.com.tr.Application.Features.Educations.Commands.Update;

public class UpdateEducationCommand : IRequest<UpdatedEducationResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Degree { get; set; }
    public string FieldOfStudy { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDateOrExcepted { get; set; }
    public string? Grade { get; set; }
    public string? ActivityAndCommunity { get; set; }
    public string? Description { get; set; }
    public string? MediaUrl { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string? CacheGroupKey => CacheGroupKeyValue.EducationCacheGroupKey;

    public string[] Roles => new[] { Admin, Write, EducationsOperationClaims.Update };

    public class UpdateEducationCommandHandler : IRequestHandler<UpdateEducationCommand, UpdatedEducationResponse>
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IMapper _mapper;
        private readonly EducationBusinessRules _educationBusinessRules;

        public UpdateEducationCommandHandler(IEducationRepository educationRepository, IMapper mapper, EducationBusinessRules educationBusinessRules)
        {
            _educationRepository = educationRepository;
            _mapper = mapper;
            _educationBusinessRules = educationBusinessRules;
        }

        public async Task<UpdatedEducationResponse> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
        {
            Education? education = await _educationRepository.GetAsync(x => x.Id == request.Id);

            _educationBusinessRules.EducationShouldExistWhenRequested(education);

            _mapper.Map(request, education);
            Education updatedEducation = await _educationRepository.UpdateAsync(education);
            UpdatedEducationResponse mappedUpdatedEducationResponse = _mapper.Map<UpdatedEducationResponse>(updatedEducation);

            return mappedUpdatedEducationResponse;
        }
    }
}