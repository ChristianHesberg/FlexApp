using Application.DTOs;
using Domain.Models;
using FluentValidation;

namespace Application.Validators;

public class AddEmployeeValidator : AbstractValidator<AddEmployeeDTO>
{
    public AddEmployeeValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(30);
    }
}

public class EditEmployeeValidator : AbstractValidator<EditEmployeeDTO>
{
    public EditEmployeeValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(30);
        RuleFor(e => e.Id)
            .GreaterThan(0);
    }
}