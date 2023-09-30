using Application.Tests.Constants;
using Application.Tests.Features.Experiences.Constants;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using asari.com.tr.Application.Features.Experiences.Commands.Delete;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Test.Application.Constants;
using FluentValidation.Results;
using Xunit;
using static asari.com.tr.Application.Features.Experiences.Commands.Delete.DeleteExperienceCommand;

namespace Application.Tests.Features.Experiences.Commands.Delete;

public class DeleteExperienceTests : ExperienceMockRepository
{
    private readonly DeleteExperienceCommand _command;
    private readonly DeleteExperienceCommandValidator _validator;
    private readonly DeleteExperienceCommandHandler _handler;

    public DeleteExperienceTests(ExperienceFakeData fakeData, DeleteExperienceCommandValidator validator, DeleteExperienceCommand command) : base(fakeData)
    {
        _validator = validator;
        _command = command;
        _handler = new DeleteExperienceCommandHandler(MockRepository.Object, Mapper, BusinessRules);
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
    #endregion

    [Fact(DisplayName = "Deneyim tablosunda başarılı veri silme testi")]
    [Trait(TestCategories.CQRSCategori, TestCategories.DeleteCategori)]
    public async Task ExperienceTablosunaBasariliVeriSilmeTesti()
    {
        _command.Id = ExperienceTestData.DeleteId;

        DeletedExperienceResponse result = await _handler.Handle(_command, CancellationToken.None);
        Assert.NotNull(result);
    }

    [Fact(DisplayName = "Deneyim tablosunda olmayan veriyi silmek istediğimizde BusinessRules Testi")]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.OlmayanVeriCategori)]
    public async Task ExperienceTablosundaOlmayanVeriyiSilmeTesti()
    {
        _command.Id = ExperienceTestData.NonexistentId;

        await Assert.ThrowsAsync<BusinessException>(async () => await _handler.Handle(_command, CancellationToken.None));
    }
}