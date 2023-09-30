using asari.com.tr.Domain.Entities;
using Core.Test.Application.FakeData;

namespace Application.Tests.Mocks.FakeData;

public class SkillFakeData : BaseFakeData<Skill>
{
    public override List<Skill> CreateFakeData()
    {
        var data = new List<Skill>
        {
            new ()
            {
                Id = 1,
                Name = "C#",
                Degree = 4.5
            },
            new ()
            {
                Id = 2,
                Name = "Java",
                Degree = 3.8
            }
        };
        return data;
    }
}