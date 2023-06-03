using AutoMapper;
using SocialApp.API.Helpers;
using SocialApp.EntityLayer.Concrete;
using SocialApp.EntityLayer.DTO;

namespace SocialApp.API.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User, UserForRegisterDTO>();
            CreateMap<User, UserForRegisterDTO>().ReverseMap();
            CreateMap<User, UserForListDTO>().ForMember(dest=>dest.Image,opt=>opt.MapFrom(src=>src.Images.FirstOrDefault(i=>i.IsProfile))).ForMember(dest=>dest.Age,opt=>opt.MapFrom(src=>src.DateOfBirth.Value.CalculateAge())).ReverseMap(); //ForMember ı  UserforlistDto da bir image var ama User da image listi var dönüşümde tekm bir image i içine basmak için kullandım.
          
            CreateMap<User, UserForDetailsDTO>().ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.Value.CalculateAge())).ReverseMap();
            CreateMap<Image,ImagesForDetails>().ReverseMap();


        }
    }
}
