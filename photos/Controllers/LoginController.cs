using Auth;
using Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core.Context;
using Core.entities;
using Core.Interfaces;using Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace photos.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost("login")]
        public string Login([FromBody] LogDto logDto)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, logDto.Login), new Claim(ClaimTypes.Role, nameof(Role.User))};
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            HttpContext.Response.Cookies.Append("Mycookies", token);
            
            return Ok(token);
        }
    }
}
