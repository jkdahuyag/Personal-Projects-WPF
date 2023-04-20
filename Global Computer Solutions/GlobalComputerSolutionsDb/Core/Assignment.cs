namespace GlobalComputerSolutionsDb.Core;

public class Assignment 
{
    public int AssignmentId { get; set; }
    public DateTime? DateStarted { get; set; }
    public DateTime? DateEnded { get; set; }

    //links
    public int EmployeeId { get; set; }
    public Employee EmplpoyeeLink { get; set; }
    public int GcsTaskId { get; set; }
    public GcsTask GcsTaskLink { get; set; }

    public ICollection<WorkLog> WorkLogs { get; set; }
}