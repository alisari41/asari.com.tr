using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Queries.GetById;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Rules;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Queries.GetById;

public class GetByIdProgrammingLanguageTechnologyQuery : IRequest<GetByIdProgrammingLanguageTechnologyGetByIdResponse>
{
    public int Id { get; set; }

    public class GetByIdProgrammingLanguageTechnologyQueryHandler : IRequestHandler<GetByIdProgrammingLanguageTechnologyQuery, GetByIdProgrammingLanguageTechnologyGetByIdResponse>
    {
        private readonly IProgrammingLanguageTechnologyRepository _programmingLanguageTechnologyRepository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageTechnologyBusinessRules _programmingLanguageTechnologyBusinessRules;

        public GetByIdProgrammingLanguageTechnologyQueryHandler(IProgrammingLanguageTechnologyRepository programmingLanguageTechnologyRepository, IMapper mapper, ProgrammingLanguageTechnologyBusinessRules programmingLanguageTechnologyRules)
        {
            _programmingLanguageTechnologyRepository = programmingLanguageTechnologyRepository;
            _mapper = mapper;
            _programmingLanguageTechnologyBusinessRules = programmingLanguageTechnologyRules;
        }

        public async Task<GetByIdProgrammingLanguageTechnologyGetByIdResponse> Handle(GetByIdProgrammingLanguageTechnologyQuery request, CancellationToken cancellationToken)
        {
            ProgrammingLanguageTechnology? ProgrammingLanguageTechnology = await _programmingLanguageTechnologyRepository.GetAsync(x => x.Id == request.Id,
                                                                                                                                        include: i =>
                                                                                                                                            i.Include(c => c.ProgrammingLanguage));
            _programmingLanguageTechnologyBusinessRules.ProgrammingLanguageTechnologyShouldExistWhenRequested(ProgrammingLanguageTechnology);

            GetByIdProgrammingLanguageTechnologyGetByIdResponse mappedGetByIdProgrammingLanguageTechnologyGetByIdResponse = _mapper.Map<GetByIdProgrammingLanguageTechnologyGetByIdResponse>(ProgrammingLanguageTechnology);

            return mappedGetByIdProgrammingLanguageTechnologyGetByIdResponse;
        }
    }
}