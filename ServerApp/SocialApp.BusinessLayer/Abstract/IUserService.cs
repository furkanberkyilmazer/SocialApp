using SocialApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.BusinessLayer.Abstract
{
    public interface IUserService:IGenericService<User>
    {
        Task<List<User>> GetUsersWithImages();
        Task<User> GetUsersWithImagesById(int id);
    }
}
