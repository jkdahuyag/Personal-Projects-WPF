namespace GlobalComputerSolutionsDb.Core;

public class Region 
{
    public int RegionId { get; set; }
    public string Name { get; set; }

    //links
    public ICollection<Customer> Customers { get; set; }
    public ICollection<Employee> Employees { get; set; }
}