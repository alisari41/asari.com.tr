using asari.com.tr.Application.Features.ProgrammingLanguages.Rules;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Dtos;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Commands.CreateProgrammingLanguageTechnology;

public class CreateProgrammingLanguageTechnologyCommand : IRequest<CreatedProgrammingLanguageTechnologyDto>
{
    // Son kullanıcının bize göndereceği son dataları içeren yapı
    public int ProgrammingLanguageId { get; set; }
    public string Name { get; set; }

    public class CreateProgrammingLanguageTechnologyCommandHandler : IRequestHandler<CreateProgrammingLanguageTechnologyCommand, CreatedProgrammingLanguageTechnologyDto>
    {
        private readonly IProgrammingLanguageTechnologyRepository _programmingLanguageTechnologyRepository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageTechnologyRules _programmingLanguageTechnologyRules;
        private readonly ProgrammingLanguageRules _programmingLanguageRules;

        public CreateProgrammingLanguageTechnologyCommandHandler(IProgrammingLanguageTechnologyRepository programmingLanguageTechnologyRepository, IMapper mapper, ProgrammingLanguageTechnologyRules programmingLanguageTechnologyRules, ProgrammingLanguageRules programmingLanguageRules)
        {
            _programmingLanguageTechnologyRepository = programmingLanguageTechnologyRepository;
            _mapper = mapper;
            _programmingLanguageTechnologyRules = programmingLanguageTechnologyRules;
            _programmingLanguageRules = programmingLanguageRules;
        }

        public async Task<CreatedProgrammingLanguageTechnologyDto> Handle(CreateProgrammingLanguageTechnologyCommand request, CancellationToken cancellationToken)
        {
            await _programmingLanguageTechnologyRules.TechnologyNameCanNotBeDuplicatedWhenIserted(request.Name);
            await _programmingLanguageRules.ProgrammingLanguageShouldExistWhenRequested(request.ProgrammingLanguageId);

            ProgrammingLanguageTechnology mappedProgrammingLanguageTechnology = _mapper.Map<ProgrammingLanguageTechnology>(request);
            ProgrammingLanguageTechnology createdProgrammingLanguageTechnology = await _programmingLanguageTechnologyRepository.AddAsync(mappedProgrammingLanguageTechnology);
            CreatedProgrammingLanguageTechnologyDto mappedCreatedProgrammingLanguageTechnologyDto = _mapper.Map<CreatedProgrammingLanguageTechnologyDto>(createdProgrammingLanguageTechnology);

            return mappedCreatedProgrammingLanguageTechnologyDto;
        }
    }
}