using Microsoft.EntityFrameworkCore;
using SocialApp.DataAccessLayer.Abstract;
using SocialApp.DataAccessLayer.Concrete;
using SocialApp.DataAccessLayer.Repositories;
using SocialApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DataAccessLayer.EntityFramework
{
    public class EfUserDal : GenericRepository<User>, IUserDal
    {
        public EfUserDal(SocialContext socialContext) : base(socialContext)
        {
        }

        public async Task<List<User>> GetUsersWithImagesAsync()
        {
            return await _socialContext.Users.Include(x=> x.Images).ToListAsync();
        }

        public async Task<User> GetUserWithImagesByIdAsync(int id)
        {
            return await _socialContext.Users.Include(x => x.Images).Where(x=>x.Id==id).FirstOrDefaultAsync();
        }
    }
}
