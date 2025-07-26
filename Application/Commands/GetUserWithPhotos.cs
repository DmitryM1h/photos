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
    public record GetUserWithPhotosCommand(int UserId) : IRequest<User>;


    public class GetUserWithPhotosCommandHandler(ILogger<GetUserWithPhotosCommand> _logger,
                                          IUnitOfWork _unitOfWork) : IRequestHandler<GetUserWithPhotosCommand,User>
    {
        public async Task<User> Handle(GetUserWithPhotosCommand request, CancellationToken cancellationToken)
        {
            var us = await _unitOfWork.GetUserWithPhotosAsync(request.UserId);
            return us;
        }
    }
}
