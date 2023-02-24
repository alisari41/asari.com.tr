using asari.com.tr.Application.Features.ProgrammingLanguages.Dtos;
using asari.com.tr.Application.Features.ProgrammingLanguages.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;

public class UpdateProgrammingLanguageCommand : IRequest<UpdatedProgrammingLanguageDto>
{
    // Son kullanıcının bize göndereceği son dataları içeren yapı
    public int Id { get; set; }
    public string Name { get; set; }

    public class UpdateProgrammingLanguageCommandHandler : IRequestHandler<UpdateProgrammingLanguageCommand, UpdatedProgrammingLanguageDto>
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageRules _programmingLanguageRules;

        public UpdateProgrammingLanguageCommandHandler(IProgrammingLanguageRepository programmingLanguageRepository, IMapper mapper, ProgrammingLanguageRules programmingLanguageRules)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
            _mapper = mapper;
            _programmingLanguageRules = programmingLanguageRules;
        }

        public async Task<UpdatedProgrammingLanguageDto> Handle(UpdateProgrammingLanguageCommand request, CancellationToken cancellationToken)
        {
            ProgrammingLanguage? programmingLanguage = await _programmingLanguageRepository.GetAsync(x => x.Id == request.Id); // id'yi ve bütün değişkenleri alır

            _programmingLanguageRules.ProgrammingLanguageShouldExistWhenRequested(programmingLanguage); // yollanan id boş mu diye kontrol sağlaması lazım

            _mapper.Map(request, programmingLanguage); // Request'in verilerini programmingLanguage1'e maple ( Benim verdiğim değişkenleri tablodaki verilerle maple )
            await _programmingLanguageRules.ProgrammingLanguageConNotBeDuplicatedWhenUpdated(programmingLanguage);


            var updatedProgrammingLanguage = await _programmingLanguageRepository.UpdateAsync(programmingLanguage);

            var updatedProgrammingLanguageDto = _mapper.Map<UpdatedProgrammingLanguageDto>(updatedProgrammingLanguage);

            return updatedProgrammingLanguageDto;
        }
    }
}
