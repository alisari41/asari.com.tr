using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Queries.GetListProgrammingLanguageTechnology;
using Core.Application.Requests;
using MediatR;
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
}
