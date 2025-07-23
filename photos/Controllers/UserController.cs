using Application.mappers;
using Core.Context;
using Core.Dtos;
using Core.entities;
using Core.Exceptions;
using Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace photos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(PhotosContext _dbContext, 
                                IValidator<UserDto> _validator,
                                IValidator<PhotoDto> _validatorPhoto,
                                IMapper<UserDto,User> _mapUser,
                                IMapper<PhotoDto,Photo> _mapPhoto,
                                IUnitOfWork _unitOfWork
                                ) : ControllerBase
    {
        [HttpPost("user")]
        public async Task<ActionResult<UserDto>> AddUser([FromBody] UserDto user)
        {
            _validator.ValidateAndThrow(user);
            var us = _mapUser.Map(user);

            await _unitOfWork.AddUserAsync(us);
            
            return(user); 

        }
        [HttpPost("Photo")]
        public async Task<ActionResult> UploadPhoto([FromBody]PhotoDto photo,[FromQuery] int userId)
        {
            _validatorPhoto.ValidateAndThrow(photo);
            var p = _mapPhoto.Map(photo);
            try
            {
                await _unitOfWork.AddPhotoAsync(p, userId);
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete("user")]
        public async Task<ActionResult> DeleteUser([FromQuery] int userId)
        {
            var u = await _dbContext.Users.Where(t => t.Id == userId).FirstOrDefaultAsync();
            if (u is null)
                return Ok();
            _dbContext.Users.Remove(u);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("UserWithPhotos")]
        public async Task<ActionResult<UserWithPhoto>> GetUserWithPhotos([FromQuery] int userId)
        {
            var u = await _dbContext.Users.Include(t => t.Photos).Where(t => t.Id == userId).FirstOrDefaultAsync();
            if (u is null)
                return NotFound();
            return Ok(u);
        }

        [HttpGet("user")]
        public async Task<ActionResult<UserDto>> GetUser([FromQuery] int userId)
        {
            var u = await _dbContext.Users.Where(t => t.Id == userId).FirstOrDefaultAsync();
            if (u is null)
                return NotFound();
            return Ok(u);
        }

        [HttpGet("photo")]
        public async Task<ActionResult<PhotoDto>> GetPhoto([FromQuery] int photoId)
        {
            var p = await _dbContext.Photos.Where(t => t.Id == photoId).FirstOrDefaultAsync();
            if (p is null)
                return NotFound();
            return Ok(p);
        }
        
    }
}
