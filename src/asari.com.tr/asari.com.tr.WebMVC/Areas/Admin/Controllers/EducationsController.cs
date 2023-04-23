using asari.com.tr.Application.Features.Auths.Commands.Register;
using asari.com.tr.Application.Features.Educations.Commands.Create;
using asari.com.tr.Application.Features.Educations.Queries.GetList;
using Azure;
using Core.Application.Requests;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Persistence.Paging;
using Core.Security.Dtos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Printing;
using X.PagedList;
using asari.com.tr.Application.Features.Educations.Commands.Update;
using asari.com.tr.Application.Features.Educations.Queries.GetById;
using Azure.Core;
using asari.com.tr.Application.Features.Educations.Commands.Delete;

namespace asari.com.tr.WebMVC.Areas.Admin.Controllers;

[Area("Admin")]
public class EducationsController : BaseController
{
    [HttpGet("GetList")]
    public async Task<IActionResult> GetList(PageRequest pageRequest)
    {
        try
        {
            // Sayfa boyutu ve sayfa sayısı hesaplanır. 
            //int pageSize = pageRequest.PageSize > 0 ? pageRequest.PageSize : 15;
            //int pageNumber = pageRequest.Page ?? 1;
            pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
            pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

            GetListEducationQuery getListEducationQuery = new() { PageRequest = pageRequest };

            GetListResponse<GetListEducationListItemDto> result = await Mediator.Send(getListEducationQuery);

            // Sayfalamaya uygun verileri görüntülemek için bir model oluşturulur
            var model = new GetListResponse<GetListEducationListItemDto>
            {
                Index = result.Index,
                Size = result.Size,
                Count = result.Count,
                Pages = result.Pages,
                HasPrevious = result.HasPrevious,
                HasNext = result.HasNext,
                Items = result.Items
            };

            // Verileri görüntülemek için view'e gönderilir
            return View(model);
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
        return View();
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Add(CreateEducationCommand createEducationCommand)
    {
        try
        {
            // Form verilerini dinamik olarak al. Formu dinamik olarak alamamım sebebi cshtml den bana eğer sayı 0.4 olarak gelidiğinde createEducationCommand bunu 4 müi gibi kabul ediyor ben 0,4 yapıp tekrar yollayınca sayı doğru oluyor.
            string myDoubleStr = Request.Form["Degree"];
            double myDegree = Double.Parse(myDoubleStr.Replace('.', ','));

            // createEducationCommand sınıfındaki myDegree özelliğini güncelle
            createEducationCommand.Degree = myDegree;


            CreatedEducationResponse result = await Mediator.Send(createEducationCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.
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

    public async Task<IActionResult> Update(GetByIdEducationQuery getByIdEducationQuery)
    {
        GetByIdEducationGetByIdResponse result = await Mediator.Send(getByIdEducationQuery);


        string myDoubleStr = result.Degree.ToString();
        double myDegree = Double.Parse(myDoubleStr.Replace('.', ','));

        UpdateEducationCommand updateEducationCommand = new UpdateEducationCommand
        { // Update metonde geriye sadece Result döndürdüğümüzde hata vermektedir.
            Id = result.Id,
            Name = result.Name,
            Degree = myDegree,
            FieldOfStudy = result.FieldOfStudy,
            StartDate = result.StartDate,
            EndDateOrExcepted = result.EndDateOrExcepted,
            Grade = result.Grade,
            ActivityAndCommunity = result.ActivityAndCommunity,
            Description = result.Description,
            MediaUrl = result.MediaUrl

        };

        return View(updateEducationCommand);
    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update(UpdateEducationCommand updateEducationCommand)
    {
        try
        {
            // Form verilerini dinamik olarak al. Formu dinamik olarak alamamım sebebi cshtml den bana eğer sayı 0.4 olarak gelidiğinde updateEducationCommand bunu 4 müi gibi kabul ediyor ben 0,4 yapıp tekrar yollayınca sayı doğru oluyor.
            string myDoubleStr = Request.Form["Degree"];
            double myDegree = Double.Parse(myDoubleStr.Replace('.', ','));

            // UpdateEducationCommand sınıfındaki myDegree özelliğini güncelle
            updateEducationCommand.Degree = myDegree;


            UpdatedEducationResponse result = await Mediator.Send(updateEducationCommand);
            return RedirectToAction("GetList");
        }
        catch (AuthorizationException authorizationException)
        {
            ViewBag.AuthorizationErrorMessage = authorizationException.Message;
            ViewBag.AuthorizationErrorStackTrace = authorizationException.StackTrace;

            return View(updateEducationCommand); // Hata MEsajı aldığımda geriye updateEducationCommand'i döndürmezsem Form içerisinde @Model.Id boş muş gibi hata veriyor
        }
        catch (BusinessException businessException)
        {
            ViewBag.BusinessErrorMessage = businessException.Message;
            ViewBag.BusinessErrorStackTrace = businessException.StackTrace;

            return View(updateEducationCommand);
        }
        catch (NotFoundException notFoundException)
        {
            ViewBag.NotFoundErrorMessage = notFoundException.Message;
            ViewBag.NotFoundErrorStackTrace = notFoundException.StackTrace;

            return View(updateEducationCommand);
        }
        catch (ValidationException validationException)
        {
            ViewBag.ValidationErrorMessage = validationException.Message;
            ViewBag.ValidationErrorStackTrace = validationException.StackTrace;

            return View(updateEducationCommand);
        }
        catch (Exception exception)
        {
            ViewBag.ExceptionErrorMessage = exception.Message;
            ViewBag.ExceptionErrorStackTrace = exception.StackTrace;

            return View(updateEducationCommand);
        }
    }

    [HttpPost("Delete")]
    public async Task<IActionResult> Delete( DeleteEducationCommand deleteEducationCommand)
    {
        DeletedEducationResponse result = await Mediator.Send(deleteEducationCommand);
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
