using asari.com.tr.Application.Features.TechnologyProjects.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.TechnologyProjects.Commands.Delete;

public class DeleteTechnologyProjectCommand : IRequest<DeletedTechnologyProjectResponse>
{
    public int Id { get; set; }

    public class DeleteTechnologyProjectCommandHandler : IRequestHandler<DeleteTechnologyProjectCommand, DeletedTechnologyProjectResponse>
    {
        private readonly ITechnologyProjectRepository _technologyProjectRepository;
        private readonly IMapper _mapper;
        private readonly TechnologyProjectBusinessRules _technologyProjectBusinessRules;

        public DeleteTechnologyProjectCommandHandler(ITechnologyProjectRepository technologyProjectRepository, IMapper mapper, TechnologyProjectBusinessRules technologyProjectBusinessRules)
        {
            _technologyProjectRepository = technologyProjectRepository;
            _mapper = mapper;
            _technologyProjectBusinessRules = technologyProjectBusinessRules;
        }

        public async Task<DeletedTechnologyProjectResponse> Handle(DeleteTechnologyProjectCommand request, CancellationToken cancellationToken)
        {
            TechnologyProject? technologyProject = await _technologyProjectRepository.GetAsync(x => x.Id == request.Id);

            await _technologyProjectBusinessRules.TechnologyProjectShouldExistWhenRequested(request.Id);

            _mapper.Map(request, technologyProject);
            TechnologyProject deletedTechnologyProject = await _technologyProjectRepository.DeleteAsync(technologyProject);
            DeletedTechnologyProjectResponse mappedDeletedTechnologyProjectResponse = _mapper.Map<DeletedTechnologyProjectResponse>(deletedTechnologyProject);

            return mappedDeletedTechnologyProjectResponse;
        }
    }
}