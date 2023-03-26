using asari.com.tr.Application.Features.EducationSkills.Queries.GetList;
using asari.com.tr.Application.Features.ExperienceSkills.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace asari.com.tr.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EducationSkillsController : BaseController
{
    [HttpGet("get-list")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListEducationSkillQuery getListEducationSkillQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListEducationSkillListItemDto> result = await Mediator.Send(getListEducationSkillQuery);
        return Ok(result);
    }
}