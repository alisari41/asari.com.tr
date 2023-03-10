using asari.com.tr.Application.Features.Projects.Commands.Create;
using asari.com.tr.Application.Features.Technologies.Commands.Create;
using asari.com.tr.Application.Features.Technologies.Queries.GetById;
using asari.com.tr.Application.Features.Technologies.Queries.GetList;
using asari.com.tr.Application.Features.Technologies.Queries.GetListByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
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

    [HttpGet("getbyid/{Id}")] // Id parametresine ihtiyacımız olduğu için yapılıyor. route dan alacağı için FromRoute kullanılır
    public async Task<IActionResult> GetById([FromRoute] GetByIdTechnologyQuery getByIdTechnologyQuery) // route'daki Id ile GetByIdTechnologyQuery Id işlemini mapleme yapacak. Id yazılımları aynı olmak zorunda 
    {
        var result = await Mediator.Send(getByIdTechnologyQuery);
        return Ok(result);
    }

    [HttpPost("GetList/ByDynamic")]
    public async Task<ActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic) // Dynamic olduğu için HttpPost kullanıldı
    {
        GetListByDynamicTechnologyQuery getListByDynamicTechnologyQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };

        GetListResponse<GetListByDynamicTechnologyListItemDto> result = await Mediator.Send(getListByDynamicTechnologyQuery);
        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateTechnologyCommand createTechnologyCommand)
    {
        var result = await Mediator.Send(createTechnologyCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.
        return Created("", result);
    }
}