using asari.com.tr.Application.Features.Technologies.Constants;
using asari.com.tr.Application.Features.Technologies.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static asari.com.tr.Application.Features.Technologies.Constants.TechnologiesOperationClaims;

namespace asari.com.tr.Application.Features.Technologies.Commands.Update;

public class UpdateTechnologyCommand : IRequest<UpdatedTechnologyResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public string Content { get; set; }

    public string[] Roles => new[] { Admin, Write, TechnologiesOperationClaims.Update };

    public class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand, UpdatedTechnologyResponse>
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IMapper _mapper;
        private readonly TechnologyBusinessRules _technologyBusinessRules;

        public UpdateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
        {
            _technologyRepository = technologyRepository;
            _mapper = mapper;
            _technologyBusinessRules = technologyBusinessRules;
        }

        public async Task<UpdatedTechnologyResponse> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
        {
            Technology? technology = await _technologyRepository.GetAsync(x => x.Id == request.Id);

            await _technologyBusinessRules.TechnologyShouldExistWhenRequested(request.Id);

            _mapper.Map(request, technology);
            await _technologyBusinessRules.TechnologyTitleConNotBeDuplicatedWhenUpdated(technology);

            Technology updatedTechnology = await _technologyRepository.UpdateAsync(technology);
            UpdatedTechnologyResponse mappedUpdatedTechnologyResponse = _mapper.Map<UpdatedTechnologyResponse>(updatedTechnology);

            return mappedUpdatedTechnologyResponse;
        }
    }
}