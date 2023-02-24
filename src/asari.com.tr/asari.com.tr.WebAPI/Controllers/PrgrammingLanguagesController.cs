using asari.com.tr.Application.Features.ProgrammingLanguages.Models;
using asari.com.tr.Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage;
using Core.Application.Requests;
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

        ProgrammingLanguageListModel result = await Mediator.Send(getListProgrammingLanguageQuery);
        return Ok(result);
    }
}