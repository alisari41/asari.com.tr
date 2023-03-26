using asari.com.tr.Application.Features.LicensesAndCertifications.Commands.Create;
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

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateLicenseAndCertificationCommand createLicenseAndCertificationCommand)
    {
        CreatedLicenseAndCertificationResponse result = await Mediator.Send(createLicenseAndCertificationCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.
        return Created("", result);
    }
}