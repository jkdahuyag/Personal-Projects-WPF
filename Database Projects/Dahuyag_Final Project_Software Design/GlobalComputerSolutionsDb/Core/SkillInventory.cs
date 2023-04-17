namespace GlobalComputerSolutionsDb.Core;

public class SkillInventory  
{
    public int SkillInventoryId { get; set; }
    public DateTime DateEarned { get; set; }

    public int SkillId { get; set; }    
    public Skill SkillLink { get; set; }
    public int EmployeeId { get; set; }
    public Employee EmployeeLink { get; set; }
}