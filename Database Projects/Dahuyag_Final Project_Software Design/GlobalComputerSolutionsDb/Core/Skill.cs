namespace GlobalComputerSolutionsDb.Core;

public class Skill
{
    public int SkillId { get; set; }
    public string Description { get; set; }
    public float PayRate { get; set; }

    //links
    public ICollection<SkillInventory> SkillInventories { get; set; }
    public ICollection<TaskSkill> TaskSkills { get; set; }
}