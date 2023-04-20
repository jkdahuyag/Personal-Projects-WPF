namespace GlobalComputerSolutionsDb.Core;

public class ProjectCost
{
    public int ProjectCostId { get; set; }
    public float TotalCost { get; set; }
    public DateTime DateEnding { get; set; }

    //links
    public int ProjectId { get; set; }
    public Project ProjectLink { get; set; }
}