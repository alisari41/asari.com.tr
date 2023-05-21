using asari.com.tr.Application.Features.ProgrammingLanguages.Commands.Delete;
using asari.com.tr.Application.Features.ProgrammingLanguages.Commands.Create;
using asari.com.tr.Application.Features.ProgrammingLanguages.Commands.Update;
using asari.com.tr.Application.Features.ProgrammingLanguages.Queries.GetById;
using asari.com.tr.Application.Features.ProgrammingLanguages.Queries.GetList;
using Core.Application.Requests;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace asari.com.tr.WebMVC.Areas.Admin.Controllers;

[Area("Admin")]
public class ProgrammingLanguagesController : BaseController
{
    [HttpGet("/ProgrammingLanguages/GetList")]
    public async Task<IActionResult> GetList(PageRequest pageRequest)
    {
        try
        {
            // Sayfa boyutu ve sayfa sayısı hesaplanır. 
            pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
            pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

            GetListProgrammingLanguageQuery getListProgrammingLanguageQuery = new() { PageRequest = pageRequest };

            GetListResponse<GetListProgrammingLanguageListItemDto> result = await Mediator.Send(getListProgrammingLanguageQuery);

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

    [HttpGet("/ProgrammingLanguages/GetListDigerTablar")]
    public async Task<IActionResult> GetListDigerTablar(PageRequest pageRequest)
    {
        try
        {
            // Sayfa boyutu ve sayfa sayısı hesaplanır. 
            pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
            pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

            GetListProgrammingLanguageQuery getListProgrammingLanguageQuery = new() { PageRequest = pageRequest };

            GetListResponse<GetListProgrammingLanguageListItemDto> result = await Mediator.Send(getListProgrammingLanguageQuery);

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

    public IActionResult Add()
    {
        ViewBag.Statu = true; // Sayfa ilk açıldığında statüyü true atadıktan sonra kaydederken doğru kaydetmesi için kullanıldı
        return View();
    }

    [HttpPost("/ProgrammingLanguages/Add")]
    public async Task<IActionResult> Add(CreateProgrammingLanguageCommand createProgrammingLanguageCommand)
    {
        try
        {
            CreatedProgrammingLanguageResponse result = await Mediator.Send(createProgrammingLanguageCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.
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

    public async Task<IActionResult> Update(GetByIdProgrammingLanguageQuery getByIdProgrammingLanguageQuery)
    {
        GetByIdProgrammingLanguageResponse result = await Mediator.Send(getByIdProgrammingLanguageQuery);

        UpdateProgrammingLanguageCommand updateProgrammingLanguageCommand = new UpdateProgrammingLanguageCommand
        { // Update metodundan geriye sadece Result döndürdüğümüzde hata vermektedir.
            Id = result.Id,
            Name = result.Name
        };

        return View(updateProgrammingLanguageCommand);
    }

    [HttpPost("/ProgrammingLanguages/Update")]
    public async Task<IActionResult> Update(UpdateProgrammingLanguageCommand updateProgrammingLanguageCommand)
    {
        try
        {
            UpdatedProgrammingLanguageResponse result = await Mediator.Send(updateProgrammingLanguageCommand);
            return RedirectToAction("GetList");
        }
        catch (AuthorizationException authorizationException)
        {
            ViewBag.AuthorizationErrorMessage = authorizationException.Message;
            ViewBag.AuthorizationErrorStackTrace = authorizationException.StackTrace;

            return View(updateProgrammingLanguageCommand); // Hata MEsajı aldığımda geriye updateProgrammingLanguageCommand'i döndürmezsem Form içerisinde @Model.Id boş muş gibi hata veriyor
        }
        catch (BusinessException businessException)
        {
            ViewBag.BusinessErrorMessage = businessException.Message;
            ViewBag.BusinessErrorStackTrace = businessException.StackTrace;

            return View(updateProgrammingLanguageCommand);
        }
        catch (NotFoundException notFoundException)
        {
            ViewBag.NotFoundErrorMessage = notFoundException.Message;
            ViewBag.NotFoundErrorStackTrace = notFoundException.StackTrace;

            return View(updateProgrammingLanguageCommand);
        }
        catch (ValidationException validationException)
        {
            ViewBag.ValidationErrorMessage = validationException.Message;
            ViewBag.ValidationErrorStackTrace = validationException.StackTrace;

            return View(updateProgrammingLanguageCommand);
        }
        catch (Exception exception)
        {
            ViewBag.ExceptionErrorMessage = exception.Message;
            ViewBag.ExceptionErrorStackTrace = exception.StackTrace;

            return View(updateProgrammingLanguageCommand);
        }
    }

    [HttpPost("/ProgrammingLanguages/Delete")]
    public async Task<IActionResult> Delete(DeleteProgrammingLanguageCommand deleteProgrammingLanguageCommand)
    {
        DeletedProgrammingLanguageResponse result = await Mediator.Send(deleteProgrammingLanguageCommand);
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