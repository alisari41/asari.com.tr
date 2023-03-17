using asari.com.tr.Application.Features.Skills.Commands.Create;
using asari.com.tr.Application.Features.Skills.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace asari.com.tr.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SkillsController : BaseController
{

    [HttpGet("get-list")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListSkillQuery getListSkillQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListSkillListItemDto> result = await Mediator.Send(getListSkillQuery);
        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateSkillCommand createSkillCommand)
    {
        CreatedSkillResponse result = await Mediator.Send(createSkillCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.
        return Created("", result);
    }
}