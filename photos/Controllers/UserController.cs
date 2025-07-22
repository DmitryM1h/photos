using Core.Context;
using Core.entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace photos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(PhotosContext _dbContext, IValidator<UserDto> _validator) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<UserDto>> AddUser([FromBody] UserDto user)
        {
            _validator.ValidateAndThrow(user);
            var us = new User
            {
                Age = user.Age,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Role = user.Role,
                Password = user.Password
            };
            await _dbContext.Users.AddAsync(us);
            await _dbContext.SaveChangesAsync();
            return(user); 

        }

    }
}
