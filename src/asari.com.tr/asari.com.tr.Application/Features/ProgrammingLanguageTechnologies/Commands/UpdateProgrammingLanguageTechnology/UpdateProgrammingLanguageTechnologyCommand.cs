using asari.com.tr.Application.Features.ProgrammingLanguages.Rules;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Dtos;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Commands.UpdateProgrammingLanguageTechnology;

public class UpdateProgrammingLanguageTechnologyCommand : IRequest<UpdatedProgrammingLanguageTechnologyDto>
{
    // Son kullanıcının bize göndereceği son dataları içeren yapı
    public int Id { get; set; }
    public int ProgrammingLanguageId { get; set; }
    public string Name { get; set; }

    public class UpdateProgrammingLanguageTechnologyCommandHandler : IRequestHandler<UpdateProgrammingLanguageTechnologyCommand, UpdatedProgrammingLanguageTechnologyDto>
    {
        private readonly IProgrammingLanguageTechnologyRepository _programmingLanguageTechnologyRepository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageTechnologyRules _programmingLanguageTechnologyRules;
        private readonly ProgrammingLanguageRules _programmingLanguageRules;

        public UpdateProgrammingLanguageTechnologyCommandHandler(IProgrammingLanguageTechnologyRepository programmingLanguageTechnologyRepository, IMapper mapper, ProgrammingLanguageTechnologyRules programmingLanguageTechnologyRules, ProgrammingLanguageRules programmingLanguageRules)
        {
            _programmingLanguageTechnologyRepository = programmingLanguageTechnologyRepository;
            _mapper = mapper;
            _programmingLanguageTechnologyRules = programmingLanguageTechnologyRules;
            _programmingLanguageRules = programmingLanguageRules;
        }

        public async Task<UpdatedProgrammingLanguageTechnologyDto> Handle(UpdateProgrammingLanguageTechnologyCommand request, CancellationToken cancellationToken)
        {
            ProgrammingLanguageTechnology? programmingLanguageTechnology = await _programmingLanguageTechnologyRepository.GetAsync(x => x.Id == request.Id); // Buna ilerde ihtiyacımız olabilir. Çünkü ilerde belirli alanları alır diğer alanları almazsam buradan dönmesini sağlayabilirim.

            await _programmingLanguageTechnologyRules.TechnologyShouldExistWhenRequested(request.Id);
            await _programmingLanguageRules.ProgrammingLanguageShouldExistWhenRequested(request.ProgrammingLanguageId);

            _mapper.Map(request, programmingLanguageTechnology);
            await _programmingLanguageTechnologyRules.TechnologyNameConNotBeDuplicatedWhenUpdated(programmingLanguageTechnology); // Güncelleme işleminden önce mapleme yapılması gerekir.

            ProgrammingLanguageTechnology updatedProgrammingLanguageTechnology = await _programmingLanguageTechnologyRepository.UpdateAsync(programmingLanguageTechnology);
            UpdatedProgrammingLanguageTechnologyDto mappedUpdatedProgrammingLanguageTechnologyDto = _mapper.Map<UpdatedProgrammingLanguageTechnologyDto>(updatedProgrammingLanguageTechnology);

            return mappedUpdatedProgrammingLanguageTechnologyDto;
        }
    }
}
