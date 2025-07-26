using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.entities;
using Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands
{
    public record GetUserCommand(int UserId) : IRequest<User>;


    public class GetUserCommandHandler(ILogger<GetUserCommandHandler> _logger,
                                          IUnitOfWork _unitOfWork) : IRequestHandler<GetUserCommand, User>
    {
        public async Task<User> Handle(GetUserCommand request, CancellationToken cancellationToken)
        {
            var us = await _unitOfWork.GetUserAsync(request.UserId);
            return us;
        }
    }
}
