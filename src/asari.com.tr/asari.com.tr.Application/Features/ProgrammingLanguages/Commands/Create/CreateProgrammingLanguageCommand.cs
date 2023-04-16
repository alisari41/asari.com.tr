using asari.com.tr.Application.Features.ProgrammingLanguages.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static asari.com.tr.Application.Features.ProgrammingLanguages.Constants.ProgrammingLanguagesOperationClaims;

namespace asari.com.tr.Application.Features.ProgrammingLanguages.Commands.Create;

public class CreateProgrammingLanguageCommand : IRequest<CreatedProgrammingLanguageResponse>, ISecuredRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, Add };

    // Bir tanede Handlerımız var yani böyle bir command sıraya koyulursa hangi Handler çalışacak onu IRequestHandler olduğunu belirtiyoruz. Hem çalışacağımız command'i hemde dönüş tipimizi belirtiyoruz.
    public class CreateProgrammingLanguageCommandHandler : IRequestHandler<CreateProgrammingLanguageCommand, CreatedProgrammingLanguageResponse>
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageBusinessRules _programmingLanguageRules;

        public CreateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper, ProgrammingLanguageBusinessRules programmingLanguageRules)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
            _mapper = mapper;
            _programmingLanguageRules = programmingLanguageRules;
        }

        public async Task<CreatedProgrammingLanguageResponse> Handle(CreateProgrammingLanguageCommand request, CancellationToken cancellationToken)
        {
            await _programmingLanguageRules.ProgrammingLanguageConNotBeDuplicatedWhenInserted(request.Name);

            ProgrammingLanguage mappedProgrammingLanguage = _mapper.Map<ProgrammingLanguage>(request); // mapper kullanarak Parametre olarak gelen "request"'i ProgrammingLanguage nesnesine çevir. 
            ProgrammingLanguage createdProgrammingLanguage = await _programmingLanguageRepository.AddAsync(mappedProgrammingLanguage);
            CreatedProgrammingLanguageResponse createdProgrammingLanguageResponse = _mapper.Map<CreatedProgrammingLanguageResponse>(createdProgrammingLanguage);


            return createdProgrammingLanguageResponse;
        }
    }
}
