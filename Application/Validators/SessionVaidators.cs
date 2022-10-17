using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class AddSessionValidator : AbstractValidator<AddSessionDTO>
{
    public AddSessionValidator()
    {
        RuleFor(s => s.EndTime)
            .NotNull();
        RuleFor(s => s.StartTime)
            .NotNull();
    }
}

public class EditSessionValidator : AbstractValidator<EditSessionDTO>
{
    public EditSessionValidator()
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