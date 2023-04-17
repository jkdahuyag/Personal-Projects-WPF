using Bogus;
using GlobalComputerSolutionsDb.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalComputerSolutionsDb.Configuration;

public class GcsTaskConfiguration : IEntityTypeConfiguration<GcsTask>
{
    public void Configure(EntityTypeBuilder<GcsTask> d)
    {
        d.ToTable("GcsTask");

        d.Property(c => c.Description).HasMaxLength(200);

        var gcsTasks = GenerateData();

        d.HasOne(c => c.ProjectScheduleLink)
            .WithMany(c => c.GcsTasks)
            .OnDelete(DeleteBehavior.Cascade);

        d.HasData(gcsTasks);

    }

    public List<GcsTask> GenerateData()
    {
        var list = new List<GcsTask>();

        var faker = new Faker();
        Randomizer.Seed = new Random(614371);
        for (int i = 1; i <= 500; i++)
        {
            var gcsTask = new GcsTask();
            gcsTask.GcsTaskId = i;
            gcsTask.Description = faker.Name.JobTitle();
            gcsTask.ProjectScheduleId = faker.Random.Number(1, 500);
            gcsTask.DateStart = faker.Date.Past(5);
            gcsTask.DateEnd = gcsTask.DateStart.AddDays(faker.Random.Number(5, 10));
            list.Add(gcsTask);
        }

        return list;
    }

}