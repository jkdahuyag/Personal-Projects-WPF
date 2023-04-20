using GlobalComputerSolutionsDb.Core;

namespace GlobalComputerSolutionsApp.Dto
{
    public class SkillName
    {
        public int SkillId { get; set; }
        public string Description { get; set; }

        public SkillName()
        {
            
        }

        public SkillName(Skill s):this()
        {
            SkillId = s.SkillId;
            Description = s.Description;
        }
    }
}
