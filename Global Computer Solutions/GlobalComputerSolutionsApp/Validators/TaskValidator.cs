using System;
using FluentValidation;
using GlobalComputerSolutionsApp.ViewModels;

namespace GlobalComputerSolutionsApp.Validators
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
