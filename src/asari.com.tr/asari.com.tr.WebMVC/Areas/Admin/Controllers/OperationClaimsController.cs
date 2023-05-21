using asari.com.tr.Application.Features.OperationClaims.Commands.Create;
using asari.com.tr.Application.Features.OperationClaims.Commands.Delete;
using asari.com.tr.Application.Features.OperationClaims.Commands.Update;
using asari.com.tr.Application.Features.OperationClaims.Queries.GetById;
using asari.com.tr.Application.Features.OperationClaims.Queries.GetList;
using Core.Application.Requests;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace asari.com.tr.WebMVC.Areas.Admin.Controllers;

[Area("Admin")]
public class OperationClaimsController : BaseController
{
    [HttpGet("/OperationClaims/GetList")]
    public async Task<IActionResult> GetList(PageRequest pageRequest)
    {
        try
        {
            // Sayfa boyutu ve sayfa sayısı hesaplanır. 
            pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
            pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

            GetListOperationClaimQuery getListOperationClaimQuery = new() { PageRequest = pageRequest };

            GetListResponse<GetListOperationClaimListItemDto> result = await Mediator.Send(getListOperationClaimQuery);

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

    [HttpGet("/OperationClaims/GetListDigerTablar")]
    public async Task<IActionResult> GetListDigerTablar(PageRequest pageRequest)
    {
        try
        {
            // Sayfa boyutu ve sayfa sayısı hesaplanır. 
            pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
            pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

            GetListOperationClaimQuery getListOperationClaimQuery = new() { PageRequest = pageRequest };

            GetListResponse<GetListOperationClaimListItemDto> result = await Mediator.Send(getListOperationClaimQuery);

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

    [HttpPost("/OperationClaims/Add")]
    public async Task<IActionResult> Add(CreateOperationClaimCommand createOperationClaimCommand)
    {
        try
        {
            CreatedOperationClaimResponse result = await Mediator.Send(createOperationClaimCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.
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

    public async Task<IActionResult> Update(GetByIdOperationClaimQuery getByIdOperationClaimQuery)
    {
        GetByIdOperationClaimResponse result = await Mediator.Send(getByIdOperationClaimQuery);

        UpdateOperationClaimCommand updateOperationClaimCommand = new UpdateOperationClaimCommand
        { // Update metodundan geriye sadece Result döndürdüğümüzde hata vermektedir.
            Id = result.Id,
            Name = result.Name
        };

        return View(updateOperationClaimCommand);
    }

    [HttpPost("/OperationClaims/Update")]
    public async Task<IActionResult> Update(UpdateOperationClaimCommand updateOperationClaimCommand)
    {
        try
        {
            UpdatedOperationClaimResponse result = await Mediator.Send(updateOperationClaimCommand);
            return RedirectToAction("GetList");
        }
        catch (AuthorizationException authorizationException)
        {
            ViewBag.AuthorizationErrorMessage = authorizationException.Message;
            ViewBag.AuthorizationErrorStackTrace = authorizationException.StackTrace;

            return View(updateOperationClaimCommand); // Hata MEsajı aldığımda geriye updateOperationClaimCommand'i döndürmezsem Form içerisinde @Model.Id boş muş gibi hata veriyor
        }
        catch (BusinessException businessException)
        {
            ViewBag.BusinessErrorMessage = businessException.Message;
            ViewBag.BusinessErrorStackTrace = businessException.StackTrace;

            return View(updateOperationClaimCommand);
        }
        catch (NotFoundException notFoundException)
        {
            ViewBag.NotFoundErrorMessage = notFoundException.Message;
            ViewBag.NotFoundErrorStackTrace = notFoundException.StackTrace;

            return View(updateOperationClaimCommand);
        }
        catch (ValidationException validationException)
        {
            ViewBag.ValidationErrorMessage = validationException.Message;
            ViewBag.ValidationErrorStackTrace = validationException.StackTrace;

            return View(updateOperationClaimCommand);
        }
        catch (Exception exception)
        {
            ViewBag.ExceptionErrorMessage = exception.Message;
            ViewBag.ExceptionErrorStackTrace = exception.StackTrace;

            return View(updateOperationClaimCommand);
        }
    }

    [HttpPost("/OperationClaims/Delete")]
    public async Task<IActionResult> Delete(DeleteOperationClaimCommand deleteOperationClaimCommand)
    {
        DeletedOperationClaimResponse result = await Mediator.Send(deleteOperationClaimCommand);
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