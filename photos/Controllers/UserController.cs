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
                                IUnitOfWork _unitOfWork,
                                ILogger<UserController> logger
                                ) : ControllerBase
    {
        [HttpPost("user")]
        public async Task<ActionResult<UserDto>> AddUser([FromBody] UserDto user)
        {
            _validator.ValidateAndThrow(user);
            var us = _mapUser.Map(user);
            var res = await _unitOfWork.AddUserAsync(us);
            logger.LogInformation("Пользователь был добавлен. Id = {userId}",res.Id);
            return Ok(res); 

        }
        [HttpPost("Photo")]
        public async Task<ActionResult> UploadPhoto([FromBody]PhotoDto photo,[FromQuery] int userId)
        {
            _validatorPhoto.ValidateAndThrow(photo);
            var p = _mapPhoto.Map(photo);
            await _unitOfWork.AddPhotoAsync(p, userId);
            return Ok();
        }

        [HttpDelete("user")]
        public async Task<ActionResult> DeleteUser([FromQuery] int userId)
        {

            await _unitOfWork.DeleteUserAsync(userId);

            return Ok();
        }

        [HttpGet("UserWithPhotos")]
        public async Task<ActionResult<UserWithPhoto>> GetUserWithPhotos([FromQuery] int userId)
        {

            var us = await _unitOfWork.GetUserWithPhotosAsync(userId);
            return Ok(us);

        }

        [HttpGet("user")]
        public async Task<ActionResult<UserDto>> GetUser([FromQuery] int userId)
        {
            var us = await _unitOfWork.GetUserAsync(userId);
            return Ok(us);
        }
        
    }
}
