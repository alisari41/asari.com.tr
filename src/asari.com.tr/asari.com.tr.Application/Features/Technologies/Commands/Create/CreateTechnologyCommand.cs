using asari.com.tr.Application.Features.Technologies.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.Technologies.Commands.Create;

public class CreateTechnologyCommand : IRequest<CreatedTechnologyResponse>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public string Content { get; set; }

    public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreatedTechnologyResponse>
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IMapper _mapper;
        private readonly TechnologyBusinessRules _technologyBusinessRules;

        public CreateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
        {
            _technologyRepository = technologyRepository;
            _mapper = mapper;
            _technologyBusinessRules = technologyBusinessRules;
        }

        public async Task<CreatedTechnologyResponse> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
        {
            await _technologyBusinessRules.TechnologyTitleConNotBeDuplicatedWhenInserted(request.Title);

            Technology mappedTechnology = _mapper.Map<Technology>(request);
            Technology createdTechnology = await _technologyRepository.AddAsync(mappedTechnology);
            CreatedTechnologyResponse mappedCreatedTechnologyResponse = _mapper.Map<CreatedTechnologyResponse>(createdTechnology);

            return mappedCreatedTechnologyResponse;
        }
    }
}