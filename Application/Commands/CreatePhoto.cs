using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Dtos;
using Core.entities;
using Core.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands
{
    public record CreatePhotoCommand(PhotoDto Photo, int userId) : IRequest<Photo>;


    public class CreatePhotoCommandHandler(IUnitOfWork _unitOfWork,
                                          ILogger<CreatePhotoCommand> _logger,
                                          IValidator<PhotoDto> _validator,
                                          IMapper<PhotoDto, Photo> _mapper) : IRequestHandler<CreatePhotoCommand, Photo>
    {
        public async Task<Photo> Handle(CreatePhotoCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request.Photo);
            var photo = _mapper.Map(request.Photo);

            var res = await _unitOfWork.AddPhotoAsync(photo,request.userId);

            return res;

        }
    }
}
