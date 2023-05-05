using asari.com.tr.Application.Features.Educations.Commands.Delete;
using asari.com.tr.Application.Features.Educations.Commands.Update;
using asari.com.tr.Application.Features.Educations.Queries.GetById;
using asari.com.tr.Application.Features.Educations.Queries.GetList;
using asari.com.tr.Application.Features.EducationSkills.Commands.Create;
using asari.com.tr.Application.Features.EducationSkills.Commands.Update;
using asari.com.tr.Application.Features.EducationSkills.Queries.GetById;
using asari.com.tr.Application.Features.EducationSkills.Queries.GetList;
using asari.com.tr.Application.Features.Skills.Queries.GetList;
using Core.Application.Requests;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using asari.com.tr.Application.Features.EducationSkills.Commands.Delete;

namespace asari.com.tr.WebMVC.Areas.Admin.Controllers;

[Area("Admin")]
public class EducationSkillsController : BaseController
{
    [HttpGet("/EducationSkills/GetList")]
    public async Task<IActionResult> GetList(PageRequest pageRequest)
    {
        try
        {
            // Sayfa boyutu ve sayfa sayısı hesaplanır. 
            pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
            pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

            GetListEducationSkillQuery getListEducationSkillQuery = new() { PageRequest = pageRequest };

            GetListResponse<GetListEducationSkillListItemDto> result = await Mediator.Send(getListEducationSkillQuery);

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

        #region Seçim yapmak için "Education" verilerini  listelemek için kullanılır
        GetListEducationQuery getListEducationQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListEducationListItemDto> resultEducation = await Mediator.Send(getListEducationQuery);

        ViewData["ControllerName"] = "Educations";
        // Populate ViewBag with the list of education dtos
        ViewBag.EducationList = resultEducation;
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

    [HttpPost("/EducationSkills/Add")]
    public async Task<IActionResult> Add(CreateEducationSkillCommand createEducationSkillCommand)
    {
        try
        {
            CreatedEducationSkillResponse result = await Mediator.Send(createEducationSkillCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.

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

    public async Task<IActionResult> Update(PageRequest pageRequest, GetByIdEducationSkillQuery getByIdEducationSkillQuery)
    {

        pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
        pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

        #region Seçim yapmak için "Education" verilerini  listelemek için kullanılır
        GetListEducationQuery getListEducationQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListEducationListItemDto> resultEducation = await Mediator.Send(getListEducationQuery);

        ViewData["ControllerName"] = "Educations";
        // Populate ViewBag with the list of education dtos
        ViewBag.EducationList = resultEducation;
        #endregion

        #region Seçim yapmak için "Skill" verilerini  listelemek için kullanılır
        GetListSkillQuery getListSkillQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListSkillListItemDto> resultSkill = await Mediator.Send(getListSkillQuery);

        ViewData["ControllerName"] = "Skills";
        // Populate ViewBag with the list of Skill dtos
        ViewBag.SkillList = resultSkill;
        #endregion


        GetByIdEducationSkillGetByIdResponse result = await Mediator.Send(getByIdEducationSkillQuery);

        ViewBag.EducationName = result.EducationName;
        ViewBag.SkillName = result.SkillName;

        UpdateEducationSkillCommand updateEducationSkillCommand = new UpdateEducationSkillCommand
        { // Update metonde geriye sadece Result döndürdüğümüzde hata vermektedir.
            Id = result.Id,
            EducationId = result.EducationId,
            SkillId = result.SkillId
        };

        return View(updateEducationSkillCommand);
    }

    [HttpPost("/EducationSkills/Update")]
    public async Task<IActionResult> Update(UpdateEducationSkillCommand updateEducationSkillCommand)
    {
        try
        {
            UpdatedEducationSkillResponse result = await Mediator.Send(updateEducationSkillCommand);
            return RedirectToAction("GetList");
        }
        catch (AuthorizationException authorizationException)
        {
            ViewBag.AuthorizationErrorMessage = authorizationException.Message;
            ViewBag.AuthorizationErrorStackTrace = authorizationException.StackTrace;

            return View(updateEducationSkillCommand); // Hata MEsajı aldığımda geriye updateEducationSkillsCommand'i döndürmezsem Form içerisinde @Model.Id boş muş gibi hata veriyor
        }
        catch (BusinessException businessException)
        {
            ViewBag.BusinessErrorMessage = businessException.Message;
            ViewBag.BusinessErrorStackTrace = businessException.StackTrace;

            return View(updateEducationSkillCommand);
        }
        catch (NotFoundException notFoundException)
        {
            ViewBag.NotFoundErrorMessage = notFoundException.Message;
            ViewBag.NotFoundErrorStackTrace = notFoundException.StackTrace;

            return View(updateEducationSkillCommand);
        }
        catch (ValidationException validationException)
        {
            ViewBag.ValidationErrorMessage = validationException.Message;
            ViewBag.ValidationErrorStackTrace = validationException.StackTrace;

            return View(updateEducationSkillCommand);
        }
        catch (Exception exception)
        {
            ViewBag.ExceptionErrorMessage = exception.Message;
            ViewBag.ExceptionErrorStackTrace = exception.StackTrace;

            return View(updateEducationSkillCommand);
        }
    }

    [HttpPost("/EducationSkills/Delete")]
    public async Task<IActionResult> Delete(DeleteEducationSkillCommand deleteEducationSkillCommand)
    {
        DeletedEducationSkillResponse result = await Mediator.Send(deleteEducationSkillCommand);
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