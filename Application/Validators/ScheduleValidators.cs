using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class AddScheduleValidator : AbstractValidator<AddScheduleDTO>
{
    public AddScheduleValidator()
    {
        RuleFor(s => s.EndTime)
            .NotNull();
        RuleFor(s => s.StartTime)
            .NotNull();
    }
}

public class EditScheduleValidator : AbstractValidator<EditScheduleDTO>
{
    public EditScheduleValidator()
    {
        RuleFor(s => s.Id)
            .GreaterThan(0)
            .NotNull();
        RuleFor(s => s.EndTime)
            .NotNull();
        RuleFor(s => s.StartTime)
            .NotNull();
    }
}