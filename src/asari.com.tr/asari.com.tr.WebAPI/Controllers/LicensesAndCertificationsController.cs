using asari.com.tr.Application.Features.LicensesAndCertifications.Commands.Create;
using asari.com.tr.Application.Features.LicensesAndCertifications.Commands.Delete;
using asari.com.tr.Application.Features.LicensesAndCertifications.Commands.Update;
using asari.com.tr.Application.Features.LicensesAndCertifications.Queries.GetById;
using asari.com.tr.Application.Features.LicensesAndCertifications.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace asari.com.tr.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LicensesAndCertificationsController : BaseController
{
    [HttpGet("get-list")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListLicenseAndCertificationQuery getListLicenseAndCertificationQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListLicenseAndCertificationListItemDto> result = await Mediator.Send(getListLicenseAndCertificationQuery);
        return Ok(result);
    }

    [HttpGet("getbyid/{Id}")] // Id parametresine ihtiyacımız olduğu için yapılıyor. route dan alacağı için FromRoute kullanılır
    public async Task<IActionResult> GetById([FromRoute] GetByIdLicenseAndCertificationQuery getByIdLicenseAndCertificationQuery)
    {
        var result = await Mediator.Send(getByIdLicenseAndCertificationQuery);
        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateLicenseAndCertificationCommand createLicenseAndCertificationCommand)
    {
        CreatedLicenseAndCertificationResponse result = await Mediator.Send(createLicenseAndCertificationCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.
        return Created("", result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateLicenseAndCertificationCommand updateLicenseAndCertificationCommand)
    {
        UpdatedLicenseAndCertificationResponse result = await Mediator.Send(updateLicenseAndCertificationCommand);
        return Created("", result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteLicenseAndCertificationCommand deleteLicenseAndCertificationCommand)
    {
        DeletedLicenseAndCertificationResponse result = await Mediator.Send(deleteLicenseAndCertificationCommand);
        return Ok(result);
    }
}