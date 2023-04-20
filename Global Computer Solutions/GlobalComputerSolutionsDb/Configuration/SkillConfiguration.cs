using Bogus;
using GlobalComputerSolutionsDb.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalComputerSolutionsDb.Configuration;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> d)
    {
        d.ToTable("Skill");

        d.Property(c => c.Description).HasMaxLength(200);

        var skills = GenerateData();

        d.HasData(skills);
    }

    public List<Skill> GenerateData()
    {
        var list = new List<Skill>();

        var faker = new Faker();
        Randomizer.Seed = new Random(12563);

        var skills = new[] {
            "data entry I", " data entry II", " systems analyst I", " systems analyst II", " database designer I",
            " database designer II", " Cobol I", " Cobol II", " C++I",  "project manager"," C++II", " VB I", " VB II", " ColdFusion I",
            " ColdFusion II", " ASP I", " ASP II", " Oracle DBA", " MS SQL Server DBA", " network engineer I",
            " network engineer II", " Web administrator", " technical writer",
        };

        for (int i = 1; i <= skills.Length; i++)
        {
            var skill = new Skill();
            skill.SkillId = i;
            skill.Description = skills[i-1];
            skill.PayRate = (float)faker.Finance.Amount(500);
            list.Add(skill);
        }

        return list;
    }
}