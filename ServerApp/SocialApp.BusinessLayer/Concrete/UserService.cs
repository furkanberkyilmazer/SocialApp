using SocialApp.BusinessLayer.Abstract;
using SocialApp.DataAccessLayer.Abstract;
using SocialApp.DataAccessLayer.UnitOfWork;
using SocialApp.EntityLayer.Concrete;

namespace SocialApp.BusinessLayer.Concrete
{
    public class UserService :GenericService<User>, IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IUnitOfWork _unitofWork;
      
        public UserService(IGenericDal<User> genericDal, IUnitOfWork unitofWork, IUserDal UserDal) : base(genericDal, unitofWork)
        {
            _userDal = UserDal;
            _unitofWork = unitofWork;
        }

        public async Task<bool> AllowToFollow(int userId, int otherUserId)
        {    
            return await _userDal.AllowToFollow(userId, otherUserId);
        }

        public async Task<UserToUser> Follow(UserToUser entity)
        {
            await _userDal.Follow(entity);
            await _unitofWork.CommitAsync();
            return entity;
        }

        public async Task<List<User>> GetUsersWithImages(UserQueryParams userParams)
        {
      
            return await _userDal.GetUsersWithImagesAsync(userParams);
        }

        public async Task<User> GetUserWithImagesById(int id)
        {
           

            return await _userDal.GetUserWithImagesByIdAsync(id);
        }

        public async Task UnFollow(UserToUser entity)
        {
             _userDal.UnFollow(entity);
            await _unitofWork.CommitAsync();
    
        }
    }
}
