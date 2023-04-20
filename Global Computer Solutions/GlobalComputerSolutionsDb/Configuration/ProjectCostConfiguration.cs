using Bogus;
using GlobalComputerSolutionsDb.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalComputerSolutionsDb.Configuration;

public class ProjectCostConfiguration : IEntityTypeConfiguration<ProjectCost>
{
    public void Configure(EntityTypeBuilder<ProjectCost> d)
    {
        d.ToTable("ProjectCost");

        var projectSchedules = GenerateData();

        d.HasData(projectSchedules);
    }
    public List<ProjectCost> GenerateData()
    {
        var list = new List<ProjectCost>();

        var faker = new Faker();
        Randomizer.Seed = new Random(598135);

        for (int i = 1; i <= 500; i++)
        {
            var projectCost = new ProjectCost();
            projectCost.ProjectCostId = i;
            projectCost.ProjectId = faker.Random.Number(1, 300);
            //projectSchedule.ManagerId = (i % 1000) + 1 + (int)Math.Floor(i / 1000f);
            projectCost.DateEnding = faker.Date.Past(5);
            list.Add(projectCost);
        }

        return list;
    }
}