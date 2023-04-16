using asari.com.tr.Application.Features.Technologies.Constants;
using asari.com.tr.Application.Features.Technologies.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.Technologies.Constants.TechnologiesOperationClaims;

namespace asari.com.tr.Application.Features.Technologies.Commands.Delete;

public class DeleteTechnologyCommand : IRequest<DeletedTechnologyResponse>, ISecuredRequest, ICacheRemoverRequest
{
    // Kullanıcının bize göndereceği son dataları içeren yapı 
    public int Id { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string? CacheGroupKey => CacheGroupKeyValue.TechnologyCacheGroupKey;

    public string[] Roles => new[] { Admin, Write, TechnologiesOperationClaims.Delete };

    public class DeleteTechnologyCommandHandler : IRequestHandler<DeleteTechnologyCommand, DeletedTechnologyResponse>
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IMapper _mapper;
        private readonly TechnologyBusinessRules _technologyBusinessRules;

        public DeleteTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
        {
            _technologyRepository = technologyRepository;
            _mapper = mapper;
            _technologyBusinessRules = technologyBusinessRules;
        }

        public async Task<DeletedTechnologyResponse> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
        {
            Technology? technology = await _technologyRepository.GetAsync(x => x.Id == request.Id); // Geriye hangi veriyi sildiğimi görmek için kullandım

            await _technologyBusinessRules.TechnologyShouldExistWhenRequested(request.Id);

            _mapper.Map(request, technology);

            Technology deletedTechnology = await _technologyRepository.DeleteAsync(technology);
            DeletedTechnologyResponse mappedDeletedTechnologyResponse = _mapper.Map<DeletedTechnologyResponse>(deletedTechnology);

            return mappedDeletedTechnologyResponse;
        }
    }
}