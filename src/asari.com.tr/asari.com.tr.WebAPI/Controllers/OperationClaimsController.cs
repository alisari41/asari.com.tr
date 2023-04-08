using asari.com.tr.Application.Features.OperationClaims.Commands.Create;
using asari.com.tr.Application.Features.OperationClaims.Queries.GetList;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace asari.com.tr.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OperationClaimsController : BaseController
{
    [HttpGet("getList")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListOperationClaimQuery getListOperationClaimQuery = new() { PageRequest = pageRequest }; // Bu yeni kullanımdır eski hali aşağıdaki gibidir.
                                                                                                     // GetListBrandQuery getListBrandQuery = new GetListBrandQuery();
                                                                                                     // getListBrandQuery.PageRequest = pageRequest;

        var result = await Mediator.Send(getListOperationClaimQuery);
        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateOperationClaimCommand createOperationClaimCommand)
    {
        var result = await Mediator.Send(createOperationClaimCommand);
        return Created("", result);
    }
}