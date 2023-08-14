using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Domain.DTOs;

namespace TaskManager.Domain.Validations
{
    public class UserTaskRequestValidator : AbstractValidator<UserTaskRequestDTO>
    {
        public UserTaskRequestValidator()
        {
            this.RuleFor(task => task.DueDate)
                .NotEmpty()
                .WithMessage("Due Date is required.")
                .When(task => task.StartTime.IsNullOrEmpty() || task.EndTime.IsNullOrEmpty());

            this.RuleFor(task => task.StartTime)
                .NotEmpty()
                .WithMessage("Start Time is required when End Time is specified.")
                .When(task => task.EndTime.IsNullOrEmpty() && task.DueDate.HasValue);

            this.RuleFor(task => task.EndTime)
                .NotEmpty()
                .WithMessage("End Time is required when Start Time is specified.")
                .When(task => task.StartTime.IsNullOrEmpty() && task.DueDate.HasValue);
        }

    }
}
