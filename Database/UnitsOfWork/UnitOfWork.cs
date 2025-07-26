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
        public async Task<User> AddUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<Photo> AddPhotoAsync(Photo photo, int userId)
        {
            await using var tran = await _dbContext.Database.BeginTransactionAsync();

            var u = await _dbContext.Users.Where(t => t.Id == userId).FirstOrDefaultAsync();
            if (u is null)
            {
                await _dbContext.Database.RollbackTransactionAsync();
                throw new UserNotFoundException(userId);
            }
            photo.PublisherId = userId;
            await _dbContext.Photos.AddAsync(photo);
            await _dbContext.SaveChangesAsync();
            await _dbContext.Database.CommitTransactionAsync();

            return photo;

        }

        public async Task<User> DeleteUserAsync(int userId)
        {
            await using var tran = await _dbContext.Database.BeginTransactionAsync();

           var u = await _dbContext.Users
                        .FromSqlInterpolated($"select * from Users with (UPDLOCK,ROWLOCK) where Id ={userId}")
                        .FirstOrDefaultAsync();
            //var u = await _dbContext.Users.Where(t => t.Id == userId).FirstOrDefaultAsync();
            if (u is null)
            {
                await _dbContext.Database.RollbackTransactionAsync();
                throw new UserNotFoundException(userId);
            }
            _dbContext.Users.Remove(u);
            await _dbContext.SaveChangesAsync();

            await _dbContext.Database.CommitTransactionAsync();

            return u;
        }

        public async Task<User> GetUserWithPhotosAsync(int userId)
        {
            var u = await _dbContext.Users.Include(t => t.Photos).Where(t => t.Id == userId).FirstOrDefaultAsync();
            if (u is null)
                throw new UserNotFoundException(userId);
            return u;
        }

        public async Task<User> GetUserAsync(int userId)
        {
            var u = await _dbContext.Users.Where(t => t.Id == userId).FirstOrDefaultAsync();
            if (u is null)
                throw new UserNotFoundException(userId);
            return u;
        }
    }
}
