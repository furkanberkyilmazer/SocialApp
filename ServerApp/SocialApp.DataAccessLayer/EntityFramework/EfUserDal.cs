using Microsoft.EntityFrameworkCore;
using SocialApp.DataAccessLayer.Abstract;
using SocialApp.DataAccessLayer.Concrete;
using SocialApp.DataAccessLayer.Repositories;
using SocialApp.EntityLayer.Concrete;

namespace SocialApp.DataAccessLayer.EntityFramework
{
    public class EfUserDal : GenericRepository<User>, IUserDal
    {
        public EfUserDal(SocialContext socialContext) : base(socialContext)
        {
        }

        public async Task<bool> AllowToFollow(int userId, int otherUserId)
        {
            
            var user = (await _socialContext.Users.Include(i => i.Followings).FirstOrDefaultAsync(i => i.Id == otherUserId)).Followings.FirstOrDefault(x => x.FollowerId == userId);          
            if (user != null)
                return false;

            return true;
        }
        public async Task<UserToUser> Follow(UserToUser entity)
        {
            await _socialContext.UserToUser.AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<int>> GetFollows(int userId, bool IsFollowings)
        {
            User user = await _socialContext.Users.Include(i => i.Followers).Include(i => i.Followings).FirstOrDefaultAsync(i => i.Id == userId);
            if (IsFollowings)
                return user.Followers.Where(i => i.FollowerId == userId).Select(i => i.UserId);

            return user.Followings.Where(i => i.UserId == userId).Select(i => i.FollowerId);

        }

        public async Task<List<User>> GetUsersWithImagesAsync(UserQueryParams userParams)
        {
            var users = _socialContext.Users.Where(x => x.Id != userParams.UserId).Include(x => x.Images).OrderByDescending(i=>i.LastActive).AsQueryable();
            if (userParams.Followers || userParams.Followings)
                users = users.Where(u => GetFollows(userParams.UserId, userParams.Followers ? false : true).Result.Contains(u.Id));

            if (!string.IsNullOrEmpty(userParams.Gender))           
                users=users.Where(g=>g.Gender.ToLower() == userParams.Gender.ToLower());
            
            if (!string.IsNullOrEmpty(userParams.Country))
                users=users.Where(c=>c.Country.ToLower() == userParams.Country.ToLower());

            if (!string.IsNullOrEmpty(userParams.City))
                users=users.Where(c=>c.City.ToLower()== userParams.City.ToLower());

            if (userParams.MaxAge!=100 || userParams.MinAge!=18)
            {
                DateTime today= DateTime.Now;
                DateTime min = today.AddYears(userParams.MinAge);
                DateTime max = today.AddYears(userParams.MaxAge);
                users = users.Where(a => a.DateOfBirth >= min && a.DateOfBirth <= max);
            }

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                if (userParams.OrderBy=="age")             
                    users = users.OrderBy(i=>i.DateOfBirth);
                else if (userParams.OrderBy == "created")
                    users = users.OrderByDescending(i => i.Created);  //en son oluşturulan en başa gelir
            }
                





            return await users.ToListAsync();
        }
     
        public async Task<User> GetUserWithImagesByIdAsync(int id)
        {
            return await _socialContext.Users.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == id);
        }

        public  void UnFollow(UserToUser entity)
        {
             _socialContext.UserToUser.Remove(entity);
        }
    }
}
