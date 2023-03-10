using asari.com.tr.Application.Features.Technologies.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace asari.com.tr.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TechnologiesController : BaseController
{

    [HttpGet("get-list")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListTechnologyQuery getListTechnologyQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListTechnologyListItemDto> result = await Mediator.Send(getListTechnologyQuery);
        return Ok(result);
    }
}