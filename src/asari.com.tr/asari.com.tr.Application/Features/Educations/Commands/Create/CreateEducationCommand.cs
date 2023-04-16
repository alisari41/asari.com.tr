using asari.com.tr.Application.Features.Educations.Constants;
using asari.com.tr.Application.Features.Educations.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static asari.com.tr.Application.Features.Educations.Constants.EducationsOperationClaims;

namespace asari.com.tr.Application.Features.Educations.Commands.Create;

public class CreateEducationCommand : IRequest<CreatedEducationResponse>, ISecuredRequest
{
    public string Name { get; set; }
    public double Degree { get; set; }
    public string FieldOfStudy { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDateOrExcepted { get; set; }
    public string? Grade { get; set; }
    public string? ActivityAndCommunity { get; set; }
    public string? Description { get; set; }
    public string? MediaUrl { get; set; }

    public string[] Roles => new[] { Admin, Write, Add };

    public class CreateEducationCommandHandler : IRequestHandler<CreateEducationCommand, CreatedEducationResponse>
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IMapper _mapper;
        private readonly EducationBusinessRules _educationBusinessRules;

        public CreateEducationCommandHandler(IEducationRepository educationRepository, IMapper mapper, EducationBusinessRules educationBusinessRules)
        {
            _educationRepository = educationRepository;
            _mapper = mapper;
            _educationBusinessRules = educationBusinessRules;
        }

        public async Task<CreatedEducationResponse> Handle(CreateEducationCommand request, CancellationToken cancellationToken)
        {
            Education? mappedEducation = _mapper.Map<Education>(request);
            Education createdEducation = await _educationRepository.AddAsync(mappedEducation);
            CreatedEducationResponse mappedCreatedEducationResponse = _mapper.Map<CreatedEducationResponse>(createdEducation);

            return mappedCreatedEducationResponse;
        }
    }
}