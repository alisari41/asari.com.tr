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
}