﻿using Application.Tests.Constants;
using Application.Tests.Features.Skills.Constants;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using asari.com.tr.Application.Features.Skills.Commands.Create;
using asari.com.tr.Application.Features.Skills.Constants;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using static asari.com.tr.Application.Features.Skills.Commands.Create.CreateSkillCommand;

namespace Application.Tests.Features.Skills.Commands.Create;

public class CreateSkillTests : SkillMockRepository
{
    private readonly CreateSkillCommand _command;
    private readonly CreateSkillCommandValidator _validator;
    private readonly CreateSkillCommandHandler _handler;

    public CreateSkillTests(SkillFakeData fakeData, CreateSkillCommand command, CreateSkillCommandValidator validator) : base(fakeData)
    {
        _command = command;
        _validator = validator;
        _handler = new CreateSkillCommandHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    #region FluentValidation - Formatlama Testleri
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

    [Fact(DisplayName = "Yetenek tablosuna başarılı veri ekleme testi")]
    [Trait(TestCategories.CQRSCategori, TestCategories.CreateCategori)]
    public async Task SkillTablosunaBasariliVeriEklemeTesti()
    {
        _command.Name = SkillTestData.CreateName;
        _command.Degree = SkillTestData.CreateDegree;

        CreatedSkillResponse result = await _handler.Handle(_command, CancellationToken.None);
        Assert.Equal(expected: SkillTestData.CreateName, result.Name);
    }

    [Fact(DisplayName = "Yetenek tablosunda olan yeteneği tekrar eklemek istediğimizde BusinessRules Testi")]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.DuplicateVeriCategori)]
    public async Task SkillTablosundaOlanIsimdeVeriEklemeTesti()
    {
        _command.Name = SkillTestData.CreateVarolanName;
        _command.Degree = SkillTestData.CreateDegree;

        var exception = await Assert.ThrowsAsync<BusinessException>(async () => await _handler.Handle(_command, CancellationToken.None));

        Assert.Contains(SkillMessages.YetenekMevcut, exception.Message);
    }
}