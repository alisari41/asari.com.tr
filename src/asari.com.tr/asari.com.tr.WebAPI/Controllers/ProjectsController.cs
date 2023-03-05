using asari.com.tr.Application.Features.Projects.Commands.Create;
using asari.com.tr.Application.Features.Projects.Commands.Delete;
using asari.com.tr.Application.Features.Projects.Commands.Update;
using asari.com.tr.Application.Features.Projects.Queries.GetById;
using asari.com.tr.Application.Features.Projects.Queries.GetList;
using asari.com.tr.Application.Features.Projects.Queries.GetListByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace asari.com.tr.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : BaseController
{
    [HttpGet("get-list")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListProjectQuery getListProjectQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListProjectListItemDto> result = await Mediator.Send(getListProjectQuery);
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

        GetListResponse<GetListProjectListItemDto> result = await Mediator.Send(getListProjectByDynamicQuery);
        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateProjectCommand createProjectCommand)
    {
        var result = await Mediator.Send(createProjectCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.
        return Created("", result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateProjectCommand updateProjectCommand)
    {
        var result = await Mediator.Send(updateProjectCommand);
        return Created("", result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteProjectCommand deleteProjectCommand)
    {
        var result = await Mediator.Send(deleteProjectCommand);
        return Ok(result);
    }
}