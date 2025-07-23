using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Context;
using Core.entities;
using Core.Exceptions;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.UnitsOfWork
{
    public class UnitOfWork(PhotosContext _dbContext) : IUnitOfWork
    {
        public async Task<User?> AddUserAsync(User user)
        {
            if (user is null)
                return null;
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;

        }

        public async Task<Photo> AddPhotoAsync(Photo photo, int userId)
        {
            var u = await _dbContext.Users.Where(t => t.Id == userId).FirstOrDefaultAsync();
            if (u is null)
                throw new UserNotFoundException(photo.Id);
            await _dbContext.Photos.AddAsync(photo);
            await _dbContext.SaveChangesAsync();
            return photo;
        }
    }
}
