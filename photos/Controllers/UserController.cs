using Application.Commands;
using Core.Dtos;
using Core.entities;
using Core.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace photos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter<LoggingFilter>]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpPost("Add")]
        public async Task<ActionResult<UserDto>> AddUser([FromBody] UserDto user)
        {
            var res = await mediator.Send(new CreateUserCommand(user));            
            return Ok(res); 

        }
        [HttpPost("Photo")]
        public async Task<ActionResult<Photo>> UploadPhoto([FromBody]PhotoDto photo,[FromQuery] int userId)
        {
            var res = await mediator.Send(new CreatePhotoCommand(photo,userId));
            return Ok(res);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteUser([FromQuery] int userId)
        {
            await mediator.Send(new DeleteUserCommand(userId));
            return Ok();
        }

        [HttpGet("GetUserWithPhotos")]
        public async Task<ActionResult<UserWithPhoto>> GetUserWithPhotos([FromQuery] int userId)
        {
            var us = await mediator.Send(new GetUserWithPhotosCommand(userId));
            return Ok(us);

        }


        [ResponseCache(Duration = 300)]
        [HttpGet("Get")]
        public async Task<ActionResult<UserDto>> GetUser([FromQuery] int userId)
        {
            var us = await mediator.Send(new GetUserCommand(userId));  
            return Ok(us);
        }
        
    }
}
