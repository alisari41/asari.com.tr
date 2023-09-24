using Application.Tests.Constants;
using Application.Tests.Features.Educations.Constants;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using asari.com.tr.Application.Features.Educations.Commands.Delete;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Test.Application.Constants;
using FluentValidation.Results;
using Xunit;
using static asari.com.tr.Application.Features.Educations.Commands.Delete.DeleteEducationCommand;

namespace Application.Tests.Features.Educations.Commands.Delete;

public class DeleteEducationTests : EducationMockRepository
{
    private readonly DeleteEducationCommand _command;
    private readonly DeleteEducationCommandValidator _validator;
    private readonly DeleteEducationCommandHandler _handler;

    public DeleteEducationTests(EducationFakeData fakeData, DeleteEducationCommandValidator validator, DeleteEducationCommand command) : base(fakeData)
    {
        _validator = validator;
        _command = command;
        _handler = new DeleteEducationCommandHandler(MockRepository.Object, Mapper, BusinessRules);
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
    #endregion

    [Fact(DisplayName = "Eğitim tablosunda başarılı veri silme testi")]
    [Trait(TestCategories.CQRSCategori, TestCategories.DeleteCategori)]
    public async Task EducationTablosunaBasariliVeriSilmeTesti()
    {
        _command.Id = EducationTestData.DeleteId;

        DeletedEducationResponse result = await _handler.Handle(_command, CancellationToken.None);
        Assert.NotNull(result);
    }

    [Fact(DisplayName = "Eğitim tablosunda olmayan veriyi silmek istediğimizde BusinessRules Testi")]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.OlmayanVeriCategori)]
    public async Task EducationTablosundaOlmayanVeriyiSilmeTesti()
    {
        _command.Id = EducationTestData.DeleteBulunmayanId;

        await Assert.ThrowsAsync<BusinessException>(async () => await _handler.Handle(_command, CancellationToken.None));
    }
}