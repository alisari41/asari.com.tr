using asari.com.tr.Application.Features.Projects.Dtos;
using Core.Persistence.Paging;

namespace asari.com.tr.Application.Features.Projects.Models;

public class ProjectListModel : BasePageableModel
{
    // Sayfalama için kullanılan sınıf olarak diyebiliriz. Hem Sayfalama verilerini hemde Dto verilerini kullacağız
    public IList<ProjectDto> Items { get; set; } // İsimlendirmeler aynı olması gerekirki mapperda bir daha configuraiton yapmamak için 
}