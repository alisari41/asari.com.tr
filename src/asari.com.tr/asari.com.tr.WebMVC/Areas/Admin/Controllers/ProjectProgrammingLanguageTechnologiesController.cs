using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Commands.Create;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Commands.Delete;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Commands.Update;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Queries.GetById;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Queries.GetList;
using asari.com.tr.Application.Features.Projects.Queries.GetList;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Queries.GetList;
using Core.Application.Requests;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace asari.com.tr.WebMVC.Areas.Admin.Controllers;

[Area("Admin")]
public class ProjectProgrammingLanguageTechnologiesController : BaseController
{
    [HttpGet("/ProjectProgrammingLanguageTechnologies/GetList")]
    public async Task<IActionResult> GetList(PageRequest pageRequest)
    {
        try
        {
            // Sayfa boyutu ve sayfa sayısı hesaplanır. 
            pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
            pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

            GetListProjectProgrammingLanguageTechnologyQuery getListProjectProgrammingLanguageTechnologyQuery = new() { PageRequest = pageRequest };

            GetListResponse<GetListProjectProgrammingLanguageTechnologyListItemDto> result = await Mediator.Send(getListProjectProgrammingLanguageTechnologyQuery);

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

        #region Seçim yapmak için "ProgrammingLanguageTechnology" verilerini  listelemek için kullanılır
        GetListProgrammingLanguageTechnologyQuery getListProgrammingLanguageTechnologyQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListProgrammingLanguageTechnologyListItemDto> resultProgrammingLanguageTechnology = await Mediator.Send(getListProgrammingLanguageTechnologyQuery);

        ViewData["ControllerName"] = "ProgrammingLanguageTechnologies";
        // Populate ViewBag with the list of ProgrammingLanguageTechnology dtos
        ViewBag.ProgrammingLanguageTechnologyList = resultProgrammingLanguageTechnology;
        #endregion

        return View();
    }

    [HttpPost("/ProjectProgrammingLanguageTechnologies/Add")]
    public async Task<IActionResult> Add(CreateProjectProgrammingLanguageTechnologyCommand createProjectProgrammingLanguageTechnologyCommand)
    {
        try
        {
            CreatedProjectProgrammingLanguageTechnologyResponse result = await Mediator.Send(createProjectProgrammingLanguageTechnologyCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.

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

    public async Task<IActionResult> Update(PageRequest pageRequest, GetByIdProjectProgrammingLanguageTechnologyQuery getByIdProjectProgrammingLanguageTechnologyQuery)
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

        #region Seçim yapmak için "ProgrammingLanguageTechnology" verilerini  listelemek için kullanılır
        GetListProgrammingLanguageTechnologyQuery getListProgrammingLanguageTechnologyQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListProgrammingLanguageTechnologyListItemDto> resultProgrammingLanguageTechnology = await Mediator.Send(getListProgrammingLanguageTechnologyQuery);

        ViewData["ControllerName"] = "ProgrammingLanguageTechnologys";
        // Populate ViewBag with the list of ProgrammingLanguageTechnology dtos
        ViewBag.ProgrammingLanguageTechnologyList = resultProgrammingLanguageTechnology;
        #endregion


        GetByIdProjectProgrammingLanguageTechnologyGetByIdResponse result = await Mediator.Send(getByIdProjectProgrammingLanguageTechnologyQuery);

        ViewBag.ProjectName = result.ProjectTitle;
        ViewBag.ProgrammingLanguageTechnologyName = result.ProgrammingLanguageTechnologyName;

        UpdateProjectProgrammingLanguageTechnologyCommand updateProjectProgrammingLanguageTechnologyCommand = new UpdateProjectProgrammingLanguageTechnologyCommand
        { // Update metonde geriye sadece Result döndürdüğümüzde hata vermektedir.
            Id = result.Id,
            ProjectId = result.ProjectId,
            ProgrammingLanguageTechnologyId = result.ProgrammingLanguageTechnologyId
        };

        return View(updateProjectProgrammingLanguageTechnologyCommand);
    }

    [HttpPost("/ProjectProgrammingLanguageTechnologies/Update")]
    public async Task<IActionResult> Update(UpdateProjectProgrammingLanguageTechnologyCommand updateProjectProgrammingLanguageTechnologyCommand)
    {
        try
        {
            UpdatedProjectProgrammingLanguageTechnologyResponse result = await Mediator.Send(updateProjectProgrammingLanguageTechnologyCommand);
            return RedirectToAction("GetList");
        }
        catch (AuthorizationException authorizationException)
        {
            ViewBag.AuthorizationErrorMessage = authorizationException.Message;
            ViewBag.AuthorizationErrorStackTrace = authorizationException.StackTrace;

            return View(updateProjectProgrammingLanguageTechnologyCommand); // Hata MEsajı aldığımda geriye updateProjectProgrammingLanguageTechnologiesCommand'i döndürmezsem Form içerisinde @Model.Id boş muş gibi hata veriyor
        }
        catch (BusinessException businessException)
        {
            ViewBag.BusinessErrorMessage = businessException.Message;
            ViewBag.BusinessErrorStackTrace = businessException.StackTrace;

            return View(updateProjectProgrammingLanguageTechnologyCommand);
        }
        catch (NotFoundException notFoundException)
        {
            ViewBag.NotFoundErrorMessage = notFoundException.Message;
            ViewBag.NotFoundErrorStackTrace = notFoundException.StackTrace;

            return View(updateProjectProgrammingLanguageTechnologyCommand);
        }
        catch (ValidationException validationException)
        {
            ViewBag.ValidationErrorMessage = validationException.Message;
            ViewBag.ValidationErrorStackTrace = validationException.StackTrace;

            return View(updateProjectProgrammingLanguageTechnologyCommand);
        }
        catch (Exception exception)
        {
            ViewBag.ExceptionErrorMessage = exception.Message;
            ViewBag.ExceptionErrorStackTrace = exception.StackTrace;

            return View(updateProjectProgrammingLanguageTechnologyCommand);
        }
    }

    [HttpPost("/ProjectProgrammingLanguageTechnologies/Delete")]
    public async Task<IActionResult> Delete(DeleteProjectProgrammingLanguageTechnologyCommand deleteProjectProgrammingLanguageTechnologyCommand)
    {
        DeletedProjectProgrammingLanguageTechnologyResponse result = await Mediator.Send(deleteProjectProgrammingLanguageTechnologyCommand);
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
