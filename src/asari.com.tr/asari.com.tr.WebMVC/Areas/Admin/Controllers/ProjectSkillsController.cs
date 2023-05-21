using asari.com.tr.Application.Features.ProjectSkills.Commands.Create;
using asari.com.tr.Application.Features.Projects.Queries.GetList;
using asari.com.tr.Application.Features.ProjectSkills.Queries.GetList;
using asari.com.tr.Application.Features.Skills.Queries.GetList;
using Core.Application.Requests;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;
using asari.com.tr.Application.Features.ProjectSkills.Commands.Update;
using asari.com.tr.Application.Features.ProjectSkills.Queries.GetById;
using asari.com.tr.Application.Features.ProjectSkills.Commands.Delete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace asari.com.tr.WebMVC.Areas.Admin.Controllers;

[Area("Admin")]
public class ProjectSkillsController : BaseController
{
    [HttpGet("/ProjectSkills/GetList")]
    public async Task<IActionResult> GetList(PageRequest pageRequest)
    {
        try
        {
            // Sayfa boyutu ve sayfa sayısı hesaplanır. 
            pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
            pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

            GetListProjectSkillQuery getListProjectSkillQuery = new() { PageRequest = pageRequest };

            GetListResponse<GetListProjectSkillListItemDto> result = await Mediator.Send(getListProjectSkillQuery);

            // Verileri görüntülemek için view'e gönderilir
            return View(result);
        }
        catch (Exception exception)
        {
            ViewBag.ExceptionErrorMessage = exception.Message;
            ViewBag.ExceptionErrorStackTrace = exception.StackTrace;

            return View();
        }
    }
    public async Task<IActionResult> Add(PageRequest pageRequest)
    {
        pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
        pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

        #region Seçim yapmak için "Project" verilerini  listelemek için kullanılır
        GetListProjectQuery getListProjectQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListProjectListItemDto> resultProject = await Mediator.Send(getListProjectQuery);

        ViewData["ControllerName"] = "Projects";
        // Populate ViewBag with the list of Project dtos
        ViewBag.ProjectList = resultProject;
        #endregion

        #region Seçim yapmak için "Skill" verilerini  listelemek için kullanılır
        GetListSkillQuery getListSkillQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListSkillListItemDto> resultSkill = await Mediator.Send(getListSkillQuery);

        ViewData["ControllerName"] = "Skills";
        // Populate ViewBag with the list of Skill dtos
        ViewBag.SkillList = resultSkill;
        #endregion

        return View();
    }

    [HttpPost("/ProjectSkills/Add")]
    public async Task<IActionResult> Add(CreateProjectSkillCommand createProjectSkillCommand)
    {
        try
        {
            CreatedProjectSkillResponse result = await Mediator.Send(createProjectSkillCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.

            return RedirectToAction("GetList");
        }
        catch (AuthorizationException authorizationException)
        {
            ViewBag.AuthorizationErrorMessage = authorizationException.Message;
            ViewBag.AuthorizationErrorStackTrace = authorizationException.StackTrace;

            return View();
        }
        catch (BusinessException businessException)
        {
            ViewBag.BusinessErrorMessage = businessException.Message;
            ViewBag.BusinessErrorStackTrace = businessException.StackTrace;

            return View();
        }
        catch (NotFoundException notFoundException)
        {
            ViewBag.NotFoundErrorMessage = notFoundException.Message;
            ViewBag.NotFoundErrorStackTrace = notFoundException.StackTrace;

            return View();
        }
        catch (ValidationException validationException)
        {
            ViewBag.ValidationErrorMessage = validationException.Message;
            ViewBag.ValidationErrorStackTrace = validationException.StackTrace;

            return View();
        }
        catch (Exception exception)
        {
            ViewBag.ExceptionErrorMessage = exception.Message;
            ViewBag.ExceptionErrorStackTrace = exception.StackTrace;

            return View();
        }
    }

    public async Task<IActionResult> Update(PageRequest pageRequest, GetByIdProjectSkillQuery getByIdProjectSkillQuery)
    {

        pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
        pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

        #region Seçim yapmak için "Project" verilerini  listelemek için kullanılır
        GetListProjectQuery getListProjectQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListProjectListItemDto> resultProject = await Mediator.Send(getListProjectQuery);

        ViewData["ControllerName"] = "Projects";
        // Populate ViewBag with the list of Project dtos
        ViewBag.ProjectList = resultProject;
        #endregion

        #region Seçim yapmak için "Skill" verilerini  listelemek için kullanılır
        GetListSkillQuery getListSkillQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListSkillListItemDto> resultSkill = await Mediator.Send(getListSkillQuery);

        ViewData["ControllerName"] = "Skills";
        // Populate ViewBag with the list of Skill dtos
        ViewBag.SkillList = resultSkill;
        #endregion


        GetByIdProjectSkillResponse result = await Mediator.Send(getByIdProjectSkillQuery);

        ViewBag.ProjectName = result.ProjectTitle;
        ViewBag.SkillName = result.SkillName;

        UpdateProjectSkillCommand updateProjectSkillCommand = new UpdateProjectSkillCommand
        { // Update metonde geriye sadece Result döndürdüğümüzde hata vermektedir.
            Id = result.Id,
            ProjectId = result.ProjectId,
            SkillId = result.SkillId
        };

        return View(updateProjectSkillCommand);
    }

    [HttpPost("/ProjectSkills/Update")]
    public async Task<IActionResult> Update(UpdateProjectSkillCommand updateProjectSkillCommand)
    {
        try
        {
            UpdatedProjectSkillResponse result = await Mediator.Send(updateProjectSkillCommand);
            return RedirectToAction("GetList");
        }
        catch (AuthorizationException authorizationException)
        {
            ViewBag.AuthorizationErrorMessage = authorizationException.Message;
            ViewBag.AuthorizationErrorStackTrace = authorizationException.StackTrace;

            return View(updateProjectSkillCommand); // Hata MEsajı aldığımda geriye updateProjectSkillsCommand'i döndürmezsem Form içerisinde @Model.Id boş muş gibi hata veriyor
        }
        catch (BusinessException businessException)
        {
            ViewBag.BusinessErrorMessage = businessException.Message;
            ViewBag.BusinessErrorStackTrace = businessException.StackTrace;

            return View(updateProjectSkillCommand);
        }
        catch (NotFoundException notFoundException)
        {
            ViewBag.NotFoundErrorMessage = notFoundException.Message;
            ViewBag.NotFoundErrorStackTrace = notFoundException.StackTrace;

            return View(updateProjectSkillCommand);
        }
        catch (ValidationException validationException)
        {
            ViewBag.ValidationErrorMessage = validationException.Message;
            ViewBag.ValidationErrorStackTrace = validationException.StackTrace;

            return View(updateProjectSkillCommand);
        }
        catch (Exception exception)
        {
            ViewBag.ExceptionErrorMessage = exception.Message;
            ViewBag.ExceptionErrorStackTrace = exception.StackTrace;

            return View(updateProjectSkillCommand);
        }
    }

    [HttpPost("/ProjectSkills/Delete")]
    public async Task<IActionResult> Delete(DeleteProjectSkillCommand deleteProjectSkillCommand)
    {
        DeletedProjectSkillResponse result = await Mediator.Send(deleteProjectSkillCommand);
        return RedirectToAction("GetList");
    }

    [AllowAnonymous]
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Session.Clear();
        return Redirect("/");
    }
}