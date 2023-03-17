using asari.com.tr.Application.Features.Experiences.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace asari.com.tr.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExperiencesController : BaseController
{
    [HttpGet("get-list")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListExperienceQuery getListExperienceQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListExperienceListItemDto> result = await Mediator.Send(getListExperienceQuery);
        return Ok(result);
    }
}