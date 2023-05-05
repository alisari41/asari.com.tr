using asari.com.tr.Application.Features.EducationSkills.Commands.Create;
using asari.com.tr.Application.Features.EducationSkills.Commands.Delete;
using asari.com.tr.Application.Features.EducationSkills.Commands.Update;
using asari.com.tr.Application.Features.EducationSkills.Queries.GetById;
using asari.com.tr.Application.Features.EducationSkills.Queries.GetList;
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

    [HttpGet("getbyid/{Id}")] // Id parametresine ihtiyacımız olduğu için yapılıyor. route dan alacağı için FromRoute kullanılır
    public async Task<IActionResult> GetById([FromRoute] GetByIdEducationSkillQuery getByIdEducationSkillQuery)
    {
        var result = await Mediator.Send(getByIdEducationSkillQuery);
        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateEducationSkillCommand createEducationSkillCommand)
    {
        CreatedEducationSkillResponse result = await Mediator.Send(createEducationSkillCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.
        return Created("", result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateEducationSkillCommand updateEducationSkillCommand)
    {
        UpdatedEducationSkillResponse result = await Mediator.Send(updateEducationSkillCommand);
        return Created("", result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteEducationSkillCommand deleteEducationSkillCommand)
    {
        DeletedEducationSkillResponse result = await Mediator.Send(deleteEducationSkillCommand);
        return Ok(result);
    }
}