using Bogus;
using GlobalComputerSolutionsDb.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalComputerSolutionsDb.Configuration;

public class WorkLogConfiguration : IEntityTypeConfiguration<WorkLog>
{
    public void Configure(EntityTypeBuilder<WorkLog> d)
    {
        d.ToTable("WorkLog");

        d.HasIndex(c => new { c.WeekEnding, c.AssignmentId }).IsUnique();

        d.HasOne(c => c.BillLink)
            .WithMany(c => c.WorkLogs)
            .OnDelete(DeleteBehavior.Restrict);

        var workLogs = GenerateData();

        d.HasData(workLogs);
    }
    public List<WorkLog> GenerateData()
    {
        var list = new List<WorkLog>();

        var faker = new Faker();
        Randomizer.Seed = new Random(827821);

        var assignmentIds = Enumerable.Range(1, 500);
        var billIds = Enumerable.Range(1, 200);

        var combinations = from a in billIds
	        from b in assignmentIds
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
		        var workLog = new WorkLog();
		        workLog.WorkLogId = count;
		        count++;
		        workLog.BillId = i.a;
		        workLog.AssignmentId = i.b;
		        workLog.HoursWorked = faker.Random.Number(0, 24);
		        var weekEnding = faker.Date.Past(5);
		        if (weekEnding.DayOfWeek != DayOfWeek.Friday)
		        {
			        weekEnding = weekEnding.AddDays(DayOfWeek.Friday - weekEnding.DayOfWeek);
		        }

		        workLog.WeekEnding = weekEnding;
		        list.Add(workLog);
	        }
		}

        return list;
    }

}