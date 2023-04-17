namespace GlobalComputerSolutionsDb.Core;

public class Customer   
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Telephone { get; set; }

    //links
    public int RegionId { get; set; }
    public Region RegionLink { get; set; }

    public ICollection<Bill> Bills { get; set; }
    public ICollection<Project> Projects { get; set; }
}