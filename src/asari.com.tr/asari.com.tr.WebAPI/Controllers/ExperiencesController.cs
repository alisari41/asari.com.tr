using asari.com.tr.Application.Features.Experiences.Queries.GetById;
using asari.com.tr.Application.Features.Experiences.Commands.Create;
using asari.com.tr.Application.Features.Experiences.Commands.Delete;
using asari.com.tr.Application.Features.Experiences.Commands.Update;
using asari.com.tr.Application.Features.Experiences.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace asari.com.tr.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExperiencesController : BaseController
{
    [HttpGet("get-list")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListExperienceQuery getListExperienceQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListExperienceListItemDto> result = await Mediator.Send(getListExperienceQuery);
        return Ok(result);
    }

    [HttpGet("getbyid/{Id}")] // Id parametresine ihtiyacımız olduğu için yapılıyor. route dan alacağı için FromRoute kullanılır
    public async Task<IActionResult> GetById([FromRoute] GetByIdExperienceQuery getByIdExperienceQuery)
    {
        var result = await Mediator.Send(getByIdExperienceQuery);
        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateExperienceCommand createExperienceCommand)
    {
        CreatedExperienceResponse result = await Mediator.Send(createExperienceCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.
        return Created("", result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateExperienceCommand updateExperienceCommand)
    {
        UpdatedExperienceResponse result = await Mediator.Send(updateExperienceCommand);
        return Created("", result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteExperienceCommand deleteExperienceCommand)
    {
        DeletedExperienceResponse result = await Mediator.Send(deleteExperienceCommand);
        return Ok(result);
    }
}