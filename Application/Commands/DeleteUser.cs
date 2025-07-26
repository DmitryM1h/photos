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
     public record DeleteUserCommand(int UserId): IRequest;


    public class DeleteUserCommandHandler(ILogger<DeleteUserCommandHandler> _logger,
                                          IUnitOfWork _unitOfWork) : IRequestHandler<DeleteUserCommand>
    {
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {

            await _unitOfWork.DeleteUserAsync(request.UserId);
            _logger.LogInformation("User has been removed. UserId = {userId}", request.UserId);
        }
    }
}
