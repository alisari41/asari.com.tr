using Application.Tests.Constants;
using Application.Tests.Features.Educations.Constants;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using asari.com.tr.Application.Features.Educations.Commands.Create;
using Core.Test.Application.Constants;
using FluentValidation.Results;
using System.Diagnostics;
using Xunit;
using static asari.com.tr.Application.Features.Educations.Commands.Create.CreateEducationCommand;

namespace Application.Tests.Features.Educations.Commands.Create;

public class CreateEducationTests : EducationMockRepository
{
    private readonly CreateEducationCommand _command;
    private readonly CreateEducationCommandValidator _validator;
    private readonly CreateEducationCommandHandler _handler;

    public CreateEducationTests(EducationFakeData fakeData, CreateEducationCommand command, CreateEducationCommandValidator validator) : base(fakeData)
    {
        _command = command;
        _validator = validator;
        _handler = new CreateEducationCommandHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    #region FluentValidation - Formatlama Testleri
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


    [Fact(DisplayName = "Eğitim tablosuna başarılı veri ekleme testi")]
    [Trait(TestCategories.CQRSCategori, TestCategories.CreateCategori)]
    public async Task EducationTablosunaBasariliVeriEklemeTesti()
    {
        _command.Name = EducationTestData.CreateName;
        _command.FieldOfStudy = EducationTestData.CreateFieldOfStudy;
        _command.Grade = EducationTestData.CreateGrade;
        _command.StartDate = EducationTestData.CreateStartDate;
        _command.EndDateOrExcepted = EducationTestData.CreateEndDateOrExcepted;
        _command.Degree = EducationTestData.CreateDegree;

        CreatedEducationResponse result = await _handler.Handle(_command, CancellationToken.None);
        Assert.Equal(expected: EducationTestData.CreateName, result.Name);
    }
}