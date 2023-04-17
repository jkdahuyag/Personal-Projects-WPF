using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalComputerSolutionsDb.Core;

namespace Dahuyag_Final_Project_Software_Design.Dto
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
