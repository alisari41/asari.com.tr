using asari.com.tr.Application.Features.LicenseAndCertificationSkills.Command.Create;
using asari.com.tr.Application.Features.LicenseAndCertificationSkills.Command.Delete;
using asari.com.tr.Application.Features.LicenseAndCertificationSkills.Command.Update;
using asari.com.tr.Application.Features.LicenseAndCertificationSkills.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace asari.com.tr.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LicenseAndCertificationSkillsController : BaseController
{
    [HttpGet("get-list")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListLicenseAndCertificationSkillQuery getListLicenseAndCertificationSkillQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListLicenseAndCertificationSkillListItemDto> result = await Mediator.Send(getListLicenseAndCertificationSkillQuery);
        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateLicenseAndCertificationSkillCommand createLicenseAndCertificationSkillCommand)
    {
        CreatedLicenseAndCertificationSkillResponse result = await Mediator.Send(createLicenseAndCertificationSkillCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.
        return Created("", result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateLicenseAndCertificationSkillCommand updateLicenseAndCertificationSkillCommand)
    {
        UpdatedLicenseAndCertificationSkillResponse result = await Mediator.Send(updateLicenseAndCertificationSkillCommand);
        return Created("", result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteLicenseAndCertificationSkillCommand deleteLicenseAndCertificationSkillCommand)
    {
        DeletedLicenseAndCertificationSkillResponse result = await Mediator.Send(deleteLicenseAndCertificationSkillCommand);
        return Ok(result);
    }
}