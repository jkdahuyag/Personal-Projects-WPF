using FluentValidation;
using GlobalComputerSolutionsApp.ViewModels;

namespace GlobalComputerSolutionsApp.Validators
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
