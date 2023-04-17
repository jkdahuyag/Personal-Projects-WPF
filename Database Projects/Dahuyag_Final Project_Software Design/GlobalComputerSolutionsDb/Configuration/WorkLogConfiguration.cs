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

        for (int i = 1; i <= 3000; i++)
        {
            var workLog = new WorkLog();
            workLog.WorkLogId = i;
            workLog.AssignmentId = i % 1000 + 1;
            workLog.BillId = (i + (int)Math.Floor(i / 1000f) )% 500 + 1 ;
            workLog.HoursWorked = faker.Random.Number(0, 24);
            var weekEnding = faker.Date.Past(5);
            if (weekEnding.DayOfWeek != DayOfWeek.Friday)
            {
                weekEnding = weekEnding.AddDays(DayOfWeek.Friday - weekEnding.DayOfWeek);
            }

            workLog.WeekEnding = weekEnding;
            list.Add(workLog);
        }

        return list;
    }

}