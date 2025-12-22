using BarberSpa.Application.DTOs.Auth;
using FluentValidation;

namespace BarberSpa.Application.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .EmailAddress().WithMessage("El formato del email no es válido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("El nombre es obligatorio.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("El apellido es obligatorio.");
        }
    }
}