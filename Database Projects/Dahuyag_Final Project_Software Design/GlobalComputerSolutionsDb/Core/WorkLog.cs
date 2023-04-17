namespace GlobalComputerSolutionsDb.Core;

public class WorkLog    
{
    public int WorkLogId { get; set; }
    public int HoursWorked { get; set; }
    public DateTime WeekEnding { get; set; }

    //links
    public int AssignmentId { get; set; }
    public Assignment AssignmentLink { get; set; }
    public int BillId { get; set; }
    public Bill BillLink { get; set; }

}