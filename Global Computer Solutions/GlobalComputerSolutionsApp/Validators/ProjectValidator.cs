using System;
using FluentValidation;
using GlobalComputerSolutionsApp.ViewModels;

namespace GlobalComputerSolutionsApp.Validators
{
    public class ProjectValidator : AbstractValidator<AddProjectViewModel>
    {
        public ProjectValidator()
        {
            RuleFor(c => c.ProjectEstimatedBudgetInput)
                .GreaterThan(0);
            RuleFor(c => c.EstimatedDateStartInput)
                .NotNull();
            RuleFor(c => c.EstimatedDateEndInput)
                .NotNull()
                .GreaterThan(c=>c.EstimatedDateStartInput.Date);
            RuleFor(c => c.DateEndInput)
                .NotEmpty()
                .GreaterThan(c=>c.DateStartInput)
                .When(c => c.DateStartInput != DateTime.MinValue);
            RuleFor(c => c.ProjectDescriptionInput)
                .NotEmpty();
            RuleFor(c => c.DateSignedInput)
                .NotNull()
                .LessThan(c=>c.EstimatedDateStartInput.Date);
            RuleFor(c => c.SelectedEmployee)
                .NotNull();
            RuleFor(c => c.SelectedCustomer)
                .NotNull();
        }
    }
}
