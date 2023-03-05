using asari.com.tr.Application.Features.ProgrammingLanguages.Rules;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Commands.Create;

public class CreateProgrammingLanguageTechnologyCommand : IRequest<CreatedProgrammingLanguageTechnologyReponse>
{
    // Son kullanıcının bize göndereceği son dataları içeren yapı
    public int ProgrammingLanguageId { get; set; }
    public string Name { get; set; }

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