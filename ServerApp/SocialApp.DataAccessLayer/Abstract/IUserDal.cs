using SocialApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DataAccessLayer.Abstract
{
    public interface IUserDal:IGenericDal<User>
    {
        Task<List<User>> GetUsersWithImagesAsync(UserQueryParams userParams);
        Task<User> GetUserWithImagesByIdAsync(int id);

        Task<UserToUser> Follow(UserToUser entity);

        Task<IEnumerable<int>> GetFollows(int userId, bool IsFollower);

        Task<Boolean> AllowToFollow(int userId, int otherUserId);

        void UnFollow(UserToUser entity);


    }
}
