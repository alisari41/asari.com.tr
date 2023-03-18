using asari.com.tr.Application.Features.Educations.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace asari.com.tr.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EducationsController : BaseController
{
    [HttpGet("get-list")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListEducationQuery getListEducationQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListEducationListItemDto> result = await Mediator.Send(getListEducationQuery);
        return Ok(result);
    }
}