﻿using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Constants;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using static asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Constants.ProgrammingLanguageTechnologiesOperationClaims;

namespace asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Commands.Delete;

public class DeleteProgrammingLanguageTechnologyCommand : IRequest<DeletedProgrammingLanguageTechnologyResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { CacheGroupKeyValue.ProgrammingLanguageTechnologyCacheGroupKey };

    public string[] Roles => new[] { Admin, Write, ProgrammingLanguageTechnologiesOperationClaims.Delete };

    public class DeleteProgrammingLanguageTechnologyCommandHandler : IRequestHandler<DeleteProgrammingLanguageTechnologyCommand, DeletedProgrammingLanguageTechnologyResponse>
    {
        private readonly IProgrammingLanguageTechnologyRepository _programmingLanguageTechnologyRepository;
        private readonly IMapper _mapper;
        private readonly ProgrammingLanguageTechnologyBusinessRules _programmingLanguageTechnologyRules;

        public DeleteProgrammingLanguageTechnologyCommandHandler(IProgrammingLanguageTechnologyRepository programmingLanguageTechnologyRepository, IMapper mapper, ProgrammingLanguageTechnologyBusinessRules programmingLanguageTechnologyRules)
        {
            _programmingLanguageTechnologyRepository = programmingLanguageTechnologyRepository;
            _mapper = mapper;
            _programmingLanguageTechnologyRules = programmingLanguageTechnologyRules;
        }

        public async Task<DeletedProgrammingLanguageTechnologyResponse> Handle(DeleteProgrammingLanguageTechnologyCommand request, CancellationToken cancellationToken)
        {
            ProgrammingLanguageTechnology? programmingLanguageTechnology = await _programmingLanguageTechnologyRepository.GetAsync(x => x.Id == request.Id); // Silinecek Verinin Adını almak için kullanılmıştır.
            await _programmingLanguageTechnologyRules.ProgrammingLanguageTechnologyShouldExistWhenRequested(request.Id);

            //ProgrammingLanguageTechnology mappedProgrammingLanguageTechnology = _mapper.Map<ProgrammingLanguageTechnology>(request);
            _mapper.Map(request, programmingLanguageTechnology);
            ProgrammingLanguageTechnology deletedProgrammingLanguageTechnology = await _programmingLanguageTechnologyRepository.DeleteAsync(programmingLanguageTechnology);
            DeletedProgrammingLanguageTechnologyResponse mappedDeletedProgrammingLanguageTechnologyDto = _mapper.Map<DeletedProgrammingLanguageTechnologyResponse>(deletedProgrammingLanguageTechnology);

            return mappedDeletedProgrammingLanguageTechnologyDto;
        }
    }
}