using asari.com.tr.Application.Features.Experiences.Commands.Create;
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
}