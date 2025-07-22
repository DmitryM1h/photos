using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Context;
using Core.entities;
using Core.Interfaces;

namespace Database
{
    public class AddUserUnitOfWork(PhotosContext _dbContext) : IUnitOfWork
    {
        public async Task<User?> AddUserAsync(User user)
        {
            if (user is null)
                return null;
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;

        }
    }
}
