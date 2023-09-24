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
    public class EfImageDal : GenericRepository<Image>, IImageDal
    {
        public EfImageDal(SocialContext socialContext) : base(socialContext)
        {
        }
    }
}
