using Application.Commands;
using Application.mappers;
using Core.Context;
using Core.Dtos;
using Core.entities;
using Core.Exceptions;
using Core.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace photos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(PhotosContext _dbContext, 
                                IValidator<PhotoDto> _validatorPhoto,
                                IMapper<UserDto,User> _mapUser,
                                IMapper<PhotoDto,Photo> _mapPhoto,
                                IUnitOfWork _unitOfWork,
                                ILogger<UserController> logger,
                                IMediator _mediator
                                ) : ControllerBase
    {
        [HttpPost("Add")]
        public async Task<ActionResult<UserDto>> AddUser([FromBody] UserDto user)
        {
            var res = await _mediator.Send(new CreateUserCommand(user));            
            return Ok(res); 

        }
        [HttpPost("Photo")]
        public async Task<ActionResult<Photo>> UploadPhoto([FromBody]PhotoDto photo,[FromQuery] int userId)
        {
            var res = await _mediator.Send(new CreatePhotoCommand(photo,userId));
            return Ok(res);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteUser([FromQuery] int userId)
        {

            await _mediator.Send(new DeleteUserCommand(userId));

            return Ok();
        }

        [HttpGet("GetUserWithPhotos")]
        public async Task<ActionResult<UserWithPhoto>> GetUserWithPhotos([FromQuery] int userId)
        {

            var us = await _mediator.Send(new GetUserWithPhotosCommand(userId));
            return Ok(us);

        }

        [HttpGet("Get")]
        public async Task<ActionResult<UserDto>> GetUser([FromQuery] int userId)
        {
            var us = await _mediator.Send(new GetUserCommand(userId));  
            return Ok(us);
        }
        
    }
}
