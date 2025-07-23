using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.entities;
using FluentValidation;

namespace Application.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(user => user.Name)
                .NotEmpty()
                .WithMessage("Не указано имя");
            RuleFor(user => user.Password)
                .NotEmpty()
                .WithMessage("Не указан пароль");
            RuleFor(user => user.Email)
                .NotEmpty()
                .WithMessage("Не указан email");
            RuleFor(user => user.Age)
                .NotNull()
                .WithMessage("Не указан возраст");
        }
    }
}
