using Application.Tests.Constants;
using Application.Tests.Features.Educations.Constants;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using asari.com.tr.Application.Features.Educations.Commands.Update;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Test.Application.Constants;
using FluentValidation.Results;
using Xunit;
using static asari.com.tr.Application.Features.Educations.Commands.Update.UpdateEducationCommand;

namespace Application.Tests.Features.Educations.Commands.Update;

public class UpdateEducationTests : EducationMockRepository
{
    private readonly UpdateEducationCommand _command;
    private readonly UpdateEducationCommandValidator _validator;
    private readonly UpdateEducationCommandHandler _handler;

    public UpdateEducationTests(EducationFakeData fakeData, UpdateEducationCommandValidator validator, UpdateEducationCommand command)
    : base(fakeData)
    {
        _validator = validator;
        _command = command;
        _handler = new UpdateEducationCommandHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    #region FluentValidation - Formatlama Testleri
    #region Id
    [Fact(DisplayName = "Eğitim Id boş girildiğinde formatlama hatası veriyor mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.BosVeriCategori)]
    public void EducationIdAlaniBosOlduguHaldeHataDonupDonmemeTesti()
    {
        _command.Id = null;
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "Id" && x.ErrorCode == ValidationErrorCodes.NotEmptyValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.NotEmptyValidator, result?.ErrorCode);
    }
    #endregion
    #region Name
    [Fact(DisplayName = "Eğitim Adı boş girildiğinde formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.BosVeriCategori)]
    public void EducationNameAlaniBosOlduguHaldeHataDonupDonmemeTesti()
    {
        _command.Name = string.Empty;
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "Name" && x.ErrorCode == ValidationErrorCodes.NotEmptyValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.NotEmptyValidator, result?.ErrorCode);
    }

    [Fact(DisplayName = "Eğitim Adı min karakter formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.MinKarakterCategori)]
    public void EducationNameAlaniMinKaraterHatasiTesti()
    {
        _command.Name = "D";
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "Name" && x.ErrorCode == ValidationErrorCodes.MinimumLengthValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.MinimumLengthValidator, result?.ErrorCode);
    }

    [Fact(DisplayName = "Eğitim Adı max karakter formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.MaxKarakterCategori)]
    public void EducationNameAlaniMaxKaraterHatasiTesti()
    {
        _command.Name = TestData.KaraterSayisi250DenFazlaData;
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "Name" && x.ErrorCode == ValidationErrorCodes.MaximumLengthValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.MaximumLengthValidator, result?.ErrorCode);
    }
    #endregion

    #region Degree
    [Fact(DisplayName = "Eğitim Notu (Degree) boş girildiğinde formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.BosVeriCategori)]
    public void EducationDegreeAlaniBosOlduguHaldeHataDonupDonmemeTesti()
    {
        _command.Degree = null;
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "Degree" && x.ErrorCode == ValidationErrorCodes.NotEmptyValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.NotEmptyValidator, result?.ErrorCode);
    }
    #endregion

    #region FieldOfStudy
    [Fact(DisplayName = "Eğitim Bölümü (FieldOfStudy) boş girildiğinde formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.BosVeriCategori)]
    public void EducationFieldOfStudyAlaniBosOlduguHaldeHataDonupDonmemeTesti()
    {
        _command.FieldOfStudy = string.Empty;
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "FieldOfStudy" && x.ErrorCode == ValidationErrorCodes.NotEmptyValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.NotEmptyValidator, result?.ErrorCode);
    }

    [Fact(DisplayName = "Eğitim Bölümü min karakter formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.MinKarakterCategori)]
    public void EducationFieldOfStudyAlaniMinKaraterHatasiTesti()
    {
        _command.FieldOfStudy = "D";
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "FieldOfStudy" && x.ErrorCode == ValidationErrorCodes.MinimumLengthValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.MinimumLengthValidator, result?.ErrorCode);
    }

    [Fact(DisplayName = "Eğitim Bölümü max karakter formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.MaxKarakterCategori)]
    public void EducationFieldOfStudyAlaniMaxKaraterHatasiTesti()
    {
        _command.FieldOfStudy = TestData.KaraterSayisi100DenFazlaData;
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "FieldOfStudy" && x.ErrorCode == ValidationErrorCodes.MaximumLengthValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.MaximumLengthValidator, result?.ErrorCode);
    }
    #endregion

    #region StartDate
    [Fact(DisplayName = "Eğitim Başlangıç Tarihi (StartDate) boş girildiğinde formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.BosVeriCategori)]
    public void EducationStartDateAlaniBosOlduguHaldeHataDonupDonmemeTesti()
    {
        _command.StartDate = null;
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "StartDate" && x.ErrorCode == ValidationErrorCodes.NotEmptyValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.NotEmptyValidator, result?.ErrorCode);
    }
    #endregion

    #region Grade
    [Fact(DisplayName = "Eğitim Derecesi (Grade) max karakter formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.MaxKarakterCategori)]
    public void EducationGradeAlaniMaxKaraterHatasiTesti()
    {
        _command.Grade = TestData.KaraterSayisi250DenFazlaData;
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "Grade" && x.ErrorCode == ValidationErrorCodes.MaximumLengthValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.MaximumLengthValidator, result?.ErrorCode);
    }
    #endregion

    #region ActivityAndCommunity
    [Fact(DisplayName = "Eğitim Faaliyet ve topluluklar (ActivityAndCommunity) max karakter formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.MaxKarakterCategori)]
    public void EducationActivityAndCommunityAlaniMaxKaraterHatasiTesti()
    {
        _command.ActivityAndCommunity = TestData.KaraterSayisi500DenFazlaData;
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "ActivityAndCommunity" && x.ErrorCode == ValidationErrorCodes.MaximumLengthValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.MaximumLengthValidator, result?.ErrorCode);
    }
    #endregion

    #region Description
    [Fact(DisplayName = "Eğitim Açıklama (Description) max karakter formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.MaxKarakterCategori)]
    public void EducationDescriptionAlaniMaxKaraterHatasiTesti()
    {
        _command.Description = TestData.KaraterSayisi1000DenFazlaData;
        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.Where(x => x.PropertyName == "Description" && x.ErrorCode == ValidationErrorCodes.MaximumLengthValidator)
            .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.MaximumLengthValidator, result?.ErrorCode);
    }
    #endregion
    #endregion

    [Fact(DisplayName = "Eğitim tablosunda başarılı veri güncelleme testi")]
    [Trait(TestCategories.CQRSCategori, TestCategories.UpdateCategori)]
    public async Task EducationTablosunaBasariliVeriGuncellemeTesti()
    {
        _command.Id = EducationTestData.UpdateId;
        _command.Name = EducationTestData.UpdateName;
        _command.FieldOfStudy = EducationTestData.UpdateFieldOfStudy;
        _command.Grade = EducationTestData.UpdateGrade;
        _command.StartDate = EducationTestData.UpdateStartDate;
        _command.EndDateOrExcepted = EducationTestData.UpdateEndDateOrExcepted;
        _command.Degree = EducationTestData.UpdateDegree;

        UpdatedEducationResponse result = await _handler.Handle(_command, CancellationToken.None);
        Assert.Equal(expected: EducationTestData.UpdateName, result.Name);
    }

    [Fact(DisplayName = "Eğitim tablosunda olmayan veriyi güncellemek istediğimizde BusinessRules Testi")]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.OlmayanVeriCategori)]
    public async Task EducationTablosundaOlmayanVeriyiGuncellemeTesti()
    {
        _command.Id = EducationTestData.NonexistentId;
        _command.Name = EducationTestData.UpdateName;
        _command.FieldOfStudy = EducationTestData.UpdateFieldOfStudy;
        _command.Grade = EducationTestData.UpdateGrade;
        _command.StartDate = EducationTestData.UpdateStartDate;
        _command.EndDateOrExcepted = EducationTestData.UpdateEndDateOrExcepted;
        _command.Degree = EducationTestData.UpdateDegree;

        await Assert.ThrowsAsync<BusinessException>(async () => await _handler.Handle(_command, CancellationToken.None));
    }
}