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

        var employeeIds = Enumerable.Range(1, 100);
        var taskIds = Enumerable.Range(1, 500);

        var combinations = from a in employeeIds
	        from b in taskIds
	        select new { a, b };

        var grouped = combinations.GroupBy(c => c.a).ToList();

        var count = 1;

        foreach (var item in grouped)
        {
	        var shuffle = faker.Random.Shuffle(item).ToList();
	        int min = 5;
	        int max = 10;
	        var selected = shuffle.Take(faker.Random.Number(min, max));
	        foreach (var i in selected)
	        {
		        var assignment = new Assignment();
		        assignment.AssignmentId = count;
		        count++;
		        assignment.EmployeeId = i.a;
		        assignment.GcsTaskId = i.b;
		        var dateStarted = faker.Date.Past(5);
		        assignment.DateStarted = dateStarted;
		        assignment.DateEnded = dateStarted.AddDays(10);
		        list.Add(assignment);
	        }
		}
        return list;
    }

}