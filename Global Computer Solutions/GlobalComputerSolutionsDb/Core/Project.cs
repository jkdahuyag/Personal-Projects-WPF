namespace GlobalComputerSolutionsDb.Core;

public class Project
{
    public int ProjectId { get; set; }
    public string Description { get; set; }
    public DateTime DateSigned { get; set; }
    public DateTime EstimatedDateStart { get; set; }
    public DateTime EstimatedDateEnd { get; set; }
    public float EstimatedBudget { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public float? TotalCost { get; set; }

    //links
    public int ManagerId { get; set; }
    public Employee ManagerLink { get; set; }
    public int CustomerId { get; set; }
    public Customer CustomerLink { get; set; }

    public ICollection<ProjectCost> ProjectCosts{ get; set; }
    public ICollection<ProjectSchedule> ProjectSchedules { get; set; }
        
}