using asari.com.tr.Application.Features.ProgrammingLanguages.Queries.GetByIdProgrammingLanguage;
using asari.com.tr.Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage;
using asari.com.tr.Application.Features.Projects.Queries.GetByIdProject;
using asari.com.tr.Application.Features.Projects.Queries.GetListProjectByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace asari.com.tr.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : BaseController
{
    [HttpGet("get-list")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListProgrammingLanguageQuery getListProgrammingLanguageQuery = new() { PageRequest = pageRequest };

        var result = await Mediator.Send(getListProgrammingLanguageQuery);
        return Ok(result);
    }

    [HttpGet("getbyid/{Id}")] // Id parametresine ihtiyacımız olduğu için yapılıyor. route dan alacağı için FromRoute kullanılır
    public async Task<IActionResult> GetById([FromRoute] GetByIdProjectQuery getByIdProjectQuery) // route'daki Id ile GetByIdProjectQuery Id işlemini mapleme yapacak. Id yazılımları aynı olmak zorunda 
    {
        var result = await Mediator.Send(getByIdProjectQuery);
        return Ok(result);
    }

    [HttpPost("GetList/ByDynamic")]
    public async Task<ActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic) // Dynamic olduğu için HttpPost kullanıldı
    {
        GetListProjectByDynamicQuery getListProjectByDynamicQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };

        var result = await Mediator.Send(getListProjectByDynamicQuery);
        return Ok(result);
    }
}