using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dahuyag_Final_Project_Software_Design.ViewModels;
using FluentValidation;
using FluentValidation.Validators;

namespace Dahuyag_Final_Project_Software_Design.Validators
{
    public class ProjectScheduleListValidator : AbstractValidator<AddProjectScheduleViewModel>
    {
        public ProjectScheduleListValidator()
        {
            RuleForEach(c => c.ProjectSchedules)
                .Must(d => d.Tasks.Count != 0)
                .WithMessage("Edit empty project schedules");
        }
    }
}
