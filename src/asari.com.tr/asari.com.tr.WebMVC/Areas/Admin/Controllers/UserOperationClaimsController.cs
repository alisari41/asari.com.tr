using asari.com.tr.Application.Features.UserOperationClaims.Commands.Create;
using asari.com.tr.Application.Features.UserOperationClaims.Commands.Delete;
using asari.com.tr.Application.Features.UserOperationClaims.Commands.Update;
using asari.com.tr.Application.Features.UserOperationClaims.Queries.GetById;
using asari.com.tr.Application.Features.UserOperationClaims.Queries.GetList;
using asari.com.tr.Application.Features.Users.Queries.GetList;
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
public class UserOperationClaimsController : BaseController
{
    [HttpGet("/UserOperationClaims/GetList")]
    public async Task<IActionResult> GetList(PageRequest pageRequest)
    {
        try
        {
            // Sayfa boyutu ve sayfa sayısı hesaplanır. 
            pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
            pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

            GetListUserOperationClaimQuery getListUserOperationClaimQuery = new() { PageRequest = pageRequest };

            GetListResponse<GetListUserOperationClaimListItemDto> result = await Mediator.Send(getListUserOperationClaimQuery);

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

        #region Seçim yapmak için "User" verilerini  listelemek için kullanılır
        GetListUserQuery getListUserQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListUserListItemDto> resultUser = await Mediator.Send(getListUserQuery);

        ViewData["ControllerName"] = "Users";
        // Populate ViewBag with the list of User dtos
        ViewBag.UserList = resultUser;
        #endregion

        #region Seçim yapmak için "OperationClaim" verilerini  listelemek için kullanılır
        GetListOperationClaimQuery getListOperationClaimQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListOperationClaimListItemDto> resultOperationClaim = await Mediator.Send(getListOperationClaimQuery);

        ViewData["ControllerName"] = "OperationClaims";
        // Populate ViewBag with the list of OperationClaim dtos
        ViewBag.OperationClaimList = resultOperationClaim;
        #endregion

        return View();
    }

    [HttpPost("/UserOperationClaims/Add")]
    public async Task<IActionResult> Add(CreateUserOperationClaimCommand createUserOperationClaimCommand)
    {
        try
        {
            CreatedUserOperationClaimResponse result = await Mediator.Send(createUserOperationClaimCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.

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

    public async Task<IActionResult> Update(PageRequest pageRequest, GetByIdUserOperationClaimQuery getByIdUserOperationClaimQuery)
    {

        pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
        pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

        #region Seçim yapmak için "User" verilerini  listelemek için kullanılır
        GetListUserQuery getListUserQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListUserListItemDto> resultUser = await Mediator.Send(getListUserQuery);

        ViewData["ControllerName"] = "Users";
        // Populate ViewBag with the list of User dtos
        ViewBag.UserList = resultUser;
        #endregion

        #region Seçim yapmak için "OperationClaim" verilerini  listelemek için kullanılır
        GetListOperationClaimQuery getListOperationClaimQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListOperationClaimListItemDto> resultOperationClaim = await Mediator.Send(getListOperationClaimQuery);

        ViewData["ControllerName"] = "OperationClaims";
        // Populate ViewBag with the list of OperationClaim dtos
        ViewBag.OperationClaimList = resultOperationClaim;
        #endregion


        GetByIdUserOperationClaimResponse result = await Mediator.Send(getByIdUserOperationClaimQuery);

        ViewBag.UserName = result.UserEmail;
        ViewBag.OperationClaimName = result.OperationClaimName;

        UpdateUserOperationClaimCommand updateUserOperationClaimCommand = new UpdateUserOperationClaimCommand
        { // Update metonde geriye sadece Result döndürdüğümüzde hata vermektedir.
            Id = result.Id,
            UserId = result.UserId,
            OperationClaimId = result.OperationClaimId
        };

        return View(updateUserOperationClaimCommand);
    }

    [HttpPost("/UserOperationClaims/Update")]
    public async Task<IActionResult> Update(UpdateUserOperationClaimCommand updateUserOperationClaimCommand)
    {
        try
        {
            UpdatedUserOperationClaimResponse result = await Mediator.Send(updateUserOperationClaimCommand);
            return RedirectToAction("GetList");
        }
        catch (AuthorizationException authorizationException)
        {
            ViewBag.AuthorizationErrorMessage = authorizationException.Message;
            ViewBag.AuthorizationErrorStackTrace = authorizationException.StackTrace;

            return View(updateUserOperationClaimCommand); // Hata MEsajı aldığımda geriye updateUserOperationClaimsCommand'i döndürmezsem Form içerisinde @Model.Id boş muş gibi hata veriyor
        }
        catch (BusinessException businessException)
        {
            ViewBag.BusinessErrorMessage = businessException.Message;
            ViewBag.BusinessErrorStackTrace = businessException.StackTrace;

            return View(updateUserOperationClaimCommand);
        }
        catch (NotFoundException notFoundException)
        {
            ViewBag.NotFoundErrorMessage = notFoundException.Message;
            ViewBag.NotFoundErrorStackTrace = notFoundException.StackTrace;

            return View(updateUserOperationClaimCommand);
        }
        catch (ValidationException validationException)
        {
            ViewBag.ValidationErrorMessage = validationException.Message;
            ViewBag.ValidationErrorStackTrace = validationException.StackTrace;

            return View(updateUserOperationClaimCommand);
        }
        catch (Exception exception)
        {
            ViewBag.ExceptionErrorMessage = exception.Message;
            ViewBag.ExceptionErrorStackTrace = exception.StackTrace;

            return View(updateUserOperationClaimCommand);
        }
    }

    [HttpPost("/UserOperationClaims/Delete")]
    public async Task<IActionResult> Delete(DeleteUserOperationClaimCommand deleteUserOperationClaimCommand)
    {
        DeletedUserOperationClaimResponse result = await Mediator.Send(deleteUserOperationClaimCommand);
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