using asari.com.tr.Application.Features.Educations.Constants;
using asari.com.tr.Application.Features.Educations.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.Educations.Constants.EducationsOperationClaims;

namespace asari.com.tr.Application.Features.Educations.Commands.Delete;

public class DeleteEducationCommand : IRequest<DeletedEducationResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int? Id { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { CacheGroupKeyValue.EducationCacheGroupKey };

    public string[] Roles => new[] { Admin, Write, EducationsOperationClaims.Delete };

    public class DeleteEducationCommandHandler : IRequestHandler<DeleteEducationCommand, DeletedEducationResponse>
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IMapper _mapper;
        private readonly EducationBusinessRules _educationBusinessRules;

        public DeleteEducationCommandHandler(IEducationRepository educationRepository, IMapper mapper, EducationBusinessRules educationBusinessRules)
        {
            _educationRepository = educationRepository;
            _mapper = mapper;
            _educationBusinessRules = educationBusinessRules;
        }

        public async Task<DeletedEducationResponse> Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
        {
            Education? education = await _educationRepository.GetAsync(x => x.Id == request.Id);

            _educationBusinessRules.EducationShouldExistWhenRequested(education);

            _mapper.Map(request, education);
            Education deletedEducation = await _educationRepository.DeleteAsync(education);
            DeletedEducationResponse mappedDeletedEducationResponse = _mapper.Map<DeletedEducationResponse>(deletedEducation);

            return mappedDeletedEducationResponse;
        }
    }
}