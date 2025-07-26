using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.entities;
using Core.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands
{
    public record CreateUserCommand(UserDto User) : IRequest<User>;


    public class CreateUserCommandHandler(IUnitOfWork _unitOfWork, 
                                          ILogger<CreateUserCommandHandler> _logger,
                                          IValidator<UserDto> _validator,
                                          IMapper<UserDto,User> _mapper) : IRequestHandler<CreateUserCommand,User>
    {
        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request.User);
            var user = _mapper.Map(request.User);

            var res = await _unitOfWork.AddUserAsync(user);

            return res;

        }
    }
}
