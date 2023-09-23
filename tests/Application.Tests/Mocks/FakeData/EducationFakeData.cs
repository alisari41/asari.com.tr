using asari.com.tr.Domain.Entities;
using Core.Test.Application.FakeData;

namespace Application.Tests.Mocks.FakeData;

public class EducationFakeData : BaseFakeData<Education>
{
    public override List<Education> CreateFakeData()
    {
        var data = new List<Education>
        {
            new() { Id = 1, Name="Derince Ticaret Meslek", FieldOfStudy="Bilişim Teknolojileri / Veri Tabanı Programcılığı Dalı", Grade="Lise", StartDate = new DateTime(2012, 9, 17), EndDateOrExcepted = new DateTime(2016, 6, 17), Degree=79.56, ActivityAndCommunity = null, Description = null, MediaUrl = null },
            new() { Id = 2, Name="Düzce Üniversitesi", FieldOfStudy="Bilgisayar Müh", Grade="Lisans", StartDate = new DateTime(2012, 9, 17), EndDateOrExcepted = new DateTime(2016, 6, 17), Degree=3.62, ActivityAndCommunity = null, Description = null, MediaUrl = null }
        };
        return data;
    }
}