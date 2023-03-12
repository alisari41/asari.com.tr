using asari.com.tr.Application.Features.TechnologyProjects.Commands.Create;
using asari.com.tr.Application.Features.TechnologyProjects.Commands.Update;
using asari.com.tr.Application.Features.TechnologyProjects.Queries.GetById;
using asari.com.tr.Application.Features.TechnologyProjects.Queries.GetList;
using asari.com.tr.Application.Features.TechnologyProjects.Queries.GetListByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
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

    [HttpGet("getbyid/{Id}")] // Id parametresine ihtiyacımız olduğu için yapılıyor. route dan alacağı için FromRoute kullanılır
    public async Task<IActionResult> GetById([FromRoute] GetByIdTechnologyProjectQuery getByIdTechnologyProjectQuery)
    {
        var result = await Mediator.Send(getByIdTechnologyProjectQuery);
        return Ok(result);
    }

    [HttpPost("GetList/ByDynamic")]
    public async Task<ActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic) // Dynamic olduğu için HttpPost kullanıldı
    {
        GetListByDynamicTechnologyProjectQuery getListByDynamicTechnologyProjectQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };

        GetListResponse<GetListByDynamicTechnologyProjectListItemDto> result = await Mediator.Send(getListByDynamicTechnologyProjectQuery);
        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateTechnologyProjectCommand createTechnologyProjectCommand)
    {
        CreatedTechnologyProjectResponse result = await Mediator.Send(createTechnologyProjectCommand);
        return Created("", result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateTechnologyProjectCommand updateTechnologyProjectCommand)
    {
        UpdatedTechnologyProjectResponse result = await Mediator.Send(updateTechnologyProjectCommand);
        return Created("", result);
    }
}