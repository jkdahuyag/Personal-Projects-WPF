using Bogus;
using GlobalComputerSolutionsDb.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalComputerSolutionsDb.Configuration;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> d)
    {
        d.ToTable("Project");

        d.Property(c => c.Description).HasMaxLength(200);

        d.HasOne(c => c.ManagerLink)
            .WithMany(c => c.Projects)
            .HasForeignKey("ManagerId")
            .OnDelete(DeleteBehavior.Restrict);

        var projects = GenerateData();

        d.HasData(projects);
    }

    public List<Project> GenerateData()
    {
        var list = new List<Project>();

        var faker = new Faker();
        Randomizer.Seed = new Random(010403);
        for (int i = 1; i <= 300; i++)
        {
            var project = new Project();
            project.ProjectId = i;
            project.Description = faker.Random.Words(3);
            project.ManagerId = (i % 100) + 1;
            project.CustomerId = (i + (int)Math.Floor(i / 50f)) % 200 + 1 ;
            project.DateSigned = faker.Date.Past();
            project.EstimatedDateStart = faker.Date.Past(5);
            project.EstimatedDateEnd = project.EstimatedDateStart.AddDays(faker.Random.Number(100, 1000));
            project.EstimatedBudget = (float)faker.Finance.Amount(1000000, 100000000);
            project.DateStart = project.EstimatedDateStart.AddDays(faker.Random.Number(0, 30));
            project.DateEnd = project.EstimatedDateEnd.AddDays(faker.Random.Number(0, 100));

            list.Add(project);
        }

        return list;
    }
}