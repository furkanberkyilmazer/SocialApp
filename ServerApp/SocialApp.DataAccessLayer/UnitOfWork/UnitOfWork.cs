using SocialApp.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialContext _socialContext;

        public UnitOfWork(SocialContext socialContext)
        {
            _socialContext = socialContext;
        }

        public void Commit()
        {
            _socialContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _socialContext.SaveChangesAsync();
        }
    }
}
