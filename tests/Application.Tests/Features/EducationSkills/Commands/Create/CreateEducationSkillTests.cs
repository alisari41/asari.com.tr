using Application.Tests.Constants;
using Application.Tests.Features.EducationSkills.Constants;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using asari.com.tr.Application.Features.Educations.Rules;
using asari.com.tr.Application.Features.EducationSkills.Commands.Create;
using asari.com.tr.Application.Features.EducationSkills.Constants;
using asari.com.tr.Application.Features.Skills.Rules;
using FluentValidation.Results;
using Xunit;
using static asari.com.tr.Application.Features.EducationSkills.Commands.Create.CreateEducationSkillCommand;

namespace Application.Tests.Features.EducationSkills.Commands.Create;

public class CreateEducationSkillTests : EducationSkillMockRepository
{
    private readonly CreateEducationSkillCommand _command;
    private readonly CreateEducationSkillCommandValidator _validator;
    private readonly CreateEducationSkillCommandHandler _handler;
    private readonly EducationBusinessRules _educationBusinessRules;
    private readonly SkillBusinessRules _skillBusinessRules;

    public CreateEducationSkillTests(EducationSkillFakeData fakeData, CreateEducationSkillCommand command, CreateEducationSkillCommandValidator validator) : base(fakeData)
    {
        _command = command;
        _validator = validator;
        //_skillBusinessRules = skillBusinessRules;
        //_educationBusinessRules = educationBusinessRules;
        _handler = new CreateEducationSkillCommandHandler(MockRepository.Object, Mapper, BusinessRules, _educationBusinessRules, _skillBusinessRules);
    }

    #region FluentValidation - Formatlama Testleri
    #region SkillId
    [Fact(DisplayName = "Yetenek Id boş girildiğinde formatlama hatası veriyor mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.BosVeriCategori)]
    public void SkillIdAlaniBosOlduguHaldeHataDonupDonmemeTesti()
    {
        _command.SkillId = null;
        _command.EducationId = EducationSkillTestData.CreateEducationId;
        ValidationResult result = _validator.Validate(_command);
        Assert.Contains(EducationSkillMessages.SkillIdBosOlmamali, result.Errors.Select(error => error.ErrorMessage));
    }
    #endregion

    #region EducationId
    [Fact(DisplayName = "Eğitim Id boş girildiğinde formatlama hatası veriyor mu testi")]
    [Trait(TestCategories.FluentValidationCategori, TestCategories.BosVeriCategori)]
    public void EducationIdAlaniBosOlduguHaldeHataDonupDonmemeTesti()
    {
        _command.EducationId = null;
        _command.SkillId = EducationSkillTestData.CreateSkillId;
        ValidationResult result = _validator.Validate(_command);
        Assert.Contains(EducationSkillMessages.EducationIdBosOlmamali, result.Errors.Select(error => error.ErrorMessage));
    }
    #endregion
    #endregion

    [Fact(DisplayName = "Rğitim Yetenek tablosuna başarılı veri ekleme testi")]
    [Trait(TestCategories.CQRSCategori, TestCategories.CreateCategori)]
    public async Task EducationSkillTablosunaBasariliVeriEklemeTesti()
    {
        _command.SkillId = EducationSkillTestData.CreateSkillId;
        _command.EducationId = EducationSkillTestData.CreateEducationId;

        CreatedEducationSkillResponse result = await _handler.Handle(_command, CancellationToken.None);
        Assert.Equal(expected: EducationSkillTestData.CreateSkillId, result.SkillId);
    }
}