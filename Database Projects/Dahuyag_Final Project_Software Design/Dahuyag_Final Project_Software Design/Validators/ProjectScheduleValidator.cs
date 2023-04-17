using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dahuyag_Final_Project_Software_Design.ViewModels;
using FluentValidation;

namespace Dahuyag_Final_Project_Software_Design.Validators
{
    public class ProjectScheduleValidator : AbstractValidator<AddProjectScheduleViewModel>
    {
        public ProjectScheduleValidator()
        {
            RuleFor(c => c.TaskList).NotEmpty();
            RuleFor(c => c.ProjectScheduleDescriptionInput).NotEmpty();
            RuleForEach(c => c.TaskList)
                .Must(d => d.SkillsRequired.Count != 0)
                .WithMessage("Edit the empty tasks");
        }
    }
}
