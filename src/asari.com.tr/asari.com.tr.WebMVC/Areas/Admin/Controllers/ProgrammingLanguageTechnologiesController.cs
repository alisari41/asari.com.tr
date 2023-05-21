using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Commands.Create;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Commands.Delete;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Commands.Update;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Queries.GetById;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Queries.GetList;
using asari.com.tr.Application.Features.ProgrammingLanguages.Queries.GetList;
using Core.Application.Requests;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace asari.com.tr.WebMVC.Areas.Admin.Controllers;

[Area("Admin")]
public class ProgrammingLanguageTechnologiesController : BaseController
{
    [HttpGet("/ProgrammingLanguageTechnologies/GetList")]
    public async Task<IActionResult> GetList(PageRequest pageRequest)
    {
        try
        {
            // Sayfa boyutu ve sayfa sayısı hesaplanır. 
            pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
            pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

            GetListProgrammingLanguageTechnologyQuery getListProgrammingLanguageTechnologyQuery = new() { PageRequest = pageRequest };

            GetListResponse<GetListProgrammingLanguageTechnologyListItemDto> result = await Mediator.Send(getListProgrammingLanguageTechnologyQuery);

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

    [HttpGet("/ProgrammingLanguageTechnologies/GetListDigerTablar")]
    public async Task<IActionResult> GetListDigerTablar(PageRequest pageRequest)
    {
        try
        {
            // Sayfa boyutu ve sayfa sayısı hesaplanır. 
            pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
            pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

            GetListProgrammingLanguageTechnologyQuery getListProgrammingLanguageTechnologyQuery = new() { PageRequest = pageRequest };

            GetListResponse<GetListProgrammingLanguageTechnologyListItemDto> result = await Mediator.Send(getListProgrammingLanguageTechnologyQuery);

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

        #region Seçim yapmak için "ProgrammingLanguage" verilerini  listelemek için kullanılır
        GetListProgrammingLanguageQuery getListProgrammingLanguageQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListProgrammingLanguageListItemDto> resultProgrammingLanguage = await Mediator.Send(getListProgrammingLanguageQuery);

        ViewData["ControllerName"] = "ProgrammingLanguages";
        // Populate ViewBag with the list of ProgrammingLanguage dtos
        ViewBag.ProgrammingLanguageList = resultProgrammingLanguage;
        #endregion
        return View();
    }

    [HttpPost("/ProgrammingLanguageTechnologies/Add")]
    public async Task<IActionResult> Add(CreateProgrammingLanguageTechnologyCommand createProgrammingLanguageTechnologyCommand)
    {
        try
        {
            CreatedProgrammingLanguageTechnologyResponse result = await Mediator.Send(createProgrammingLanguageTechnologyCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.
            //ViewBag.Success = "Kaydetme İşlemi Başarılı";

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

    public async Task<IActionResult> Update(PageRequest pageRequest, GetByIdProgrammingLanguageTechnologyQuery getByIdProgrammingLanguageTechnologyQuery)
    {

        pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
        pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

        #region Seçim yapmak için "ProgrammingLanguage" verilerini  listelemek için kullanılır
        GetListProgrammingLanguageQuery getListProgrammingLanguageQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListProgrammingLanguageListItemDto> resultProgrammingLanguage = await Mediator.Send(getListProgrammingLanguageQuery);

        ViewData["ControllerName"] = "ProgrammingLanguages";
        // Populate ViewBag with the list of ProgrammingLanguage dtos
        ViewBag.ProgrammingLanguageList = resultProgrammingLanguage;
        #endregion


        GetByIdProgrammingLanguageTechnologyResponse result = await Mediator.Send(getByIdProgrammingLanguageTechnologyQuery);
        ViewBag.ProgrammingLanguageName = result.ProgrammingLanguageName;


        UpdateProgrammingLanguageTechnologyCommand updateProgrammingLanguageTechnologyCommand = new UpdateProgrammingLanguageTechnologyCommand
        { // Update metodundan geriye sadece Result döndürdüğümüzde hata vermektedir.
            Id = result.Id,
            ProgrammingLanguageId = result.ProgrammingLanguageId,
            Name = result.Name
        };

        return View(updateProgrammingLanguageTechnologyCommand);
    }

    [HttpPost("/ProgrammingLanguageTechnologies/Update")]
    public async Task<IActionResult> Update(UpdateProgrammingLanguageTechnologyCommand updateProgrammingLanguageTechnologyCommand)
    {
        try
        {
            UpdatedProgrammingLanguageTechnologyResponse result = await Mediator.Send(updateProgrammingLanguageTechnologyCommand);
            return RedirectToAction("GetList");
        }
        catch (AuthorizationException authorizationException)
        {
            ViewBag.AuthorizationErrorMessage = authorizationException.Message;
            ViewBag.AuthorizationErrorStackTrace = authorizationException.StackTrace;

            return View(updateProgrammingLanguageTechnologyCommand); // Hata MEsajı aldığımda geriye updateProgrammingLanguageTechnologyCommand'i döndürmezsem Form içerisinde @Model.Id boş muş gibi hata veriyor
        }
        catch (BusinessException businessException)
        {
            ViewBag.BusinessErrorMessage = businessException.Message;
            ViewBag.BusinessErrorStackTrace = businessException.StackTrace;

            return View(updateProgrammingLanguageTechnologyCommand);
        }
        catch (NotFoundException notFoundException)
        {
            ViewBag.NotFoundErrorMessage = notFoundException.Message;
            ViewBag.NotFoundErrorStackTrace = notFoundException.StackTrace;

            return View(updateProgrammingLanguageTechnologyCommand);
        }
        catch (ValidationException validationException)
        {
            ViewBag.ValidationErrorMessage = validationException.Message;
            ViewBag.ValidationErrorStackTrace = validationException.StackTrace;

            return View(updateProgrammingLanguageTechnologyCommand);
        }
        catch (Exception exception)
        {
            ViewBag.ExceptionErrorMessage = exception.Message;
            ViewBag.ExceptionErrorStackTrace = exception.StackTrace;

            return View(updateProgrammingLanguageTechnologyCommand);
        }
    }

    [HttpPost("/ProgrammingLanguageTechnologies/Delete")]
    public async Task<IActionResult> Delete(DeleteProgrammingLanguageTechnologyCommand deleteProgrammingLanguageTechnologyCommand)
    {
        DeletedProgrammingLanguageTechnologyResponse result = await Mediator.Send(deleteProgrammingLanguageTechnologyCommand);
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