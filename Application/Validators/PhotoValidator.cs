using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Dtos;
using FluentValidation;

namespace Application.Validators
{
    public class PhotoValidator : AbstractValidator<PhotoDto>
    {
        public PhotoValidator()
        {
            RuleFor(photo => photo.Picture)
                    .NotEmpty()
                    .WithMessage("Содержимое фото отсутствует");
        }
    }
}
