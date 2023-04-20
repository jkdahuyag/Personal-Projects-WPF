namespace GlobalComputerSolutionsDb.Core;

public class Bill  
{
    public int BillId { get; set; }
    public DateTime DateSent { get; set; }

    //links
    public int CustomerId { get; set; }
    public Customer CustomerLink { get; set; }

    public ICollection<WorkLog> WorkLogs { get; set; }
}