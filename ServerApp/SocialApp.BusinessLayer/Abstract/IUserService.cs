using SocialApp.EntityLayer.Concrete;

namespace SocialApp.BusinessLayer.Abstract
{
    public interface IUserService:IGenericService<User>
    {
        Task<List<User>> GetUsersWithImages(UserQueryParams userParams);
        Task<User> GetUserWithImagesById(int id);
        Task<UserToUser> Follow (UserToUser entity);

        Task UnFollow(UserToUser entity);
        Task<Boolean> AllowToFollow(int userId , int otherUserId);
    }
}
