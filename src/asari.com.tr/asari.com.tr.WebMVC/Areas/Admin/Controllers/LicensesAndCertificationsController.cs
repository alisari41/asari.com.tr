using asari.com.tr.Application.Features.LicensesAndCertifications.Commands.Update;
using asari.com.tr.Application.Features.LicensesAndCertifications.Queries.GetById;
using asari.com.tr.Application.Features.LicensesAndCertifications.Commands.Create;
using asari.com.tr.Application.Features.LicensesAndCertifications.Queries.GetList;
using Core.Application.Requests;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;
using asari.com.tr.Application.Features.LicensesAndCertifications.Commands.Delete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace asari.com.tr.WebMVC.Areas.Admin.Controllers;

[Area("Admin")]
public class LicensesAndCertificationsController : BaseController
{
    [HttpGet("/LicensesAndCertifications/GetList")]
    public async Task<IActionResult> GetList(PageRequest pageRequest)
    {
        try
        {
            // Sayfa boyutu ve sayfa sayısı hesaplanır. 
            pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
            pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

            GetListLicenseAndCertificationQuery getListLicenseAndCertificationQuery = new() { PageRequest = pageRequest };

            GetListResponse<GetListLicenseAndCertificationListItemDto> result = await Mediator.Send(getListLicenseAndCertificationQuery);

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

    [HttpGet("/LicensesAndCertifications/GetListDigerTablar")]
    public async Task<IActionResult> GetListDigerTablar(PageRequest pageRequest)
    {
        try
        {
            // Sayfa boyutu ve sayfa sayısı hesaplanır. 
            pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
            pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

            GetListLicenseAndCertificationQuery getListLicenseAndCertificationQuery = new() { PageRequest = pageRequest };

            GetListResponse<GetListLicenseAndCertificationListItemDto> result = await Mediator.Send(getListLicenseAndCertificationQuery);

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

    [HttpPost("/LicensesAndCertifications/Add")]
    public async Task<IActionResult> Add(CreateLicenseAndCertificationCommand createLicenseAndCertificationCommand)
    {
        try
        {
            CreatedLicenseAndCertificationResponse result = await Mediator.Send(createLicenseAndCertificationCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.
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

    public async Task<IActionResult> Update(GetByIdLicenseAndCertificationQuery getByIdLicenseAndCertificationQuery)
    {
        GetByIdLicenseAndCertificationResponse result = await Mediator.Send(getByIdLicenseAndCertificationQuery);

        UpdateLicenseAndCertificationCommand updateLicensesAndCertificationCommand = new UpdateLicenseAndCertificationCommand
        { // Update metodundan geriye sadece Result döndürdüğümüzde hata vermektedir.
            Id = result.Id,
            Name = result.Name,
            IssuingOrganization = result.IssuingOrganization,
            IssueDate = result.IssueDate,
            ExpirationDate = result.ExpirationDate,
            ImagegUrl = result.ImagegUrl,
            CredentialId = result.CredentialId,
            CredentialUrl = result.CredentialUrl,
        };

        return View(updateLicensesAndCertificationCommand);
    }

    [HttpPost("/LicensesAndCertifications/Update")]
    public async Task<IActionResult> Update(UpdateLicenseAndCertificationCommand updateLicenseAndCertificationCommand)
    {
        try
        {
            UpdatedLicenseAndCertificationResponse result = await Mediator.Send(updateLicenseAndCertificationCommand);
            return RedirectToAction("GetList");
        }
        catch (AuthorizationException authorizationException)
        {
            ViewBag.AuthorizationErrorMessage = authorizationException.Message;
            ViewBag.AuthorizationErrorStackTrace = authorizationException.StackTrace;

            return View(updateLicenseAndCertificationCommand); // Hata MEsajı aldığımda geriye updateLicensesAndCertificationCommand'i döndürmezsem Form içerisinde @Model.Id boş muş gibi hata veriyor
        }
        catch (BusinessException businessException)
        {
            ViewBag.BusinessErrorMessage = businessException.Message;
            ViewBag.BusinessErrorStackTrace = businessException.StackTrace;

            return View(updateLicenseAndCertificationCommand);
        }
        catch (NotFoundException notFoundException)
        {
            ViewBag.NotFoundErrorMessage = notFoundException.Message;
            ViewBag.NotFoundErrorStackTrace = notFoundException.StackTrace;

            return View(updateLicenseAndCertificationCommand);
        }
        catch (ValidationException validationException)
        {
            ViewBag.ValidationErrorMessage = validationException.Message;
            ViewBag.ValidationErrorStackTrace = validationException.StackTrace;

            return View(updateLicenseAndCertificationCommand);
        }
        catch (Exception exception)
        {
            ViewBag.ExceptionErrorMessage = exception.Message;
            ViewBag.ExceptionErrorStackTrace = exception.StackTrace;

            return View(updateLicenseAndCertificationCommand);
        }
    }

    [HttpPost("/LicensesAndCertifications/Delete")]
    public async Task<IActionResult> Delete(DeleteLicenseAndCertificationCommand deleteLicenseAndCertificationCommand)
    {
        DeletedLicenseAndCertificationResponse result = await Mediator.Send(deleteLicenseAndCertificationCommand);
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