namespace GlobalComputerSolutionsDb.Core;

public class TaskSkill  
{
    public int TaskSkillId { get; set; }
    public int Quantity { get; set; }

    //links
    public int GcsTaskId { get; set; }
    public GcsTask GcsTaskLink { get; set; }
    public int SkillId { get; set; }
    public Skill SkillLink { get; set; }
}