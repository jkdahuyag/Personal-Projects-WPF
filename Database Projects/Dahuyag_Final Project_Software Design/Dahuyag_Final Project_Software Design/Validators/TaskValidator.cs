using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dahuyag_Final_Project_Software_Design.ViewModels;
using FluentValidation;

namespace Dahuyag_Final_Project_Software_Design.Validators
{
    public class TaskValidator : AbstractValidator<AddProjectScheduleViewModel> 
    {
        public TaskValidator()
        {
            RuleFor(c => c.SkillList).NotEmpty();
            RuleFor(c => c.TaskDescriptionInput).NotEmpty();
            RuleFor(c=>c.EstimatedDateStartInput).NotEqual(DateTime.MinValue);
            RuleFor(c=>c.EstimatedDateEndInput).NotEqual(DateTime.MinValue).GreaterThanOrEqualTo(c=>c.EstimatedDateStartInput);
        }
    }
}
