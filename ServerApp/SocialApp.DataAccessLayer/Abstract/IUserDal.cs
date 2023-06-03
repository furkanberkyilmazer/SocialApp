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
        Task<List<User>> GetUsersWithImagesAsync();
        Task<User> GetUserWithImagesByIdAsync(int id);
    }
}
