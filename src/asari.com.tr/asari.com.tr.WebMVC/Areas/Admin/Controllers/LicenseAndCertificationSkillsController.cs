using asari.com.tr.Application.Features.LicenseAndCertificationSkills.Command.Create;
using asari.com.tr.Application.Features.LicenseAndCertificationSkills.Command.Delete;
using asari.com.tr.Application.Features.LicenseAndCertificationSkills.Command.Update;
using asari.com.tr.Application.Features.LicenseAndCertificationSkills.Queries.GetById;
using asari.com.tr.Application.Features.LicenseAndCertificationSkills.Queries.GetList;
using asari.com.tr.Application.Features.LicensesAndCertifications.Queries.GetList;
using asari.com.tr.Application.Features.Skills.Queries.GetList;
using Core.Application.Requests;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace asari.com.tr.WebMVC.Areas.Admin.Controllers;

[Area("Admin")]
public class LicenseAndCertificationSkillsController : BaseController
{
    [HttpGet("/LicenseAndCertificationSkills/GetList")]
    public async Task<IActionResult> GetList(PageRequest pageRequest)
    {
        try
        {
            // Sayfa boyutu ve sayfa sayısı hesaplanır. 
            pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
            pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

            GetListLicenseAndCertificationSkillQuery getListLicenseAndCertificationSkillQuery = new() { PageRequest = pageRequest };

            GetListResponse<GetListLicenseAndCertificationSkillListItemDto> result = await Mediator.Send(getListLicenseAndCertificationSkillQuery);

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

        #region Seçim yapmak için "LicenseAndCertification" verilerini  listelemek için kullanılır
        GetListLicenseAndCertificationQuery getListLicenseAndCertificationQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListLicenseAndCertificationListItemDto> resultLicenseAndCertification = await Mediator.Send(getListLicenseAndCertificationQuery);

        ViewData["ControllerName"] = "LicenseAndCertifications";
        // Populate ViewBag with the list of LicenseAndCertification dtos
        ViewBag.LicenseAndCertificationList = resultLicenseAndCertification;
        #endregion

        #region Seçim yapmak için "Skill" verilerini  listelemek için kullanılır
        GetListSkillQuery getListSkillQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListSkillListItemDto> resultSkill = await Mediator.Send(getListSkillQuery);

        ViewData["ControllerName"] = "Skills";
        // Populate ViewBag with the list of Skill dtos
        ViewBag.SkillList = resultSkill;
        #endregion

        return View();
    }

    [HttpPost("/LicenseAndCertificationSkills/Add")]
    public async Task<IActionResult> Add(CreateLicenseAndCertificationSkillCommand createLicenseAndCertificationSkillCommand)
    {
        try
        {
            CreatedLicenseAndCertificationSkillResponse result = await Mediator.Send(createLicenseAndCertificationSkillCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.

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

    public async Task<IActionResult> Update(PageRequest pageRequest, GetByIdLicenseAndCertificationSkillQuery getByIdLicenseAndCertificationSkillQuery)
    {

        pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
        pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

        #region Seçim yapmak için "LicenseAndCertification" verilerini  listelemek için kullanılır
        GetListLicenseAndCertificationQuery getListLicenseAndCertificationQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListLicenseAndCertificationListItemDto> resultLicenseAndCertification = await Mediator.Send(getListLicenseAndCertificationQuery);

        ViewData["ControllerName"] = "LicenseAndCertifications";
        // Populate ViewBag with the list of LicenseAndCertification dtos
        ViewBag.LicenseAndCertificationList = resultLicenseAndCertification;
        #endregion

        #region Seçim yapmak için "Skill" verilerini  listelemek için kullanılır
        GetListSkillQuery getListSkillQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListSkillListItemDto> resultSkill = await Mediator.Send(getListSkillQuery);

        ViewData["ControllerName"] = "Skills";
        // Populate ViewBag with the list of Skill dtos
        ViewBag.SkillList = resultSkill;
        #endregion


        GetByIdLicenseAndCertificationSkillGetByIdResponse result = await Mediator.Send(getByIdLicenseAndCertificationSkillQuery);

        ViewBag.LicenseAndCertificationName = result.LicenseAndCertificationName;
        ViewBag.SkillName = result.SkillName;

        UpdateLicenseAndCertificationSkillCommand updateLicenseAndCertificationSkillCommand = new UpdateLicenseAndCertificationSkillCommand
        { // Update metonde geriye sadece Result döndürdüğümüzde hata vermektedir.
            Id = result.Id,
            LicenseAndCertificationId = result.LicenseAndCertificationId,
            SkillId = result.SkillId
        };

        return View(updateLicenseAndCertificationSkillCommand);
    }

    [HttpPost("/LicenseAndCertificationSkills/Update")]
    public async Task<IActionResult> Update(UpdateLicenseAndCertificationSkillCommand updateLicenseAndCertificationSkillCommand)
    {
        try
        {
            UpdatedLicenseAndCertificationSkillResponse result = await Mediator.Send(updateLicenseAndCertificationSkillCommand);
            return RedirectToAction("GetList");
        }
        catch (AuthorizationException authorizationException)
        {
            ViewBag.AuthorizationErrorMessage = authorizationException.Message;
            ViewBag.AuthorizationErrorStackTrace = authorizationException.StackTrace;

            return View(updateLicenseAndCertificationSkillCommand); // Hata MEsajı aldığımda geriye updateLicenseAndCertificationSkillsCommand'i döndürmezsem Form içerisinde @Model.Id boş muş gibi hata veriyor
        }
        catch (BusinessException businessException)
        {
            ViewBag.BusinessErrorMessage = businessException.Message;
            ViewBag.BusinessErrorStackTrace = businessException.StackTrace;

            return View(updateLicenseAndCertificationSkillCommand);
        }
        catch (NotFoundException notFoundException)
        {
            ViewBag.NotFoundErrorMessage = notFoundException.Message;
            ViewBag.NotFoundErrorStackTrace = notFoundException.StackTrace;

            return View(updateLicenseAndCertificationSkillCommand);
        }
        catch (ValidationException validationException)
        {
            ViewBag.ValidationErrorMessage = validationException.Message;
            ViewBag.ValidationErrorStackTrace = validationException.StackTrace;

            return View(updateLicenseAndCertificationSkillCommand);
        }
        catch (Exception exception)
        {
            ViewBag.ExceptionErrorMessage = exception.Message;
            ViewBag.ExceptionErrorStackTrace = exception.StackTrace;

            return View(updateLicenseAndCertificationSkillCommand);
        }
    }

    [HttpPost("/LicenseAndCertificationSkills/Delete")]
    public async Task<IActionResult> Delete(DeleteLicenseAndCertificationSkillCommand deleteLicenseAndCertificationSkillCommand)
    {
        DeletedLicenseAndCertificationSkillResponse result = await Mediator.Send(deleteLicenseAndCertificationSkillCommand);
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