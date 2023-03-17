using asari.com.tr.Application.Features.Skills.Queries.GetList;
using asari.com.tr.Application.Features.Technologies.Queries.GetList;
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

}