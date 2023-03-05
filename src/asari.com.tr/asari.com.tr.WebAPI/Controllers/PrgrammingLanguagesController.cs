using asari.com.tr.Application.Features.ProgrammingLanguages.Commands.Create;
using asari.com.tr.Application.Features.ProgrammingLanguages.Commands.Delete;
using asari.com.tr.Application.Features.ProgrammingLanguages.Commands.Update;
using asari.com.tr.Application.Features.ProgrammingLanguages.Queries.GetById;
using asari.com.tr.Application.Features.ProgrammingLanguages.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace asari.com.tr.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrgrammingLanguagesController : BaseController
{
    [HttpGet("getList")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListProgrammingLanguageQuery getListProgrammingLanguageQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListProgrammingLanguageListItemDto> result = await Mediator.Send(getListProgrammingLanguageQuery);
        return Ok(result);
    }

    [HttpGet("getbyid/{Id}")] // Id parametresine ihtiyacımız olduğu için yapılıyor. route dan alacağı için FromRoute kullanılır
    public async Task<IActionResult> GetById([FromRoute] GetByIdProgrammingLanguageQuery getByIdProgrammingLanguageQuery)
    {
        var result = await Mediator.Send(getByIdProgrammingLanguageQuery);
        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateProgrammingLanguageCommand createProgrammingLanguageCommand)
    {
        var result = await Mediator.Send(createProgrammingLanguageCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.
        return Created("", result);
    }

    [HttpPut("update")] // Commandden Id geleceği için tekrar /{Id} yazmaya gerek yok
    public async Task<IActionResult> Update([FromBody] UpdateProgrammingLanguageCommand updateProgrammingLanguageCommand)
    {
        var result = await Mediator.Send(updateProgrammingLanguageCommand);

        return Created("", result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteProgrammingLanguageCommand deleteProgrammingLanguageCommand)
    {
        var result = await Mediator.Send(deleteProgrammingLanguageCommand);

        return Ok(result);
    }
}