using SocialApp.BusinessLayer.Abstract;
using SocialApp.DataAccessLayer.Abstract;
using SocialApp.DataAccessLayer.UnitOfWork;
using SocialApp.EntityLayer.Concrete;

namespace SocialApp.BusinessLayer.Concrete
{
    public class ImageService : GenericService<Image>, IImageService
    {
        private readonly IImageDal _imageDal;
        private readonly IUnitOfWork _unitofWork;
        public ImageService(IGenericDal<Image> genericDal, IUnitOfWork unitofWork, IImageDal imageDal = null) : base(genericDal, unitofWork)
        {
            _imageDal = imageDal;
            _unitofWork = unitofWork;
        }
    }
}
