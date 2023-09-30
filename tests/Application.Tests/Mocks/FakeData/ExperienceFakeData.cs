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
                Title = "Data Analyst",
                EmploymentType = "Contract",
                CompanyName = "XYZ Data Solutions",
                Location = "San Francisco",
                Statu = false,
                StartDate = DateTime.Parse("2021-09-01"),
                EndDate = DateTime.Parse("2022-06-30"),
                Industry = "Data Analysis",
                Description = "Performed data analysis and generated reports.",
                ProfileHeadline = "Experienced Data Analyst"
            }
        };
        return data;
    }
}