using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Commands.Create;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Commands.Delete;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Commands.Update;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Paging;
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

            GetListResponse<GetListProjectProgrammingLanguageTechnologyListDto> result = await Mediator.Send(getListProjectProgrammingLanguageTechnologyQuery);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateProjectProgrammingLanguageTechnologyCommand createProjectProgrammingLanguageTechnologyCommand)
        {
            var result = await Mediator.Send(createProjectProgrammingLanguageTechnologyCommand);
            return Created("", result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateProjectProgrammingLanguageTechnologyCommand updateProjectProgrammingLanguageTechnologyCommand)
        {
            var result = await Mediator.Send(updateProjectProgrammingLanguageTechnologyCommand);
            return Created("", result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteProjectProgrammingLanguageTechnologyCommand deleteProjectProgrammingLanguageTechnologyCommand)
        {
            var result = await Mediator.Send(deleteProjectProgrammingLanguageTechnologyCommand);
            return Ok(result);
        }
    }
}