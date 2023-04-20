namespace GlobalComputerSolutionsDb.Core;

public class GcsTask
{
    public int GcsTaskId { get; set; }
    public string Description { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }

    //links
    public int ProjectScheduleId { get; set; }
    public ProjectSchedule ProjectScheduleLink { get; set; }

    public ICollection<Assignment> Assignments { get; set; }
    public ICollection<TaskSkill> TaskSkills { get; set; }
}