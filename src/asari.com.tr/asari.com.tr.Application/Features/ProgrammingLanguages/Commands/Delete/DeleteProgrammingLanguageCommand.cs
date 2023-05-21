using asari.com.tr.Application.Features.ProgrammingLanguages.Constants;
using asari.com.tr.Application.Features.ProgrammingLanguages.Rules;
using asari.com.tr.Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.ProgrammingLanguages.Constants.ProgrammingLanguagesOperationClaims;

namespace asari.com.tr.Application.Features.ProgrammingLanguages.Commands.Delete;

public class DeleteProgrammingLanguageCommand : IRequest<DeletedProgrammingLanguageResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { CacheGroupKeyValue.ProgrammingLanguageCacheGroupKey };

    public string[] Roles => new[] { Admin, Write, ProgrammingLanguagesOperationClaims.Delete };

    public class DeleteProgrammingLanguageCommandHandler : IRequestHandler<DeleteProgrammingLanguageCommand, DeletedProgrammingLanguageResponse>
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageBusinessRules _programmingLanguageRules;

        public DeleteProgrammingLanguageCommandHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper, ProgrammingLanguageBusinessRules programmingLanguageRules)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
            _mapper = mapper;
            _programmingLanguageRules = programmingLanguageRules;
        }

        public async Task<DeletedProgrammingLanguageResponse> Handle(DeleteProgrammingLanguageCommand request, CancellationToken cancellationToken)
        {

            var programmingLanguage = await _programmingLanguageRepository.GetAsync(x => x.Id == request.Id);

            _programmingLanguageRules.ProgrammingLanguageShouldExistWhenRequested(programmingLanguage); // yollanan id boş mu diye kontrol sağlaması lazım

            _mapper.Map(request, programmingLanguage);

            var deletedProgrammingLanguage = await _programmingLanguageRepository.DeleteAsync(programmingLanguage);

            var deleteProgrammingLanguageResponse = _mapper.Map<DeletedProgrammingLanguageResponse>(deletedProgrammingLanguage);

            return deleteProgrammingLanguageResponse;

        }
    }
}
