using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.entities;
using Core.Interfaces;

namespace Application.mappers
{
    public class UserMapper : IMapper<UserDto,User>
    {
        public User Map(UserDto user)
        {
            return new User()
            {
                Age = user.Age,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                Password = user.Password,
                Role = user.Role
            };
        }
    }
}
