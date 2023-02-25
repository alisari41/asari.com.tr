using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Models;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Queries.GetListProgrammingLanguageTechnology;

public class GetListProgrammingLanguageTechnologyQuery: IRequest<ProgrammingLanguageTechnologyListModel>
{
    public PageRequest PageRequest { get; set; } // Bir listeleme yapılacağı için bir Request üzerinden geçekleştirilecek

    public class GetListProgrammingLanguageTechnologyQueryQueryHandler : IRequestHandler<GetListProgrammingLanguageTechnologyQuery, ProgrammingLanguageTechnologyListModel>
    {
        // IRequestHandler<GetListTechnologyQuery, TechnologyListModel> bu satır amacı GetListTechnologyQuery bunu gönderildiğinde hangi Handler çalışıcak 

        public readonly IProgrammingLanguageTechnologyRepository _programmingLanguageTechnolojyRepository;
        public readonly IMapper _mapper;

        public GetListProgrammingLanguageTechnologyQueryQueryHandler(IProgrammingLanguageTechnologyRepository programmingLanguageTechnolojyRepository, IMapper mapper)
        {
            _programmingLanguageTechnolojyRepository = programmingLanguageTechnolojyRepository;
            _mapper = mapper;
        }

        public async Task<ProgrammingLanguageTechnologyListModel> Handle(GetListProgrammingLanguageTechnologyQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ProgrammingLanguageTechnology> programmingLanguageTechnologies = await _programmingLanguageTechnolojyRepository.GetListAsync(include:
                                                                                            x => x.Include(c => c.ProgrammingLanguage),   // Include işlemi ilişkilendirme için
                                                                                            index: request.PageRequest.Page,
                                                                                            size: request.PageRequest.PageSize); // Birden fazla ilişkide yapılabilir. Github Projesinden bakılabilir. Linkedinde paylaşıldı.

            ProgrammingLanguageTechnologyListModel mappedProgrammingLanguageTechnologyListModel = _mapper.Map<ProgrammingLanguageTechnologyListModel>(programmingLanguageTechnologies);

            return mappedProgrammingLanguageTechnologyListModel;
        }
    }
}
