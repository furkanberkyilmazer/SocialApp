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
    public class EfMessageDal : GenericRepository<Message>, IMessageDal
    {
        public EfMessageDal(SocialContext socialContext) : base(socialContext)
        {
        }
    }
}
