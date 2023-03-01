using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Dtos;
using Core.Persistence.Paging;

namespace asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Models;

public class ProjectProgrammingLanguageTechnologyListModel : BasePageableModel
{
    // Sayfalama için kullanılan sınıf olarak diyebiliriz. Hem Sayfalama verilerini hemde Dto verilerini kullacağız
    public IList<ProjectProgrammingLanguageTechnologyListDto> Items { get; set; } // İsimlendirmeler aynı olması gerekirki mapperda bir daha configuraiton yapmamak için
}