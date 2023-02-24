using asari.com.tr.Application.Features.ProgrammingLanguages.Dtos;
using Core.Persistence.Paging;

namespace asari.com.tr.Application.Features.ProgrammingLanguages.Models;

public class ProgrammingLanguageListModel : BasePageableModel
{
    // Sayfalama için kullanılan sınıf olarak diyebiliriz. Hem Sayfalama verilerini hemde Dto verilerini kullacağız
    public IList<ProgrammingLanguageDto> Items { get; set; } // İsimlendirmeler aynı olması gerekirki mapperda bir daha configuraiton yapmamak için 
}
