using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dahuyag_Final_Project_Software_Design.ViewModels;
using FluentValidation;

namespace Dahuyag_Final_Project_Software_Design.Validators
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
