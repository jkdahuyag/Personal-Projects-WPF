using Bogus;
using GlobalComputerSolutionsDb.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalComputerSolutionsDb.Configuration;

public class TaskSkillConfiguration : IEntityTypeConfiguration<TaskSkill>
{
    public void Configure(EntityTypeBuilder<TaskSkill> d)
    {
        d.ToTable("TaskSkill");

        var taskSkills = GenerateData();
        d.HasIndex(c => new { c.SkillId, c.GcsTaskId }).IsUnique();

        d.HasData(taskSkills);
    }
    public List<TaskSkill> GenerateData()
    {
        var list = new List<TaskSkill>();

        var faker = new Faker();
        Randomizer.Seed = new Random(267146);

        for (int i = 1; i <= 700; i++)
        {
            var taskSkill = new TaskSkill();
            taskSkill.TaskSkillId = i;
            taskSkill.GcsTaskId = i % 500 + 1;
            taskSkill.SkillId = (i + (int)Math.Floor(i / 500f) )% 23 + 1 ;
            taskSkill.Quantity = faker.Random.Number(1,5);
            list.Add(taskSkill);
        }

        return list;
    }

}