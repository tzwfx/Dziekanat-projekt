using CoreApp.Dto;
using FluentValidation;

namespace CoreApp.Validators;

public class GradeDtoValidator : AbstractValidator<GradeDto>
{
    public GradeDtoValidator()
    {
        RuleFor(x => x.CourseId)
            .NotEmpty().WithMessage("Identyfikator kursu jest wymagany.");

        RuleFor(x => x.LecturerId)
            .NotEmpty().WithMessage("Identyfikator wykładowcy jest wymagany.");

        RuleFor(x => x.AcademicYearId)
            .NotEmpty().WithMessage("Identyfikator roku akademickiego jest wymagany.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Data jest wymagana.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Data nie może być z przyszłości.");

        RuleFor(x => x.GradeValue)
            .IsInEnum().WithMessage("Niepoprawna wartość oceny.");
    }
}