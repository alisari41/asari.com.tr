﻿using asari.com.tr.Application.Features.UserOperationClaims.Commands.Create;
using asari.com.tr.Application.Features.UserOperationClaims.Commands.Delete;
using asari.com.tr.Application.Features.UserOperationClaims.Commands.Update;
using asari.com.tr.Application.Features.UserOperationClaims.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace asari.com.tr.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserOperationClaimsController : BaseController
{
    [HttpGet("get-list")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListUserOperationClaimQuery getListUserOperationClaimQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListUserOperationClaimListItemDto> result = await Mediator.Send(getListUserOperationClaimQuery);
        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateUserOperationClaimCommand createUserOperationClaimCommand)
    {
        CreatedUserOperationClaimResponse result = await Mediator.Send(createUserOperationClaimCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.
        return Created("", result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateUserOperationClaimCommand updateUserOperationClaimCommand)
    {
        UpdatedUserOperationClaimResponse result = await Mediator.Send(updateUserOperationClaimCommand);
        return Created("", result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteUserOperationClaimCommand deleteProjectSkillCommand)
    {
        DeletedUserOperationClaimResponse result = await Mediator.Send(deleteProjectSkillCommand);
        return Ok(result);
    }
}