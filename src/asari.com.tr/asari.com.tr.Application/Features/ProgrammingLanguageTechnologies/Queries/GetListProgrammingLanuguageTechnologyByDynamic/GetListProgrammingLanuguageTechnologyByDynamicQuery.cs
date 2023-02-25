using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Models;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Queries.GetListProgrammingLanuguageTechnologyByDynamic;

public class GetListProgrammingLanuguageTechnologyByDynamicQuery : IRequest<ProgrammingLanguageTechnologyListModel>
{
    public Dynamic Dynamic { get; set; } // Dinamik sorgu yazmak için kullanacağız
    public PageRequest PageRequest { get; set; }

    public class GetListProgrammingLanuguageTechnologyByDynamicQueryJandler : IRequestHandler<GetListProgrammingLanuguageTechnologyByDynamicQuery, ProgrammingLanguageTechnologyListModel>
    {
        private readonly IProgrammingLanguageTechnologyRepository _programmingLanguageTechnologyRepository;
        private readonly IMapper _mapper;

        public GetListProgrammingLanuguageTechnologyByDynamicQueryJandler(IProgrammingLanguageTechnologyRepository programmingLanguageTechnologyRepository, IMapper mapper)
        {
            _programmingLanguageTechnologyRepository = programmingLanguageTechnologyRepository;
            _mapper = mapper;
        }

        public async Task<ProgrammingLanguageTechnologyListModel> Handle(GetListProgrammingLanuguageTechnologyByDynamicQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ProgrammingLanguageTechnology> programmingLanguageTechnologies = await _programmingLanguageTechnologyRepository.GetListByDynamicAsync( // Dinamik Sorgu
                                                                                                    request.Dynamic,
                                                                                                    include: x => x.Include(c => c.ProgrammingLanguage), // Hangi tablodan veri alınacaksa
                                                                                                    index: request.PageRequest.Page,
                                                                                                    size: request.PageRequest.PageSize);

            ProgrammingLanguageTechnologyListModel mappedProgrammingLanguageTechnologyListModel = _mapper.Map<ProgrammingLanguageTechnologyListModel>(programmingLanguageTechnologies);

            return mappedProgrammingLanguageTechnologyListModel;
        }
    }
}