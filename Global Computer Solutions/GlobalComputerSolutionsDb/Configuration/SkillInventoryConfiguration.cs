using Bogus;
using GlobalComputerSolutionsDb.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalComputerSolutionsDb.Configuration;

public class SkillInventoryConfiguration : IEntityTypeConfiguration<SkillInventory>
{
    public void Configure(EntityTypeBuilder<SkillInventory> d)
    {
        d.ToTable("SkillInventory"); 
            
        var skillInventories = GenerateData();

        d.HasIndex(c => new { c.EmployeeId, c.SkillId }).IsUnique();

        d.HasData(skillInventories);

    }

    public List<SkillInventory> GenerateData()
    {
        var list = new List<SkillInventory>();

        var faker = new Faker();
        Randomizer.Seed = new Random(147527);

        var employeeIds = Enumerable.Range(1, 100);
        var skillIds = Enumerable.Range(1, 23);

        var combinations = from a in employeeIds
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
				var skillInventory = new SkillInventory();
				skillInventory.SkillInventoryId = count;
				count++;
				skillInventory.EmployeeId = i.a;
				skillInventory.SkillId = i.b;
				skillInventory.DateEarned = faker.Date.Past(20);
                list.Add(skillInventory);
	        }
		}
        return list;
    }
}