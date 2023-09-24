using SocialApp.BusinessLayer.Abstract;
using SocialApp.DataAccessLayer.Abstract;
using SocialApp.DataAccessLayer.UnitOfWork;
using SocialApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.BusinessLayer.Concrete
{
    public class MessageService : GenericService<Message>, IMessageService
    {
        private readonly IMessageDal _messageDal;
        private readonly IUnitOfWork _unitofWork;

        public MessageService(IGenericDal<Message> genericDal, IUnitOfWork unitofWork, IMessageDal messageDal = null) : base(genericDal, unitofWork)
        {
            _messageDal = messageDal;
            _unitofWork = unitofWork;
        }
    }
}
