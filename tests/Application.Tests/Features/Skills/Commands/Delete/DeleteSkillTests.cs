using Application.Tests.Constants;
using Application.Tests.Features.Skills.Constants;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using asari.com.tr.Application.Features.Skills.Commands.Delete;
using asari.com.tr.Application.Features.Skills.Constants;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.Results;
using Xunit;
using static asari.com.tr.Application.Features.Skills.Commands.Delete.DeleteSkillCommand;

namespace Application.Tests.Features.Skills.Commands.Delete;

public class DeleteSkillTests : SkillMockRepository
{
    private readonly DeleteSkillCommand _command;
    private readonly DeleteSkillCommandValidator _validator;
    private readonly DeleteSkillCommandHandler _handler;

    public DeleteSkillTests(SkillFakeData fakeData, DeleteSkillCommandValidator validator, DeleteSkillCommand command) : base(fakeData)
    {
        _command = command;
        _validator = validator;
        _handler = new DeleteSkillCommandHandler(MockRepository.Object, Mapper, BusinessRules);
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
    #endregion

    [Fact(DisplayName = "Yetenek tablosunda olmayan veriyi silmek istediğimizde BusinessRules Testi")]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.OlmayanVeriCategori)]
    public async Task SkillTablosundaOlmayanVeriyiSilmeTesti()
    {
        _command.Id = SkillTestData.NonexistentId;

        var exception = await Assert.ThrowsAsync<BusinessException>(async () => await _handler.Handle(_command, CancellationToken.None));

        Assert.Contains(SkillMessages.YetenekMevcutDegil, exception.Message);
    }

    [Fact(DisplayName = "Yetenek tablosunda başarılı veri silme testi")]
    [Trait(TestCategories.CQRSCategori, TestCategories.DeleteCategori)]
    public async Task SkillTablosunaBasariliVeriSilmeTesti()
    {
        _command.Id = SkillTestData.DeleteId;

        DeletedSkillResponse result = await _handler.Handle(_command, CancellationToken.None);
        Assert.NotNull(result);
    }
}