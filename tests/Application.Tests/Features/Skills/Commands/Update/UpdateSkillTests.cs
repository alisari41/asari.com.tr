using Application.Tests.Constants;
using Application.Tests.Features.Skills.Constants;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using asari.com.tr.Application.Features.Skills.Commands.Update;
using asari.com.tr.Application.Features.Skills.Constants;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.Results;
using Xunit;
using static asari.com.tr.Application.Features.Skills.Commands.Update.UpdateSkillCommand;

namespace Application.Tests.Features.Skills.Commands.Update;

public class UpdateSkillTests : SkillMockRepository
{
    private readonly UpdateSkillCommand _command;
    private readonly UpdateSkillCommandValidator _validator;
    private readonly UpdateSkillCommandHandler _handler;

    public UpdateSkillTests(SkillFakeData fakeData, UpdateSkillCommand command, UpdateSkillCommandValidator validator) : base(fakeData)
    {
        _command = command;
        _validator = validator;
        _handler = new UpdateSkillCommandHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    #region FluentValidation - Formatlama Testleri
    #region Id
    [Fact(DisplayName = "Yetenek Id boş girildiğinde formatlama hatası veriyor mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.BosVeriCategori)]
    public void SkillIdAlaniBosOlduguHaldeHataDonupDonmemeTesti()
    {
        _command.Id = null;
        ValidationResult result = _validator.Validate(_command);
        Assert.Contains(SkillMessages.IdBosOlmamali, result.Errors.Select(error => error.ErrorMessage));
    }
    #endregion

    #region Name
    [Fact(DisplayName = "Yetenek Adı boş girildiğinde formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.BosVeriCategori)]
    public void SkillNameAlaniBosOlduguHaldeHataDonupDonmemeTesti()
    {
        _command.Name = string.Empty;
        ValidationResult result = _validator.Validate(_command);
        Assert.Contains(SkillMessages.NameBosOlmamali, result.Errors.Select(error => error.ErrorMessage));
    }

    [Fact(DisplayName = "Yetenek Adı max karakter formatlama hatası veriyoru mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.MaxKarakterCategori)]
    public void SkillNameAlaniMaxKaraterHatasiTesti()
    {
        _command.Name = TestData.KaraterSayisi250DenFazlaData;
        ValidationResult result = _validator.Validate(_command);
        Assert.Contains(SkillMessages.NameMaxKarakter, result.Errors.Select(error => error.ErrorMessage));
    }
    #endregion

    #region Degree
    [Fact(DisplayName = "Yetenek Derecesi boş girildiğinde formatlama hatası veriyor mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.BosVeriCategori)]
    public void SkillDegreeAlaniBosOlduguHaldeHataDonupDonmemeTesti()
    {
        _command.Degree = null;
        ValidationResult result = _validator.Validate(_command);
        Assert.Contains(SkillMessages.DegreeBosOlmamali, result.Errors.Select(error => error.ErrorMessage));
    }

    [Fact(DisplayName = "Yetenek Derecesi virgülden sonra bir karakterden fazla ise formatlama hatası veriyor mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.MaxKarakterCategori)]
    public void SkillDegreeVirguldenSonraBirKarakterdenFazlaHatasiTesti()
    {
        _command.Degree = 5.123;
        ValidationResult result = _validator.Validate(_command);
        Assert.Contains(SkillMessages.DegreeVirguldenSonraMaxKarakter, result.Errors.Select(error => error.ErrorMessage));
    }

    [Fact(DisplayName = "Yetenek Derecesi 10'dan fazla ise formatlama hatası veriyor mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.MaxKarakterCategori)]
    public void SkillDegreeMaxKarakterHatasiTesti()
    {
        _command.Degree = 15.0;
        ValidationResult result = _validator.Validate(_command);
        Assert.Contains(SkillMessages.DegreeMaxKarakter, result.Errors.Select(error => error.ErrorMessage));
    }
    #endregion
    #endregion


    [Fact(DisplayName = "Yetenek tablosunda olan yeteneği adında başka veri güncellemek istediğimizde BusinessRules Testi")]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.DuplicateVeriCategori)]
    public async Task SkillTablosundaOlanIsimdeVerigüncellemeTesti()
    {
        _command.Id = SkillTestData.UpdateId;
        _command.Name = SkillTestData.UpdateVarolanName;

        var exception = await Assert.ThrowsAsync<BusinessException>(async () => await _handler.Handle(_command, CancellationToken.None));

        Assert.Contains(SkillMessages.YetenekMevcut, exception.Message);
    }

    [Fact(DisplayName = "Yetenek tablosuna başarılı veri güncelleme testi")]
    [Trait(TestCategories.CQRSCategori, TestCategories.UpdateCategori)]
    public async Task SkillTablosunaBasariliVerigüncellemeTesti()
    {
        _command.Id = SkillTestData.UpdateId;
        _command.Name = SkillTestData.UpdateName;
        _command.Degree = SkillTestData.UpdateDegree;

        UpdatedSkillResponse result = await _handler.Handle(_command, CancellationToken.None);
        Assert.Equal(expected: SkillTestData.UpdateName, result.Name);
    }

    [Fact(DisplayName = "Yetenek tablosunda olmayan veriyi güncellemek istediğimizde BusinessRules Testi")]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.OlmayanVeriCategori)]
    public async Task SkillTablosundaOlmayanVeriyiGuncellemeTesti()
    {
        _command.Id = SkillTestData.NonexistentId;
        _command.Name = SkillTestData.UpdateName;
        _command.Degree = SkillTestData.UpdateDegree;

        var exception = await Assert.ThrowsAsync<BusinessException>(async () => await _handler.Handle(_command, CancellationToken.None));

        Assert.Contains(SkillMessages.YetenekMevcutDegil, exception.Message);
    }
}