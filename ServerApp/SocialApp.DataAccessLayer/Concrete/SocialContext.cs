using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SocialApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DataAccessLayer.Concrete
{
    public class SocialContext : IdentityDbContext<User,Role,int>
    {
        //dockerda ınstance ı oluştur ama dbeaverla bağlanmadan migration yap daha sonra database ismiyle bağlan.

        public SocialContext(DbContextOptions<SocialContext> options) : base(options)
        {

        }    
        public DbSet<Image> Images { get; set; }
    }
}
