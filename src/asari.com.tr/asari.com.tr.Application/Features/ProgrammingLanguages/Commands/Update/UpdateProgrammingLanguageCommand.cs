using asari.com.tr.Application.Features.ProgrammingLanguages.Constants;
using asari.com.tr.Application.Features.ProgrammingLanguages.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.ProgrammingLanguages.Constants.ProgrammingLanguagesOperationClaims;

namespace asari.com.tr.Application.Features.ProgrammingLanguages.Commands.Update;

public class UpdateProgrammingLanguageCommand : IRequest<UpdatedProgrammingLanguageResponse>, ISecuredRequest, ICacheRemoverRequest
{
    // Son kullanıcının bize göndereceği son dataları içeren yapı
    public int Id { get; set; }
    public string Name { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { CacheGroupKeyValue.ProgrammingLanguageCacheGroupKey };

    public string[] Roles => new[] { Admin, Write, ProgrammingLanguagesOperationClaims.Update };

    public class UpdateProgrammingLanguageCommandHandler : IRequestHandler<UpdateProgrammingLanguageCommand, UpdatedProgrammingLanguageResponse>
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageBusinessRules _programmingLanguageRules;

        public UpdateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper, ProgrammingLanguageBusinessRules programmingLanguageRules)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
            _mapper = mapper;
            _programmingLanguageRules = programmingLanguageRules;
        }

        public async Task<UpdatedProgrammingLanguageResponse> Handle(UpdateProgrammingLanguageCommand request, CancellationToken cancellationToken)
        {
            ProgrammingLanguage? programmingLanguage = await _programmingLanguageRepository.GetAsync(x => x.Id == request.Id); // id'yi ve bütün değişkenleri alır

            _programmingLanguageRules.ProgrammingLanguageShouldExistWhenRequested(programmingLanguage); // yollanan id boş mu diye kontrol sağlaması lazım

            _mapper.Map(request, programmingLanguage); // Request'in verilerini programmingLanguage1'e maple ( Benim verdiğim değişkenleri tablodaki verilerle maple )
            await _programmingLanguageRules.ProgrammingLanguageConNotBeDuplicatedWhenUpdated(programmingLanguage);


            var updatedProgrammingLanguage = await _programmingLanguageRepository.UpdateAsync(programmingLanguage);

            var updatedProgrammingLanguageResponse = _mapper.Map<UpdatedProgrammingLanguageResponse>(updatedProgrammingLanguage);

            return updatedProgrammingLanguageResponse;
        }
    }
}