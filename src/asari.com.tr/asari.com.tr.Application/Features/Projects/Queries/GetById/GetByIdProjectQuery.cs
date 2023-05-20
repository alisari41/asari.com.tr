using asari.com.tr.Application.Features.Projects.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using MediatR;

namespace asari.com.tr.Application.Features.Projects.Queries.GetById;

public class GetByIdProjectQuery : IRequest<GetByIdProjectGetByIdResponse>
{
    public int Id { get; set; }

    public class GetByIdProjectQueryHandler : IRequestHandler<GetByIdProjectQuery, GetByIdProjectGetByIdResponse>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly ProjectBusinessRules _projectRules;

        public GetByIdProjectQueryHandler(IProjectRepository projectRepository, IMapper mapper, ProjectBusinessRules projectRules)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _projectRules = projectRules;
        }

        public async Task<GetByIdProjectGetByIdResponse> Handle(GetByIdProjectQuery request, CancellationToken cancellationToken)
        {
            Project? projects = await _projectRepository.GetAsync(x => x.Id == request.Id);

            await _projectRules.ProjectShouldExistWhenRequested(request.Id);

            GetByIdProjectGetByIdResponse mappedProjectGetByIdDto = _mapper.Map<GetByIdProjectGetByIdResponse>(projects);

            return mappedProjectGetByIdDto;
        }
    }
}