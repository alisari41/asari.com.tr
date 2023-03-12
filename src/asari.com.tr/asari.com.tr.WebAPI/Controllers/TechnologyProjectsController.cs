using asari.com.tr.Application.Features.Technologies.Queries.GetList;
using asari.com.tr.Application.Features.TechnologyProjects.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace asari.com.tr.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TechnologyProjectsController : BaseController
{

    [HttpGet("get-list")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListTechnologyProjectQuery getListTechnologyProjectQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListTechnologyProjectListItemDto> result = await Mediator.Send(getListTechnologyProjectQuery);
        return Ok(result);
    }
}