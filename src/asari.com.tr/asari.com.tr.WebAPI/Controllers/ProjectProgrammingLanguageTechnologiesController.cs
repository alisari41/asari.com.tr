using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Commands.CreateProjectProgrammingLanguageTechnology;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Queries.GetListProjectProgrammingLanguageTechnology;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace asari.com.tr.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectProgrammingLanguageTechnologiesController : BaseController
    {
        [HttpGet("get-list")]
        public async Task<ActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListProjectProgrammingLanguageTechnologyQuery getListProjectProgrammingLanguageTechnologyQuery = new() { PageRequest = pageRequest }; // Bu yeni kullanımdır eski hali aşağıdaki gibidir.
                                                                                                // GetListBrandQuery getListBrandQuery = new GetListBrandQuery();
                                                                                                // getListBrandQuery.PageRequest = pageRequest;

            var result = await Mediator.Send(getListProjectProgrammingLanguageTechnologyQuery);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateProjectProgrammingLanguageTechnologyCommand createProjectProgrammingLanguageTechnologyCommand)
        {
            var result = await Mediator.Send(createProjectProgrammingLanguageTechnologyCommand);
            return Created("", result);
        }
    }
}