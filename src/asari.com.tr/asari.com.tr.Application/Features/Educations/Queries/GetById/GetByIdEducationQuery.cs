using asari.com.tr.Application.Features.Educations.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.Educations.Queries.GetById;

public class GetByIdEducationQuery : IRequest<GetByIdEducationResponse>
{
    public int Id { get; set; }

    public class GetByIdEducationQueryHandler : IRequestHandler<GetByIdEducationQuery, GetByIdEducationResponse>
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IMapper _mapper;
        private readonly EducationBusinessRules _educationBusinessRules;

        public GetByIdEducationQueryHandler(IEducationRepository educationRepository, IMapper mapper, EducationBusinessRules educationBusinessRules)
        {
            _educationRepository = educationRepository;
            _mapper = mapper;
            _educationBusinessRules = educationBusinessRules;
        }

        public async Task<GetByIdEducationResponse> Handle(GetByIdEducationQuery request, CancellationToken cancellationToken)
        {
            Education? education = await _educationRepository.GetAsync(x => x.Id == request.Id);
            _educationBusinessRules.EducationShouldExistWhenRequested(education);

            GetByIdEducationResponse mappedGetByIdEducationGetByIdResponse = _mapper.Map<GetByIdEducationResponse>(education);

            return mappedGetByIdEducationGetByIdResponse;
        }
    }
}