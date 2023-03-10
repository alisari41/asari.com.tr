using asari.com.tr.Application.Features.Technologies.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.Technologies.Queries.GetById;

public class GetByIdTechnologyQuery : IRequest<GetByIdTechnologyResponse>
{
    public int Id { get; set; }

    public class GetByIdTechnologyQueryHandler : IRequestHandler<GetByIdTechnologyQuery, GetByIdTechnologyResponse>
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IMapper _mapper;
        private readonly TechnologyBusinessRules _technologyBusinessRules;

        public GetByIdTechnologyQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
        {
            _technologyRepository = technologyRepository;
            _mapper = mapper;
            _technologyBusinessRules = technologyBusinessRules;
        }

        public async Task<GetByIdTechnologyResponse> Handle(GetByIdTechnologyQuery request, CancellationToken cancellationToken)
        {
            Technology? technology = await _technologyRepository.GetAsync(x => x.Id == request.Id);

            _technologyBusinessRules.TechnologyShouldExistWhenRequested(technology);

            GetByIdTechnologyResponse mappedGetByIdTechnologyResponse = _mapper.Map<GetByIdTechnologyResponse>(technology);

            return mappedGetByIdTechnologyResponse;
        }
    }
}