using asari.com.tr.Application.Features.ProgrammingLanguages.Rules;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Constants.ProgrammingLanguageTechnologiesOperationClaims;

namespace asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Commands.Create;

public class CreateProgrammingLanguageTechnologyCommand : IRequest<CreatedProgrammingLanguageTechnologyReponse>, ISecuredRequest, ICacheRemoverRequest
{
    // Son kullanıcının bize göndereceği son dataları içeren yapı
    public int ProgrammingLanguageId { get; set; }
    public string Name { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string? CacheGroupKey => CacheGroupKeyValue.ProgrammingLanguageTechnologyCacheGroupKey;

    public string[] Roles => new[] { Admin, Write, Add };

    public class CreateProgrammingLanguageTechnologyCommandHandler : IRequestHandler<CreateProgrammingLanguageTechnologyCommand, CreatedProgrammingLanguageTechnologyReponse>
    {
        private readonly IProgrammingLanguageTechnologyRepository _programmingLanguageTechnologyRepository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageTechnologyBusinessRules _programmingLanguageTechnologyRules;
        private readonly ProgrammingLanguageBusinessRules _programmingLanguageRules;

        public CreateProgrammingLanguageTechnologyCommandHandler(IProgrammingLanguageTechnologyRepository programmingLanguageTechnologyRepository, IMapper mapper, ProgrammingLanguageTechnologyBusinessRules programmingLanguageTechnologyRules, ProgrammingLanguageBusinessRules programmingLanguageRules)
        {
            _programmingLanguageTechnologyRepository = programmingLanguageTechnologyRepository;
            _mapper = mapper;
            _programmingLanguageTechnologyRules = programmingLanguageTechnologyRules;
            _programmingLanguageRules = programmingLanguageRules;
        }

        public async Task<CreatedProgrammingLanguageTechnologyReponse> Handle(CreateProgrammingLanguageTechnologyCommand request, CancellationToken cancellationToken)
        {
            await _programmingLanguageTechnologyRules.ProgrammingLanguageTechnologyNameCanNotBeDuplicatedWhenIserted(request.Name);
            await _programmingLanguageRules.ProgrammingLanguageShouldExistWhenRequested(request.ProgrammingLanguageId);

            ProgrammingLanguageTechnology mappedProgrammingLanguageTechnology = _mapper.Map<ProgrammingLanguageTechnology>(request);
            ProgrammingLanguageTechnology createdProgrammingLanguageTechnology = await _programmingLanguageTechnologyRepository.AddAsync(mappedProgrammingLanguageTechnology);
            CreatedProgrammingLanguageTechnologyReponse mappedCreatedProgrammingLanguageTechnologyDto = _mapper.Map<CreatedProgrammingLanguageTechnologyReponse>(createdProgrammingLanguageTechnology);

            return mappedCreatedProgrammingLanguageTechnologyDto;
        }
    }
}