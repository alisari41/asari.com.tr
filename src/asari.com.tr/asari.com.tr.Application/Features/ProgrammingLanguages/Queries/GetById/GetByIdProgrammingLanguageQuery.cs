using asari.com.tr.Application.Features.ProgrammingLanguages.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.ProgrammingLanguages.Queries.GetById;

public class GetByIdProgrammingLanguageQuery : IRequest<GetByIdProgrammingLanguageResponse>
{
    public int Id { get; set; }

    public class GetByIdProgrammingLanguageQueryHandler : IRequestHandler<GetByIdProgrammingLanguageQuery, GetByIdProgrammingLanguageResponse>
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageBusinessRules _programmingLanguageRules;

        public GetByIdProgrammingLanguageQueryHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper, ProgrammingLanguageBusinessRules programmingLanguageRules)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
            _mapper = mapper;
            _programmingLanguageRules = programmingLanguageRules;
        }

        public async Task<GetByIdProgrammingLanguageResponse> Handle(GetByIdProgrammingLanguageQuery request, CancellationToken cancellationToken)
        {
            ProgrammingLanguage? programmingLanguage = await _programmingLanguageRepository.GetAsync(x => x.Id == request.Id); // Aranan Id'e göre tüm liste gelmesi istendiği için bu şekilde kullanıldı
            _programmingLanguageRules.ProgrammingLanguageShouldExistWhenRequested(programmingLanguage);

            GetByIdProgrammingLanguageResponse mappedPrgrammingLanguageGetByIdResponse = _mapper.Map<GetByIdProgrammingLanguageResponse>(programmingLanguage);

            return mappedPrgrammingLanguageGetByIdResponse;
        }
    }
}
