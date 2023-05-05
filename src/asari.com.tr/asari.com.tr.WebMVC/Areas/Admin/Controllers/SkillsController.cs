using asari.com.tr.Application.Features.Skills.Commands.Create;
using asari.com.tr.Application.Features.Skills.Commands.Delete;
using asari.com.tr.Application.Features.Skills.Commands.Update;
using asari.com.tr.Application.Features.Skills.Queries.GetById;
using asari.com.tr.Application.Features.Skills.Queries.GetList;
using Core.Application.Requests;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace asari.com.tr.WebMVC.Areas.Admin.Controllers;

[Area("Admin")]
public class SkillsController : BaseController
{
    [HttpGet("/Skills/Index")]
    public async Task<IActionResult> GetList(PageRequest pageRequest)
    {
        try
        {
            // Sayfa boyutu ve sayfa sayısı hesaplanır. 
            pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
            pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

            GetListSkillQuery getListSkillQuery = new() { PageRequest = pageRequest };

            GetListResponse<GetListSkillListItemDto> result = await Mediator.Send(getListSkillQuery);

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

    [HttpGet("/Skills/GetListDigerTablar")]
    public async Task<IActionResult> GetListDigerTablar(PageRequest pageRequest)
    {
        try
        {
            // Sayfa boyutu ve sayfa sayısı hesaplanır. 
            pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
            pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

            GetListSkillQuery getListSkillQuery = new() { PageRequest = pageRequest };

            GetListResponse<GetListSkillListItemDto> result = await Mediator.Send(getListSkillQuery);

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
        return View();
    }

    [HttpPost("/Skills/Add")]
    public async Task<IActionResult> Add(CreateSkillCommand createSkillCommand)
    {
        try
        {
            // Form verilerini dinamik olarak al. Formu dinamik olarak alamamım sebebi cshtml den bana eğer sayı 0.4 olarak gelidiğinde createSkillCommand bunu 4 müi gibi kabul ediyor ben 0,4 yapıp tekrar yollayınca sayı doğru oluyor.
            string myDoubleStr = Request.Form["Degree"];
            double myDegree = Double.Parse(myDoubleStr.Replace('.', ','));

            // createSkillCommand sınıfındaki myDegree özelliğini güncelle
            createSkillCommand.Degree = myDegree;


            CreatedSkillResponse result = await Mediator.Send(createSkillCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.
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

    public async Task<IActionResult> Update(GetByIdSkillQuery getByIdSkillQuery)
    {
        GetByIdSkillGetByIdResponse result = await Mediator.Send(getByIdSkillQuery);

        string myDoubleStr = result.Degree.ToString();
        double myDegree = Double.Parse(myDoubleStr.Replace('.', ','));

        UpdateSkillCommand updateSkillCommand = new UpdateSkillCommand
        { // Update metonde geriye sadece Result döndürdüğümüzde hata vermektedir.
            Id = result.Id,
            Name = result.Name,
            Degree = myDegree
        };

        return View(updateSkillCommand);
    }

    [HttpPost("/Skills/Update")]
    public async Task<IActionResult> Update(UpdateSkillCommand updateSkillCommand)
    {
        try
        {
            // Form verilerini dinamik olarak al. Formu dinamik olarak alamamım sebebi cshtml den bana eğer sayı 0.4 olarak gelidiğinde updateSkillCommand bunu 4 müi gibi kabul ediyor ben 0,4 yapıp tekrar yollayınca sayı doğru oluyor.
            string myDoubleStr = Request.Form["Degree"];

            if (!string.Equals(myDoubleStr, ""))
            {
                double myDegree = Double.Parse(myDoubleStr.Replace('.', ','));

                // UpdateSkillCommand sınıfındaki myDegree özelliğini güncelle
                updateSkillCommand.Degree = myDegree;
            }


            UpdatedSkillResponse result = await Mediator.Send(updateSkillCommand);
            return RedirectToAction("GetList");
        }
        catch (AuthorizationException authorizationException)
        {
            ViewBag.AuthorizationErrorMessage = authorizationException.Message;
            ViewBag.AuthorizationErrorStackTrace = authorizationException.StackTrace;

            return View(updateSkillCommand); // Hata MEsajı aldığımda geriye updateSkillCommand'i döndürmezsem Form içerisinde @Model.Id boş muş gibi hata veriyor
        }
        catch (BusinessException businessException)
        {
            ViewBag.BusinessErrorMessage = businessException.Message;
            ViewBag.BusinessErrorStackTrace = businessException.StackTrace;

            return View(updateSkillCommand);
        }
        catch (NotFoundException notFoundException)
        {
            ViewBag.NotFoundErrorMessage = notFoundException.Message;
            ViewBag.NotFoundErrorStackTrace = notFoundException.StackTrace;

            return View(updateSkillCommand);
        }
        catch (ValidationException validationException)
        {
            ViewBag.ValidationErrorMessage = validationException.Message;
            ViewBag.ValidationErrorStackTrace = validationException.StackTrace;

            return View(updateSkillCommand);
        }
        catch (Exception exception)
        {
            ViewBag.ExceptionErrorMessage = exception.Message;
            ViewBag.ExceptionErrorStackTrace = exception.StackTrace;

            return View(updateSkillCommand);
        }
    }

    [HttpPost("/Skills/Delete")]
    public async Task<IActionResult> Delete(DeleteSkillCommand deleteSkillCommand)
    {
        DeletedSkillResponse result = await Mediator.Send(deleteSkillCommand);
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
