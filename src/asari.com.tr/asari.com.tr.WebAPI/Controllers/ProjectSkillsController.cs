using asari.com.tr.Application.Features.ProjectSkills.Commands.Create;
using asari.com.tr.Application.Features.ProjectSkills.Commands.Delete;
using asari.com.tr.Application.Features.ProjectSkills.Commands.Update;
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

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateProjectSkillCommand updateProjectSkillCommand)
    {
        UpdatedProjectSkillResponse result = await Mediator.Send(updateProjectSkillCommand);
        return Created("", result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteProjectSkillCommand deleteProjectSkillCommand)
    {
        DeletedProjectSkillResponse result = await Mediator.Send(deleteProjectSkillCommand);
        return Ok(result);
    }
}