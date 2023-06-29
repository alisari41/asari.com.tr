using asari.com.tr.Application.Features.Educations.Queries.GetList;
using asari.com.tr.Application.Features.Experiences.Queries.GetList;
using asari.com.tr.Application.Features.Skills.Queries.GetList;
using asari.com.tr.WebMVC.Models;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace asari.com.tr.WebMVC.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(PageRequest pageRequest)
        {
            try
            {
                // Sayfa boyutu ve sayfa sayısı hesaplanır. 
                pageRequest.Page = pageRequest.Page != 0 ? pageRequest.Page : 0;
                pageRequest.PageSize = pageRequest.PageSize != 0 ? pageRequest.PageSize : 150;

                GetListSkillQuery getListSkillQuery = new() { PageRequest = pageRequest };

                GetListResponse<GetListSkillListItemDto> result = await Mediator.Send(getListSkillQuery);

                #region "Education" verilerini  listelemek için kullanılır
                GetListEducationQuery getListEducationQuery = new() { PageRequest = pageRequest };
                GetListResponse<GetListEducationListItemDto> resultEducation = await Mediator.Send(getListEducationQuery);

                // Populate ViewBag with the list of Education dtos
                ViewBag.EducationList = resultEducation.Items;
                #endregion


                #region "Experience" verilerini  listelemek için kullanılır
                GetListExperienceQuery getListExperienceQuery = new() { PageRequest = pageRequest };
                GetListResponse<GetListExperienceListItemDto> resultExperience = await Mediator.Send(getListExperienceQuery);

                // Populate ViewBag with the list of Experience dtos
                ViewBag.ExperienceList = resultExperience.Items;
                #endregion


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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}