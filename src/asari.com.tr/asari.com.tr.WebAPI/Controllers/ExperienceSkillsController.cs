using asari.com.tr.Application.Features.ExperienceSkills.Commands.Create;
using asari.com.tr.Application.Features.ExperienceSkills.Commands.Delete;
using asari.com.tr.Application.Features.ExperienceSkills.Commands.Update;
using asari.com.tr.Application.Features.ExperienceSkills.Queries.GetList;
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

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateExperienceSkillCommand createExperienceSkillCommand)
    {
        CreatedExperienceSkillResponse result = await Mediator.Send(createExperienceSkillCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.
        return Created("", result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateExperienceSkillCommand updateExperienceSkillCommand)
    {
        UpdatedExperienceSkillResponse result = await Mediator.Send(updateExperienceSkillCommand);
        return Created("", result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteExperienceSkillCommand deleteProjectSkillCommand)
    {
        DeletedExperienceSkillResponse result = await Mediator.Send(deleteProjectSkillCommand);
        return Ok(result);
    }
}