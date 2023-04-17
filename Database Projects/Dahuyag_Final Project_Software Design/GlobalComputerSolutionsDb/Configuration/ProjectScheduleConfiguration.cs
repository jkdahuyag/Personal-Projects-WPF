using Bogus;
using GlobalComputerSolutionsDb.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalComputerSolutionsDb.Configuration;

public class ProjectScheduleConfiguration : IEntityTypeConfiguration<ProjectSchedule>
{
    public void Configure(EntityTypeBuilder<ProjectSchedule> d)
    {
        d.ToTable("ProjectSchedule");

        var projectSchedules = GenerateData();

        d.HasOne(c => c.ProjectLink)
            .WithMany(c => c.ProjectSchedules)
            .OnDelete(DeleteBehavior.Cascade);

        d.HasData(projectSchedules);
    }
    public List<ProjectSchedule> GenerateData()
    {
        var list = new List<ProjectSchedule>();

        var faker = new Faker();
        Randomizer.Seed = new Random(12656);

        for (int i = 1; i <= 500; i++)
        {
            var projectSchedule = new ProjectSchedule();
            projectSchedule.ProjectScheduleId = i;
            projectSchedule.ProjectId = (i % 300) + 1;
            //projectSchedule.ManagerId = (i % 1000) + 1 + (int)Math.Floor(i / 1000f);
            projectSchedule.Description = faker.Name.JobDescriptor();
            list.Add(projectSchedule);
        }

        return list;
    }
}