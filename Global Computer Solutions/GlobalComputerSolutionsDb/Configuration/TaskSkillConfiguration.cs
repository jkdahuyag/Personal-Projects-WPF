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

        var skillIds = Enumerable.Range(1, 23);
        var taskIds = Enumerable.Range(1, 500);

        var combinations = from a in taskIds
	        from b in skillIds
						   select new { a, b };

        var grouped = combinations.GroupBy(c => c.a).ToList();

        var count = 1;

        foreach (var item in grouped)
        {
	        var shuffle = faker.Random.Shuffle(item).ToList();
	        int min = 1;
	        int max = 5;
	        var selected = shuffle.Take(faker.Random.Number(min, max));
	        foreach (var i in selected)
	        {
		        var taskSkill = new TaskSkill();
		        taskSkill.TaskSkillId = count;
		        count++;
		        taskSkill.GcsTaskId = i.a;
		        taskSkill.SkillId = i.b;
		        taskSkill.Quantity = faker.Random.Number(1, 5);
		        list.Add(taskSkill);
	        }
        }
        return list;
    }

}