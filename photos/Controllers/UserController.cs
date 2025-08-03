using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Commands;
using Auth;
using Core.Dtos;
using Core.entities;
using Core.Filters;
using Mailer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace photos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpPost("Add")]
        [TypeFilter<LoggingFilter>]
        [Authorize]
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

        
        [HttpPost("login")]
        public string Login([FromBody] LogDto logDto)
        {
            var claims = new List<Claim> {new Claim(ClaimTypes.Name, "dmitry"),new  Claim(ClaimTypes.Role,"user") };
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
            //return Ok();
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
