using asari.com.tr.Application.Features.Experiences.Commands.Update;
using asari.com.tr.Application.Features.Experiences.Queries.GetById;
using asari.com.tr.Application.Features.Experiences.Commands.Create;
using asari.com.tr.Application.Features.Experiences.Queries.GetList;
using Core.Application.Requests;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using asari.com.tr.Application.Features.Experiences.Commands.Delete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace asari.com.tr.WebMVC.Areas.Admin.Controllers;

[Area("Admin")]
public class ExperiencesController : BaseController
{
    [HttpGet("/Experiences/GetList")]
    public async Task<IActionResult> GetList(PageRequest pageRequest)
    {
        try
        {
            // Sayfa boyutu ve sayfa sayısı hesaplanır. 
            pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
            pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

            GetListExperienceQuery getListExperienceQuery = new() { PageRequest = pageRequest };

            GetListResponse<GetListExperienceListItemDto> result = await Mediator.Send(getListExperienceQuery);

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

    [HttpGet("/Experiences/GetListDigerTablar")]
    public async Task<IActionResult> GetListDigerTablar(PageRequest pageRequest)
    {
        try
        {
            // Sayfa boyutu ve sayfa sayısı hesaplanır. 
            pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
            pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

            GetListExperienceQuery getListExperienceQuery = new() { PageRequest = pageRequest };

            GetListResponse<GetListExperienceListItemDto> result = await Mediator.Send(getListExperienceQuery);

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

    [HttpPost("/Experiences/Add")]
    public async Task<IActionResult> Add(CreateExperienceCommand createExperienceCommand)
    {
        try
        {
            CreatedExperienceResponse result = await Mediator.Send(createExperienceCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.
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

    public async Task<IActionResult> Update(GetByIdExperienceQuery getByIdExperienceQuery)
    {
        GetByIdExperienceResponse result = await Mediator.Send(getByIdExperienceQuery);

        UpdateExperienceCommand updateExperienceCommand = new UpdateExperienceCommand
        { // Update metodundan geriye sadece Result döndürdüğümüzde hata vermektedir.
            Id = result.Id,
            Title = result.Title,
            EmploymentType = result.EmploymentType,
            CompanyName = result.CompanyName,
            Location = result.Location,
            Statu = result.Statu,
            StartDate = result.StartDate,
            EndDate = result.EndDate,
            Industry = result.Industry,
            Description = result.Description,
            ProfileHeadline = result.ProfileHeadline
        };

        return View(updateExperienceCommand);
    }

    [HttpPost("/Experiences/Update")]
    public async Task<IActionResult> Update(UpdateExperienceCommand updateExperienceCommand)
    {
        try
        {
            UpdatedExperienceResponse result = await Mediator.Send(updateExperienceCommand);
            return RedirectToAction("GetList");
        }
        catch (AuthorizationException authorizationException)
        {
            ViewBag.AuthorizationErrorMessage = authorizationException.Message;
            ViewBag.AuthorizationErrorStackTrace = authorizationException.StackTrace;

            return View(updateExperienceCommand); // Hata MEsajı aldığımda geriye updateExperienceCommand'i döndürmezsem Form içerisinde @Model.Id boş muş gibi hata veriyor
        }
        catch (BusinessException businessException)
        {
            ViewBag.BusinessErrorMessage = businessException.Message;
            ViewBag.BusinessErrorStackTrace = businessException.StackTrace;

            return View(updateExperienceCommand);
        }
        catch (NotFoundException notFoundException)
        {
            ViewBag.NotFoundErrorMessage = notFoundException.Message;
            ViewBag.NotFoundErrorStackTrace = notFoundException.StackTrace;

            return View(updateExperienceCommand);
        }
        catch (ValidationException validationException)
        {
            ViewBag.ValidationErrorMessage = validationException.Message;
            ViewBag.ValidationErrorStackTrace = validationException.StackTrace;

            return View(updateExperienceCommand);
        }
        catch (Exception exception)
        {
            ViewBag.ExceptionErrorMessage = exception.Message;
            ViewBag.ExceptionErrorStackTrace = exception.StackTrace;

            return View(updateExperienceCommand);
        }
    }

    [HttpPost("/Experiences/Delete")]
    public async Task<IActionResult> Delete(DeleteExperienceCommand deleteExperienceCommand)
    {
        DeletedExperienceResponse result = await Mediator.Send(deleteExperienceCommand);
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