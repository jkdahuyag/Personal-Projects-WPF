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

        for (int i = 1; i <= 300; i++)
        {
            var skillInventory = new SkillInventory();
            skillInventory.SkillInventoryId = i; 
            skillInventory.EmployeeId = ((i-1) % 100) + 1;
            skillInventory.SkillId = (((i - 1) + (int)Math.Floor((i - 1) / 100f)) % 20) + 1 ;
            skillInventory.DateEarned = faker.Date.Past(20);
            list.Add(skillInventory);
        }

        return list;
    }
}