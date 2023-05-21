using asari.com.tr.Application.Features.Experiences.Queries.GetList;
using asari.com.tr.Application.Features.ExperienceSkills.Commands.Update;
using asari.com.tr.Application.Features.ExperienceSkills.Queries.GetById;
using asari.com.tr.Application.Features.ExperienceSkills.Commands.Create;
using asari.com.tr.Application.Features.ExperienceSkills.Queries.GetList;
using asari.com.tr.Application.Features.Skills.Queries.GetList;
using Core.Application.Requests;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;
using asari.com.tr.Application.Features.ExperienceSkills.Commands.Delete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace asari.com.tr.WebMVC.Areas.Admin.Controllers;

[Area("Admin")]
public class ExperienceSkillsController : BaseController
{
    [HttpGet("/ExperienceSkills/GetList")]
    public async Task<IActionResult> GetList(PageRequest pageRequest)
    {
        try
        {
            // Sayfa boyutu ve sayfa sayısı hesaplanır. 
            pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
            pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

            GetListExperienceSkillQuery getListExperienceSkillQuery = new() { PageRequest = pageRequest };

            GetListResponse<GetListExperienceSkillListItemDto> result = await Mediator.Send(getListExperienceSkillQuery);

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

        #region Seçim yapmak için "Experience" verilerini  listelemek için kullanılır
        GetListExperienceQuery getListExperienceQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListExperienceListItemDto> resultExperience = await Mediator.Send(getListExperienceQuery);

        ViewData["ControllerName"] = "Experiences";
        // Populate ViewBag with the list of Experience dtos
        ViewBag.ExperienceList = resultExperience;
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

    [HttpPost("/ExperienceSkills/Add")]
    public async Task<IActionResult> Add(CreateExperienceSkillCommand createExperienceSkillCommand)
    {
        try
        {
            CreatedExperienceSkillResponse result = await Mediator.Send(createExperienceSkillCommand); // Command'i de Madiator aracığılıyla handler'ını bulması için görevlendiriyoruz.

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

    public async Task<IActionResult> Update(PageRequest pageRequest, GetByIdExperienceSkillQuery getByIdExperienceSkillQuery)
    {

        pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
        pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 15;

        #region Seçim yapmak için "Experience" verilerini  listelemek için kullanılır
        GetListExperienceQuery getListExperienceQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListExperienceListItemDto> resultExperience = await Mediator.Send(getListExperienceQuery);

        ViewData["ControllerName"] = "Experiences";
        // Populate ViewBag with the list of Experience dtos
        ViewBag.ExperienceList = resultExperience;
        #endregion

        #region Seçim yapmak için "Skill" verilerini  listelemek için kullanılır
        GetListSkillQuery getListSkillQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListSkillListItemDto> resultSkill = await Mediator.Send(getListSkillQuery);

        ViewData["ControllerName"] = "Skills";
        // Populate ViewBag with the list of Skill dtos
        ViewBag.SkillList = resultSkill;
        #endregion


        GetByIdExperienceSkillResponse result = await Mediator.Send(getByIdExperienceSkillQuery);

        ViewBag.ExperienceName = result.ExperienceTitle;
        ViewBag.SkillName = result.SkillName;

        UpdateExperienceSkillCommand updateExperienceSkillCommand = new UpdateExperienceSkillCommand
        { // Update metonde geriye sadece Result döndürdüğümüzde hata vermektedir.
            Id = result.Id,
            ExperienceId = result.ExperienceId,
            SkillId = result.SkillId
        };

        return View(updateExperienceSkillCommand);
    }

    [HttpPost("/ExperienceSkills/Update")]
    public async Task<IActionResult> Update(UpdateExperienceSkillCommand updateExperienceSkillCommand)
    {
        try
        {
            UpdatedExperienceSkillResponse result = await Mediator.Send(updateExperienceSkillCommand);
            return RedirectToAction("GetList");
        }
        catch (AuthorizationException authorizationException)
        {
            ViewBag.AuthorizationErrorMessage = authorizationException.Message;
            ViewBag.AuthorizationErrorStackTrace = authorizationException.StackTrace;

            return View(updateExperienceSkillCommand); // Hata MEsajı aldığımda geriye updateExperienceSkillsCommand'i döndürmezsem Form içerisinde @Model.Id boş muş gibi hata veriyor
        }
        catch (BusinessException businessException)
        {
            ViewBag.BusinessErrorMessage = businessException.Message;
            ViewBag.BusinessErrorStackTrace = businessException.StackTrace;

            return View(updateExperienceSkillCommand);
        }
        catch (NotFoundException notFoundException)
        {
            ViewBag.NotFoundErrorMessage = notFoundException.Message;
            ViewBag.NotFoundErrorStackTrace = notFoundException.StackTrace;

            return View(updateExperienceSkillCommand);
        }
        catch (ValidationException validationException)
        {
            ViewBag.ValidationErrorMessage = validationException.Message;
            ViewBag.ValidationErrorStackTrace = validationException.StackTrace;

            return View(updateExperienceSkillCommand);
        }
        catch (Exception exception)
        {
            ViewBag.ExceptionErrorMessage = exception.Message;
            ViewBag.ExceptionErrorStackTrace = exception.StackTrace;

            return View(updateExperienceSkillCommand);
        }
    }

    [HttpPost("/ExperienceSkills/Delete")]
    public async Task<IActionResult> Delete(DeleteExperienceSkillCommand deleteExperienceSkillCommand)
    {
        DeletedExperienceSkillResponse result = await Mediator.Send(deleteExperienceSkillCommand);
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