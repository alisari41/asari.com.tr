﻿using asari.com.tr.Application.Features.Educations.Commands.Create;
using asari.com.tr.Application.Features.Educations.Commands.Delete;
using asari.com.tr.Application.Features.Educations.Commands.Update;
using asari.com.tr.Application.Features.Educations.Queries.GetList;
using asari.com.tr.Application.Features.Educations.Queries.GetById;
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

    [HttpGet("getbyid/{Id}")] // Id parametresine ihtiyacımız olduğu için yapılıyor. route dan alacağı için FromRoute kullanılır
    public async Task<IActionResult> GetById([FromRoute] GetByIdEducationQuery getByIdEducationQuery)
    {
        var result = await Mediator.Send(getByIdEducationQuery);
        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateEducationCommand createEducationCommand)
    {
        CreatedEducationResponse result = await Mediator.Send(createEducationCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.
        return Created("", result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateEducationCommand updateEducationCommand)
    {
        UpdatedEducationResponse result = await Mediator.Send(updateEducationCommand);
        return Created("", result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteEducationCommand deleteEducationCommand)
    {
        DeletedEducationResponse result = await Mediator.Send(deleteEducationCommand);
        return Ok(result);
    }
}