using SocialApp.BusinessLayer.Abstract;
using SocialApp.DataAccessLayer.Abstract;
using SocialApp.DataAccessLayer.UnitOfWork;
using SocialApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<User>> GetUsersWithImages()
        {
            var users = await _userDal.GetUsersWithImagesAsync();

            return users;
        }

        public async Task<User> GetUsersWithImagesById(int id)
        {
            var user = await _userDal.GetUserWithImagesByIdAsync(id);

            return user;
        }
    }
}
