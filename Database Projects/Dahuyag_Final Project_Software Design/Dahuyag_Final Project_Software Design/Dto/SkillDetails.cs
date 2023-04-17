﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalComputerSolutionsDb.Core;

namespace Dahuyag_Final_Project_Software_Design.Dto
{
    public class SkillDetails
    {
        public int SkillId { get; set; }
        public string Description { get; set; }
        public float PayRate { get; set; }
        public int Quantity { get; set; }

        public SkillDetails(Skill s)
        {
            SkillId = s.SkillId;
            Description = s.Description;
            PayRate = s.PayRate;
            Quantity = 1;
        }

        public SkillDetails(Skill s, int taskId):this(s)
        {
            if(taskId > 0)
            {
                Quantity = s.TaskSkills.Count == 0 ? 0 : s.TaskSkills
                    .Count(c => c.GcsTaskId == taskId);
            }
        }

        public SkillDetails(SkillDetails skill)
        {
            SkillId = skill.SkillId;
            Description = skill.Description;
            PayRate = skill.PayRate;
            Quantity = skill.Quantity;
        }
    }
}
