using asari.com.tr.Application.Services.AuthService;
using Core.Security.Dtos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using asari.com.tr.Application.Features.Auths.Commands.Login;
using MediatR;
using Core.Security.Entities;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace asari.com.tr.WebMVC.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminLoginController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Index(UserForLoginDto userForLoginDto)
    {
        try
        {
            LoginCommand loginCommand = new() { UserForLoginDto = userForLoginDto, IpAddress = GetIpAddress() };
            LoggedResponse result = await Mediator.Send(loginCommand); // Hatalı mesaj dondürdüğümde hata mesajını nasıl gösteririm denenecek


            if (result.RefreshToken is not null) SetRefreshTokenToCookie(result.RefreshToken);


            return Ok(result.CreateResponseDto());
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
        catch (ValidationException validationException)//ValidationException
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

    [AllowAnonymous]
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Session.Clear();
        return Redirect("/");
    }

    private void SetRefreshTokenToCookie(RefreshToken refreshToken)
    {
        // Oluşan RefreshToken'ı aynı zamanda Cookie ye eklemek gerekir

        CookieOptions cookieOptions = new()
        {
            HttpOnly = true, // Http isteklerinde bu işi yap demek
            Expires = DateTime.Now.AddDays(7)
        };

        Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
    }
}