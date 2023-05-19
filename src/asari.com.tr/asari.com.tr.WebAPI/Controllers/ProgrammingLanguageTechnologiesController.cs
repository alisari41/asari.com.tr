using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Queries.GetById;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Commands.Create;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Commands.Delete;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Commands.Update;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Queries.GetList;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Queries.GetListByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
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

        GetListResponse<GetListProgrammingLanguageTechnologyListItemDto> result = await Mediator.Send(getListPrgrammingLanguageTecnologyQuery);
        return Ok(result);
    }

    [HttpGet("getbyid/{Id}")] // Id parametresine ihtiyacımız olduğu için yapılıyor. route dan alacağı için FromRoute kullanılır
    public async Task<IActionResult> GetById([FromRoute] GetByIdProgrammingLanguageTechnologyQuery getByIdProgrammingLanguageTechnologyQuery)
    {
        var result = await Mediator.Send(getByIdProgrammingLanguageTechnologyQuery);
        return Ok(result);
    }

    [HttpPost("GetList/ByDynamic")]
    public async Task<ActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic) // Dynamic olduğu için HttpPost kullanıldı
    {
        GetListProgrammingLanuguageTechnologyByDynamicQuery getListProgrammingLanuguageTechnologyByDynamicQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };

        GetListResponse<GetListProgrammingLanguageTechnologyListItemDto> result = await Mediator.Send(getListProgrammingLanuguageTechnologyByDynamicQuery);
        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateProgrammingLanguageTechnologyCommand createProgrammingLanguageTechnologyCommand)
    {
        var result = await Mediator.Send(createProgrammingLanguageTechnologyCommand);
        return Created("", result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateProgrammingLanguageTechnologyCommand updateProgrammingLanguageTechnologyCommand)
    {
        var result = await Mediator.Send(updateProgrammingLanguageTechnologyCommand);
        return Created("", result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteProgrammingLanguageTechnologyCommand deleteProgrammingLanguageTechnologyCommand)
    {
        var result = await Mediator.Send(deleteProgrammingLanguageTechnologyCommand);
        return Ok(result);
    }
}