using asari.com.tr.Application.Features.Educations.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.Educations.Queries.GetById;

public class GetByIdEducationQuery : IRequest<GetByIdEducationGetByIdResponse>
{
    public int Id { get; set; }

    public class GetByIdEducationQueryHandler : IRequestHandler<GetByIdEducationQuery, GetByIdEducationGetByIdResponse>
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

        public async Task<GetByIdEducationGetByIdResponse> Handle(GetByIdEducationQuery request, CancellationToken cancellationToken)
        {
            Education? education = await _educationRepository.GetAsync(x => x.Id == request.Id);
            _educationBusinessRules.EducationShouldExistWhenRequested(education);

            GetByIdEducationGetByIdResponse mappedGetByIdEducationGetByIdResponse = _mapper.Map<GetByIdEducationGetByIdResponse>(education);

            return mappedGetByIdEducationGetByIdResponse;
        }
    }
}