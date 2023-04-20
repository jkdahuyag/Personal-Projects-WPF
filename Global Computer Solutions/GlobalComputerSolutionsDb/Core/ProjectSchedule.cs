namespace GlobalComputerSolutionsDb.Core;

public class ProjectSchedule
{
    public int ProjectScheduleId { get; set; }
    public string Description { get; set; }
    public DateTime DateMade { get; set; }

    //links
    public int ProjectId { get; set; }
    public Project ProjectLink { get; set; }

    public ICollection<GcsTask> GcsTasks { get; set; }

}