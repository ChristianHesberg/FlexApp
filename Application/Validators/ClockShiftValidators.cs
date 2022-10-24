using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class ClockInValidator : AbstractValidator<ClockInDTO>
{
    public ClockInValidator()
    {
        RuleFor(s => s.StartTime)
            .NotNull();
        RuleFor(s => s.EmployeeId)
            .GreaterThan(0);
    }
}

public class ClockOutValidator : AbstractValidator<ClockOutDTO>
{
    public ClockOutValidator()
    {
        RuleFor(s => s.EndTime)
            .NotNull();
        RuleFor(s => s.EmployeeId)
            .GreaterThan(0);
    }
}