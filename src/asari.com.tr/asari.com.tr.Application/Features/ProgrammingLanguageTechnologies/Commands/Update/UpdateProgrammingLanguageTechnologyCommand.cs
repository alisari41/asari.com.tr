using asari.com.tr.Application.Features.ProgrammingLanguages.Rules;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Constants;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Constants.ProgrammingLanguageTechnologiesOperationClaims;

namespace asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Commands.Update;

public class UpdateProgrammingLanguageTechnologyCommand : IRequest<UpdatedProgrammingLanguageTechnologyResponse>, ISecuredRequest, ICacheRemoverRequest
{
    // Son kullanıcının bize göndereceği son dataları içeren yapı
    public int Id { get; set; }
    public int ProgrammingLanguageId { get; set; }
    public string Name { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string? CacheGroupKey => CacheGroupKeyValue.ProgrammingLanguageTechnologyCacheGroupKey;

    public string[] Roles => new[] { Admin, Write, ProgrammingLanguageTechnologiesOperationClaims.Update };

    public class UpdateProgrammingLanguageTechnologyCommandHandler : IRequestHandler<UpdateProgrammingLanguageTechnologyCommand, UpdatedProgrammingLanguageTechnologyResponse>
    {
        private readonly IProgrammingLanguageTechnologyRepository _programmingLanguageTechnologyRepository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageTechnologyBusinessRules _programmingLanguageTechnologyRules;
        private readonly ProgrammingLanguageBusinessRules _programmingLanguageRules;

        public UpdateProgrammingLanguageTechnologyCommandHandler(IProgrammingLanguageTechnologyRepository programmingLanguageTechnologyRepository, IMapper mapper, ProgrammingLanguageTechnologyBusinessRules programmingLanguageTechnologyRules, ProgrammingLanguageBusinessRules programmingLanguageRules)
        {
            _programmingLanguageTechnologyRepository = programmingLanguageTechnologyRepository;
            _mapper = mapper;
            _programmingLanguageTechnologyRules = programmingLanguageTechnologyRules;
            _programmingLanguageRules = programmingLanguageRules;
        }

        public async Task<UpdatedProgrammingLanguageTechnologyResponse> Handle(UpdateProgrammingLanguageTechnologyCommand request, CancellationToken cancellationToken)
        {
            ProgrammingLanguageTechnology? programmingLanguageTechnology = await _programmingLanguageTechnologyRepository.GetAsync(x => x.Id == request.Id); // Buna ilerde ihtiyacımız olabilir. Çünkü ilerde belirli alanları alır diğer alanları almazsam buradan dönmesini sağlayabilirim.

            await _programmingLanguageTechnologyRules.ProgrammingLanguageTechnologyShouldExistWhenRequested(request.Id);
            await _programmingLanguageRules.ProgrammingLanguageShouldExistWhenRequested(request.ProgrammingLanguageId);

            _mapper.Map(request, programmingLanguageTechnology);
            await _programmingLanguageTechnologyRules.ProgrammingLanguageTechnologyNameConNotBeDuplicatedWhenUpdated(programmingLanguageTechnology); // Güncelleme işleminden önce mapleme yapılması gerekir.

            ProgrammingLanguageTechnology updatedProgrammingLanguageTechnology = await _programmingLanguageTechnologyRepository.UpdateAsync(programmingLanguageTechnology);
            UpdatedProgrammingLanguageTechnologyResponse mappedUpdatedProgrammingLanguageTechnologyDto = _mapper.Map<UpdatedProgrammingLanguageTechnologyResponse>(updatedProgrammingLanguageTechnology);

            return mappedUpdatedProgrammingLanguageTechnologyDto;
        }
    }
}