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

            var res = await _unitOfWork.AddUserAsync(us);
            
            return Ok(res); 

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
            try
            {
                await _unitOfWork.DeleteUserAsync(userId);
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpGet("UserWithPhotos")]
        public async Task<ActionResult<UserWithPhoto>> GetUserWithPhotos([FromQuery] int userId)
        {
            try
            {
                var us = await _unitOfWork.GetUserWithPhotosAsync(userId);
                return Ok(us);
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user")]
        public async Task<ActionResult<UserDto>> GetUser([FromQuery] int userId)
        {
            try
            {
                var us = await _unitOfWork.GetUserAsync(userId);
                return Ok(us);
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
