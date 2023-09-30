using asari.com.tr.Domain.Entities;
using Core.Test.Application.FakeData;

namespace Application.Tests.Mocks.FakeData;

public class ExperienceFakeData : BaseFakeData<Experience>
{
    public override List<Experience> CreateFakeData()
    {
        var data = new List<Experience>
        {
            new () {
                Id = 1,
                Title = "Software Engineer", // Deneyim başlığı
                EmploymentType = "Full-time", // İş türü
                CompanyName = "ABC Inc.", // Şirket adı
                Location = "New York", // Konum
                Statu = true, // Durum (Aktif veya Pasif)
                StartDate = DateTime.Parse("2022-01-15"), // Başlangıç tarihi
                EndDate = null, // Bitiş tarihi (null, hala devam ediyor anlamına gelir)
                Industry = "Technology", // Endüstri
                Description = "Worked on software development projects.", // Açıklama
                ProfileHeadline = "Experienced Software Engineer" // Profil başlığı
            },
            new () {
                Id = 2,
                Title = "Data Analyst", // Deneyim başlığı
                EmploymentType = "Contract", // İş türü
                CompanyName = "XYZ Data Solutions", // Şirket adı
                Location = "San Francisco", // Konum
                Statu = false, // Durum (Aktif değil)
                StartDate = DateTime.Parse("2021-09-01"), // Başlangıç tarihi
                EndDate = DateTime.Parse("2022-06-30"), // Bitiş tarihi
                Industry = "Data Analysis", // Endüstri
                Description = "Performed data analysis and generated reports.", // Açıklama
                ProfileHeadline = "Experienced Data Analyst" // Profil başlığı
            }
        };
        return data;
    }
}