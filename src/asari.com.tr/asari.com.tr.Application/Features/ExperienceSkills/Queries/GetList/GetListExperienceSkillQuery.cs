using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.ExperienceSkills.Queries.GetList;

public class GetListExperienceSkillQuery : IRequest<GetListResponse<GetListExperienceSkillListItemDto>>
{
    public PageRequest PageRequest { get; set; } // Bir listeleme yapılacağı için bir Request üzerinden geçekleştirilecek

    public class GetListExperienceSkillQueryHandler : IRequestHandler<GetListExperienceSkillQuery, GetListResponse<GetListExperienceSkillListItemDto>>
    {
        private readonly IExperienceSkillRepository _experienceSkillRepository;
        private readonly IMapper _mapper;

        public GetListExperienceSkillQueryHandler(IExperienceSkillRepository experienceSkillRepository, IMapper mapper)
        {
            _experienceSkillRepository = experienceSkillRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListExperienceSkillListItemDto>> Handle(GetListExperienceSkillQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ExperienceSkill> experienceSkill = await _experienceSkillRepository.GetListAsync(include: x =>
                                                                    x.Include(c => c.Experience)
                                                                     .Include(c => c.Skill),
                                                                    index: request.PageRequest.Page,
                                                                    size: request.PageRequest.PageSize);

            GetListResponse<GetListExperienceSkillListItemDto> mappedGetListExperienceSkillListItemDto = _mapper.Map<GetListResponse<GetListExperienceSkillListItemDto>>(experienceSkill);

            return mappedGetListExperienceSkillListItemDto;
        }
    }
}