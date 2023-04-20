using GlobalComputerSolutionsDb.Core;

namespace GlobalComputerSolutionsApp.Dto;

public class CustomerName
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string Region { get; set; }

    public CustomerName(Customer c)
    {
        CustomerId = c.CustomerId;
        Region = c.RegionLink.Name.Substring(c.RegionLink.Name.Length - 3, 2);
        Name = $"{c.FirstName} {c.MiddleName[0]}. {c.LastName}, {Region}";
    }
}