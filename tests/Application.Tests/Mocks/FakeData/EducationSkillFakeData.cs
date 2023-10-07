using asari.com.tr.Domain.Entities;
using Core.Test.Application.FakeData;

namespace Application.Tests.Mocks.FakeData;

public class EducationSkillFakeData : BaseFakeData<EducationSkill>
{
    public override List<EducationSkill> CreateFakeData()
    {
        var data = new List<EducationSkill>
        {
            new()
            {
                Id = 1,
                EducationId = 1,
                SkillId = 1
            },
            new()
            {
                Id = 2,
                EducationId = 2,
                SkillId = 2
            }
        };
        return data;
    }
}