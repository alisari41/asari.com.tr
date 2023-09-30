using Application.Tests.Constants;
using Application.Tests.Features.Experiences.Constants;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using asari.com.tr.Application.Features.Experiences.Commands.Update;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Test.Application.Constants;
using FluentValidation.Results;
using Xunit;
using static asari.com.tr.Application.Features.Experiences.Commands.Update.UpdateExperienceCommand;

namespace Application.Tests.Features.Experiences.Commands.Update;

public class UpdateExperienceTests : ExperienceMockRepository
{
    private readonly UpdateExperienceCommand _command;
    private readonly UpdateExperienceCommandValidator _validator;
    private readonly UpdateExperienceCommandHandler _handler;

    public UpdateExperienceTests(ExperienceFakeData fakeData, UpdateExperienceCommand command, UpdateExperienceCommandValidator validator) : base(fakeData)
    {
        _command = command;
        _validator = validator;
        _handler = new UpdateExperienceCommandHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    #region FluentValidation - Formatlama Testleri
    #region Id
    [Fact(DisplayName = "Deneyim Id boş girildiğinde formatlama hatası veriyor mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.BosVeriCategori)]
    public void ExperienceIdAlaniBosOlduguHaldeHataDonupDonmemeTesti()
    {
        _command.Id = null;
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "Id" && x.ErrorCode == ValidationErrorCodes.NotEmptyValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.NotEmptyValidator, result?.ErrorCode);
    }
    #endregion

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

    [Fact(DisplayName = "Deneyim tablosunda başarılı veri güncelleme testi")]
    [Trait(TestCategories.CQRSCategori, TestCategories.UpdateCategori)]
    public async Task ExperienceTablosunaBasariliVeriGuncellemeTesti()
    {
        _command.Id = ExperienceTestData.UpdateId;
        _command.Title = ExperienceTestData.UpdateTitle;
        _command.EmploymentType = ExperienceTestData.UpdateEmploymentType;
        _command.CompanyName = ExperienceTestData.UpdateCompanyName;
        _command.Location = ExperienceTestData.UpdateLocation;
        _command.Statu = ExperienceTestData.UpdateStatu;
        _command.StartDate = ExperienceTestData.UpdateStartDate;
        _command.EndDate = ExperienceTestData.UpdateEndDate;
        _command.Industry = ExperienceTestData.UpdateIndustry;
        _command.Description = ExperienceTestData.UpdateDescription;
        _command.ProfileHeadline = ExperienceTestData.UpdateProfileHeadline;

        UpdatedExperienceResponse result = await _handler.Handle(_command, CancellationToken.None);
        Assert.Equal(expected: ExperienceTestData.UpdateTitle, result.Title);
    }

    [Fact(DisplayName = "Deneyim tablosunda olmayan veriyi güncellemek istediğimizde BusinessRules Testi")]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.OlmayanVeriCategori)]
    public async Task ExperienceTablosundaOlmayanVeriyiGuncellemeTesti()
    {
        _command.Id = ExperienceTestData.NonexistentId;
        _command.Title = ExperienceTestData.UpdateTitle;
        _command.EmploymentType = ExperienceTestData.UpdateEmploymentType;
        _command.CompanyName = ExperienceTestData.UpdateCompanyName;
        _command.Location = ExperienceTestData.UpdateLocation;
        _command.Statu = ExperienceTestData.UpdateStatu;
        _command.StartDate = ExperienceTestData.UpdateStartDate;
        _command.EndDate = ExperienceTestData.UpdateEndDate;
        _command.Industry = ExperienceTestData.UpdateIndustry;
        _command.Description = ExperienceTestData.UpdateDescription;
        _command.ProfileHeadline = ExperienceTestData.UpdateProfileHeadline;

        await Assert.ThrowsAsync<BusinessException>(async () => await _handler.Handle(_command, CancellationToken.None));
    }
}