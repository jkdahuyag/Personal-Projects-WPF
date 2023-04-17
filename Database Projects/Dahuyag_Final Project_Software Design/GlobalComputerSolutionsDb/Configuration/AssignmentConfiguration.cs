using Bogus;
using GlobalComputerSolutionsDb.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalComputerSolutionsDb.Configuration;

public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
{
    public void Configure(EntityTypeBuilder<Assignment> d)
    {
        d.ToTable("Assignment");

        var assignments = GenerateData();

        d.HasIndex(c => new { c.EmployeeId, c.GcsTaskId }).IsUnique();

        d.HasOne(c => c.GcsTaskLink)
            .WithMany(c => c.Assignments)
            .OnDelete(DeleteBehavior.Restrict);

        d.HasData(assignments);
    }

    public List<Assignment> GenerateData()
    {
        var list = new List<Assignment>();

        var faker = new Faker();
        Randomizer.Seed = new Random(687104);

        for (int i = 1; i <= 1000; i++)
        {
            var assignment = new Assignment();
            assignment.AssignmentId = i;
            assignment.GcsTaskId = (i % 500) + 1;
            assignment.EmployeeId = (i + (int)Math.Floor(i / 500f)) % 100 + 1 ;
            var dateStarted = faker.Date.Past(5);
            assignment.DateStarted = dateStarted;
            assignment.DateEnded = dateStarted.AddDays(10);
            list.Add(assignment);
        }

        return list;
    }

}