using asari.com.tr.Application.Features.TechnologyProjects.Commands.Create;
using asari.com.tr.Application.Features.TechnologyProjects.Commands.Delete;
using asari.com.tr.Application.Features.TechnologyProjects.Commands.Update;
using asari.com.tr.Application.Features.TechnologyProjects.Queries.GetById;
using asari.com.tr.Application.Features.TechnologyProjects.Queries.GetList;
using asari.com.tr.Application.Features.Technologies.Queries.GetList;
using asari.com.tr.Application.Features.Projects.Queries.GetList;
using Core.Application.Requests;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace asari.com.tr.WebMVC.Areas.Admin.Controllers;

[Area("Admin")]
public class TechnologyProjectsController : BaseController
{
    [HttpGet("/TechnologyProjects/GetList")]
    public async Task<IActionResult> GetList(PageRequest pageRequest)
    {
        try
        {
            // Sayfa boyutu ve sayfa sayısı hesaplanır. 
            pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
            pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

            GetListTechnologyProjectQuery getListTechnologyProjectQuery = new() { PageRequest = pageRequest };

            GetListResponse<GetListTechnologyProjectListItemDto> result = await Mediator.Send(getListTechnologyProjectQuery);

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

        #region Seçim yapmak için "Technology" verilerini  listelemek için kullanılır
        GetListTechnologyQuery getListTechnologyQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListTechnologyListItemDto> resultTechnology = await Mediator.Send(getListTechnologyQuery);

        ViewData["ControllerName"] = "Technologies";
        // Populate ViewBag with the list of Technology dtos
        ViewBag.TechnologyList = resultTechnology;
        #endregion

        #region Seçim yapmak için "Project" verilerini  listelemek için kullanılır
        GetListProjectQuery getListProjectQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListProjectListItemDto> resultProject = await Mediator.Send(getListProjectQuery);

        ViewData["ControllerName"] = "Projects";
        // Populate ViewBag with the list of Project dtos
        ViewBag.ProjectList = resultProject;
        #endregion

        return View();
    }

    [HttpPost("/TechnologyProjects/Add")]
    public async Task<IActionResult> Add(CreateTechnologyProjectCommand createTechnologyProjectCommand)
    {
        try
        {
            CreatedTechnologyProjectResponse result = await Mediator.Send(createTechnologyProjectCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.

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

    public async Task<IActionResult> Update(PageRequest pageRequest, GetByIdTechnologyProjectQuery getByIdTechnologyProjectQuery)
    {

        pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
        pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

        #region Seçim yapmak için "Technology" verilerini  listelemek için kullanılır
        GetListTechnologyQuery getListTechnologyQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListTechnologyListItemDto> resultTechnology = await Mediator.Send(getListTechnologyQuery);

        ViewData["ControllerName"] = "Technologies";
        // Populate ViewBag with the list of Technology dtos
        ViewBag.TechnologyList = resultTechnology;
        #endregion

        #region Seçim yapmak için "Project" verilerini  listelemek için kullanılır
        GetListProjectQuery getListProjectQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListProjectListItemDto> resultProject = await Mediator.Send(getListProjectQuery);

        ViewData["ControllerName"] = "Projects";
        // Populate ViewBag with the list of Project dtos
        ViewBag.ProjectList = resultProject;
        #endregion


        GetByIdTechnologyProjectResponse result = await Mediator.Send(getByIdTechnologyProjectQuery);

        ViewBag.TechnologyName = result.TechnologyTitle;
        ViewBag.ProjectName = result.ProjectTitle;

        UpdateTechnologyProjectCommand updateTechnologyProjectCommand = new UpdateTechnologyProjectCommand
        { // Update metonde geriye sadece Result döndürdüğümüzde hata vermektedir.
            Id = result.Id,
            TechnologyId = result.TechnologyId,
            ProjectId = result.ProjectId
        };

        return View(updateTechnologyProjectCommand);
    }

    [HttpPost("/TechnologyProjects/Update")]
    public async Task<IActionResult> Update(UpdateTechnologyProjectCommand updateTechnologyProjectCommand)
    {
        try
        {
            UpdatedTechnologyProjectResponse result = await Mediator.Send(updateTechnologyProjectCommand);
            return RedirectToAction("GetList");
        }
        catch (AuthorizationException authorizationException)
        {
            ViewBag.AuthorizationErrorMessage = authorizationException.Message;
            ViewBag.AuthorizationErrorStackTrace = authorizationException.StackTrace;

            return View(updateTechnologyProjectCommand); // Hata MEsajı aldığımda geriye updateTechnologyProjectsCommand'i döndürmezsem Form içerisinde @Model.Id boş muş gibi hata veriyor
        }
        catch (BusinessException businessException)
        {
            ViewBag.BusinessErrorMessage = businessException.Message;
            ViewBag.BusinessErrorStackTrace = businessException.StackTrace;

            return View(updateTechnologyProjectCommand);
        }
        catch (NotFoundException notFoundException)
        {
            ViewBag.NotFoundErrorMessage = notFoundException.Message;
            ViewBag.NotFoundErrorStackTrace = notFoundException.StackTrace;

            return View(updateTechnologyProjectCommand);
        }
        catch (ValidationException validationException)
        {
            ViewBag.ValidationErrorMessage = validationException.Message;
            ViewBag.ValidationErrorStackTrace = validationException.StackTrace;

            return View(updateTechnologyProjectCommand);
        }
        catch (Exception exception)
        {
            ViewBag.ExceptionErrorMessage = exception.Message;
            ViewBag.ExceptionErrorStackTrace = exception.StackTrace;

            return View(updateTechnologyProjectCommand);
        }
    }

    [HttpPost("/TechnologyProjects/Delete")]
    public async Task<IActionResult> Delete(DeleteTechnologyProjectCommand deleteTechnologyProjectCommand)
    {
        DeletedTechnologyProjectResponse result = await Mediator.Send(deleteTechnologyProjectCommand);
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