using asari.com.tr.Application.Features.ExperienceSkills.Queries.GetList;
using asari.com.tr.Application.Features.ProjectSkills.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace asari.com.tr.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExperienceSkillsController : BaseController
{
    [HttpGet("get-list")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListExperienceSkillQuery getListExperienceSkillQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListExperienceSkillListItemDto> result = await Mediator.Send(getListExperienceSkillQuery);
        return Ok(result);
    }
}