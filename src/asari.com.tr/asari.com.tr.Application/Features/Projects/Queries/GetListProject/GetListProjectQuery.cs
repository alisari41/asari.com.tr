using asari.com.tr.Application.Features.Projects.Dtos;
using asari.com.tr.Application.Features.Projects.Models;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;

namespace asari.com.tr.Application.Features.Projects.Queries.GetListProject;

public class GetListProjectQuery : IRequest<ProjectListModel>
{
    public PageRequest PageRequest { get; set; } // Bir listeleme yapılacağı için bir Request üzerinden geçekleştirilecek

    public class GetListProjectQueryHandler : IRequestHandler<GetListProjectQuery, ProjectListModel>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetListProjectQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ProjectListModel> Handle(GetListProjectQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Project> projects = await _projectRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

            ProjectListModel mappedProjectListModel = _mapper.Map<ProjectListModel>(projects);

            return mappedProjectListModel;
        }
    }
}