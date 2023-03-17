using asari.com.tr.Application.Features.ProjectSkills.Commands.Create;
using asari.com.tr.Application.Features.ProjectSkills.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace asari.com.tr.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectSkillsController : BaseController
{

    [HttpGet("get-list")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListProjectSkillQuery getListProjectSkillQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListProjectSkillListItemDto> result = await Mediator.Send(getListProjectSkillQuery);
        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateProjectSkillCommand createProjectSkillCommand)
    {
        CreatedProjectSkillResponse result = await Mediator.Send(createProjectSkillCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.
        return Created("", result);
    }
}