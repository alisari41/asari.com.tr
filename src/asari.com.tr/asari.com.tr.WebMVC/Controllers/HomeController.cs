using asari.com.tr.Application.Features.Educations.Queries.GetList;
using asari.com.tr.Application.Features.Experiences.Queries.GetList;
using asari.com.tr.Application.Features.LicensesAndCertifications.Queries.GetList;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Queries.GetList;
using asari.com.tr.Application.Features.Projects.Queries.GetById;
using asari.com.tr.Application.Features.Skills.Queries.GetList;
using asari.com.tr.Application.Features.Technologies.Queries.GetById;
using asari.com.tr.Application.Features.Technologies.Queries.GetList;
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


                #region "Skill" verilerini  listelemek için kullanılır
                GetListSkillQuery getListSkillQuery = new() { PageRequest = pageRequest };

                GetListResponse<GetListSkillListItemDto> result = await Mediator.Send(getListSkillQuery);
                #endregion


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


                #region "LicensesAndCertification" verilerini  listelemek için kullanılır
                GetListLicenseAndCertificationQuery getListLicensesAndCertificationQuery = new() { PageRequest = pageRequest };
                GetListResponse<GetListLicenseAndCertificationListItemDto> resultLicensesAndCertification = await Mediator.Send(getListLicensesAndCertificationQuery);

                // Populate ViewBag with the list of LicensesAndCertification dtos
                ViewBag.LicensesAndCertificationList = resultLicensesAndCertification.Items;
                #endregion


                #region "ProjectProgrammingLanguageTechnology" verilerini  listelemek için kullanılır
                GetListProjectProgrammingLanguageTechnologyQuery getListProjectProgrammingLanguageTechnologyQuery = new() { PageRequest = pageRequest };
                GetListResponse<GetListProjectProgrammingLanguageTechnologyListItemDto> resultProjectProgrammingLanguageTechnology = await Mediator.Send(getListProjectProgrammingLanguageTechnologyQuery);

                // Populate ViewBag with the list of ProjectProgrammingLanguageTechnology dtos
                ViewBag.ProjectProgrammingLanguageTechnologyList = resultProjectProgrammingLanguageTechnology.Items;
                #endregion


                #region "Technology" verilerini  listelemek için kullanılır
                GetListTechnologyQuery getListTechnologyQuery = new() { PageRequest = pageRequest };
                GetListResponse<GetListTechnologyListItemDto> resultTechnology = await Mediator.Send(getListTechnologyQuery);

                // Populate ViewBag with the list of Technology dtos
                ViewBag.TechnologyList = resultTechnology.Items;
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

        public async Task<IActionResult> GetByIdProject(GetByIdProjectQuery getByIdProjectQuery) // route'daki Id ile GetByIdProjectQuery Id işlemini mapleme yapacak. Id yazılımları aynı olmak zorunda 
        {
            var result = await Mediator.Send(getByIdProjectQuery);

            return View(result);
        }

        public async Task<IActionResult> GetByIdTechnology(GetByIdTechnologyQuery getByIdTechnologyQuery) // route'daki Id ile GetByIdTechnologyQuery Id işlemini mapleme yapacak. Id yazılımları aynı olmak zorunda 
        {
            var result = await Mediator.Send(getByIdTechnologyQuery);

            return View(result);
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