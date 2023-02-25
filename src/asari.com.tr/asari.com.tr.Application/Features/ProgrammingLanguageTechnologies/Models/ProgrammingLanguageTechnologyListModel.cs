using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Dtos;
using Core.Persistence.Paging;

namespace asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Models;

public class ProgrammingLanguageTechnologyListModel : BasePageableModel
{
    // Sayfalama için kullanılan sınıf olarak diyebiliriz. Hem Sayfalama verilerini hemde Dto verilerini kullacağız
    public IList<ProgrammingLanguageTechnologyDto> Items { get; set; } // İsimlendirmeler aynı olması gerekirki mapperda bir daha configuraiton yapmamak için

}
