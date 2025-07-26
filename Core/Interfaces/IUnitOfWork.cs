using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.entities;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task<Photo> AddPhotoAsync(Photo photo, int userId);
        Task<User> AddUserAsync(User user);
        Task<User> DeleteUserAsync(int userId);
        Task<User> GetUserWithPhotosAsync(int userId);
        Task<User> GetUserAsync(int userId);




    }
}
