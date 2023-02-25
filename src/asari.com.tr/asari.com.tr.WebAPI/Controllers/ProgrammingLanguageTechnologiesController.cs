using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Commands.CreateProgrammingLanguageTechnology;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Queries.GetListProgrammingLanguageTechnology;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Queries.GetListProgrammingLanuguageTechnologyByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace asari.com.tr.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProgrammingLanguageTechnologiesController : BaseController
{
    [HttpGet("get-list")]
    public async Task<ActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListProgrammingLanguageTechnologyQuery getListPrgrammingLanguageTecnologyQuery = new() { PageRequest = pageRequest }; // Bu yeni kullanımdır eski hali aşağıdaki gibidir.
                                                                                            // GetListBrandQuery getListBrandQuery = new GetListBrandQuery();
                                                                                            // getListBrandQuery.PageRequest = pageRequest;

        var result = await Mediator.Send(getListPrgrammingLanguageTecnologyQuery);
        return Ok(result);
    }

    [HttpPost("GetList/ByDynamic")]
    public async Task<ActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic) // Dynamic olduğu için HttpPost kullanıldı
    {
        GetListProgrammingLanuguageTechnologyByDynamicQuery getListProgrammingLanuguageTechnologyByDynamicQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };

        var result = await Mediator.Send(getListProgrammingLanuguageTechnologyByDynamicQuery);
        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateProgrammingLanguageTechnologyCommand createProgrammingLanguageTechnologyCommand)
    {
        var result = await Mediator.Send(createProgrammingLanguageTechnologyCommand);
        return Created("", result);
    }
}