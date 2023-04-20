using FluentValidation;
using GlobalComputerSolutionsApp.ViewModels;

namespace GlobalComputerSolutionsApp.Validators
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
