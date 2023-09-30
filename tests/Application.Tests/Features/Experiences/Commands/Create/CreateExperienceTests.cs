using Application.Tests.Constants;
using Application.Tests.Features.Experiences.Constants;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using asari.com.tr.Application.Features.Experiences.Commands.Create;
using Core.Test.Application.Constants;
using FluentValidation.Results;
using Xunit;
using static asari.com.tr.Application.Features.Experiences.Commands.Create.CreateExperienceCommand;

namespace Application.Tests.Features.Experiences.Commands.Create;

public class CreateExperienceTests : ExperienceMockRepository
{
    private readonly CreateExperienceCommand _command;
    private readonly CreateExperienceCommandValidator _validator;
    private readonly CreateExperienceCommandHandler _handler;

    public CreateExperienceTests(ExperienceFakeData fakeData, CreateExperienceCommand command, CreateExperienceCommandValidator validator) : base(fakeData)
    {
        _command = command;
        _validator = validator;
        _handler = new CreateExperienceCommandHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    #region FluentValidation - Formatlama Testleri
    #region Title
    [Fact(DisplayName = "Deneyim Adı boş girildiğinde formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.BosVeriCategori)]
    public void ExperienceNameAlaniBosOlduguHaldeHataDonupDonmemeTesti()
    {
        _command.Title = string.Empty;
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "Title" && x.ErrorCode == ValidationErrorCodes.NotEmptyValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.NotEmptyValidator, result?.ErrorCode);
    }

    [Fact(DisplayName = "Deneyim Adı min karakter formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.MinKarakterCategori)]
    public void ExperienceTitleAlaniMinKaraterHatasiTesti()
    {
        _command.Title = "D";
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "Title" && x.ErrorCode == ValidationErrorCodes.MinimumLengthValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.MinimumLengthValidator, result?.ErrorCode);
    }

    [Fact(DisplayName = "Deneyim Adı max karakter formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.MaxKarakterCategori)]
    public void ExperienceTitleAlaniMaxKaraterHatasiTesti()
    {
        _command.Title = TestData.KaraterSayisi250DenFazlaData;
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "Title" && x.ErrorCode == ValidationErrorCodes.MaximumLengthValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.MaximumLengthValidator, result?.ErrorCode);
    }
    #endregion

    #region EmploymentType
    [Fact(DisplayName = "Deneyim İstihdam türü boş girildiğinde formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.BosVeriCategori)]
    public void ExperienceEmploymentTypeAlaniBosOlduguHaldeHataDonupDonmemeTesti()
    {
        _command.EmploymentType = string.Empty;
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "EmploymentType" && x.ErrorCode == ValidationErrorCodes.NotEmptyValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.NotEmptyValidator, result?.ErrorCode);
    }

    [Fact(DisplayName = "Deneyim İstihdam türü min karakter formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.MinKarakterCategori)]
    public void ExperienceEmploymentTypeAlaniMinKaraterHatasiTesti()
    {
        _command.EmploymentType = "D";
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "EmploymentType" && x.ErrorCode == ValidationErrorCodes.MinimumLengthValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.MinimumLengthValidator, result?.ErrorCode);
    }

    [Fact(DisplayName = "Deneyim İstihdam türü max karakter formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.MaxKarakterCategori)]
    public void ExperienceEmploymentTypeAlaniMaxKaraterHatasiTesti()
    {
        _command.EmploymentType = TestData.KaraterSayisi50DenFazlaData;
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "EmploymentType" && x.ErrorCode == ValidationErrorCodes.MaximumLengthValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.MaximumLengthValidator, result?.ErrorCode);
    }
    #endregion

    #region CompanyName
    [Fact(DisplayName = "Deneyim Şirket adı boş girildiğinde formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.BosVeriCategori)]
    public void ExperienceCompanyNameAlaniBosOlduguHaldeHataDonupDonmemeTesti()
    {
        _command.CompanyName = string.Empty;
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "CompanyName" && x.ErrorCode == ValidationErrorCodes.NotEmptyValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.NotEmptyValidator, result?.ErrorCode);
    }

    [Fact(DisplayName = "Deneyim Şirket adı min karakter formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.MinKarakterCategori)]
    public void ExperienceCompanyNameAlaniMinKaraterHatasiTesti()
    {
        _command.CompanyName = "D";
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "CompanyName" && x.ErrorCode == ValidationErrorCodes.MinimumLengthValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.MinimumLengthValidator, result?.ErrorCode);
    }

    [Fact(DisplayName = "Deneyim Şirket adı max karakter formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.MaxKarakterCategori)]
    public void ExperienceCompanyNameAlaniMaxKaraterHatasiTesti()
    {
        _command.CompanyName = TestData.KaraterSayisi250DenFazlaData;
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "CompanyName" && x.ErrorCode == ValidationErrorCodes.MaximumLengthValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.MaximumLengthValidator, result?.ErrorCode);
    }
    #endregion

    #region Location
    [Fact(DisplayName = "Deneyim Konumu boş girildiğinde formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.BosVeriCategori)]
    public void ExperienceLocationAlaniBosOlduguHaldeHataDonupDonmemeTesti()
    {
        _command.Location = string.Empty;
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "Location" && x.ErrorCode == ValidationErrorCodes.NotEmptyValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.NotEmptyValidator, result?.ErrorCode);
    }

    [Fact(DisplayName = "Deneyim Konumu min karakter formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.MinKarakterCategori)]
    public void ExperienceLocationAlaniMinKaraterHatasiTesti()
    {
        _command.Location = "D";
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "Location" && x.ErrorCode == ValidationErrorCodes.MinimumLengthValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.MinimumLengthValidator, result?.ErrorCode);
    }

    [Fact(DisplayName = "Deneyim Konumu max karakter formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.MaxKarakterCategori)]
    public void ExperienceLocationAlaniMaxKaraterHatasiTesti()
    {
        _command.Location = TestData.KaraterSayisi250DenFazlaData;
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "Location" && x.ErrorCode == ValidationErrorCodes.MaximumLengthValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.MaximumLengthValidator, result?.ErrorCode);
    }
    #endregion

    #region StartDate
    [Fact(DisplayName = "Deneyim Başlangıç Tarihi (StartDate) boş girildiğinde formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.BosVeriCategori)]
    public void ExperienceStartDateAlaniBosOlduguHaldeHataDonupDonmemeTesti()
    {
        _command.StartDate = null;
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "StartDate" && x.ErrorCode == ValidationErrorCodes.NotEmptyValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.NotEmptyValidator, result?.ErrorCode);
    }
    #endregion

    #region Industry
    [Fact(DisplayName = "Deneyim Sektörü boş girildiğinde formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.BosVeriCategori)]
    public void ExperienceIndustryAlaniBosOlduguHaldeHataDonupDonmemeTesti()
    {
        _command.Industry = string.Empty;
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "Industry" && x.ErrorCode == ValidationErrorCodes.NotEmptyValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.NotEmptyValidator, result?.ErrorCode);
    }

    [Fact(DisplayName = "Deneyim Sektörü min karakter formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.MinKarakterCategori)]
    public void ExperienceIndustryAlaniMinKaraterHatasiTesti()
    {
        _command.Industry = "D";
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "Industry" && x.ErrorCode == ValidationErrorCodes.MinimumLengthValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.MinimumLengthValidator, result?.ErrorCode);
    }

    [Fact(DisplayName = "Deneyim Sektörü max karakter formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.MaxKarakterCategori)]
    public void ExperienceIndustryAlaniMaxKaraterHatasiTesti()
    {
        _command.Industry = TestData.KaraterSayisi250DenFazlaData;
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "Industry" && x.ErrorCode == ValidationErrorCodes.MaximumLengthValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.MaximumLengthValidator, result?.ErrorCode);
    }
    #endregion

    #region ProfileHeadline
    [Fact(DisplayName = "Deneyim Profil başlığı boş girildiğinde formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.BosVeriCategori)]
    public void ExperienceProfileHeadlineAlaniBosOlduguHaldeHataDonupDonmemeTesti()
    {
        _command.ProfileHeadline = string.Empty;
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "ProfileHeadline" && x.ErrorCode == ValidationErrorCodes.NotEmptyValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.NotEmptyValidator, result?.ErrorCode);
    }
    #endregion
    #endregion

    [Fact(DisplayName = "Deneyim tablosuna başarılı veri ekleme testi")]
    [Trait(TestCategories.CQRSCategori, TestCategories.CreateCategori)]
    public async Task ExperienceTablosunaBasariliVeriEklemeTesti()
    {
        _command.Title = ExperienceTestData.CreateTitle;
        _command.EmploymentType = ExperienceTestData.CreateEmploymentType;
        _command.CompanyName = ExperienceTestData.CreateCompanyName;
        _command.Location = ExperienceTestData.CreateLocation;
        _command.Statu = ExperienceTestData.CreateStatu;
        _command.StartDate = ExperienceTestData.CreateStartDate;
        _command.EndDate = ExperienceTestData.CreateEndDate;
        _command.Industry = ExperienceTestData.CreateIndustry;
        _command.Description = ExperienceTestData.CreateDescription;
        _command.ProfileHeadline = ExperienceTestData.CreateProfileHeadline;

        CreatedExperienceResponse result = await _handler.Handle(_command, CancellationToken.None);
        Assert.Equal(expected: ExperienceTestData.CreateTitle, result.Title);
    }
}