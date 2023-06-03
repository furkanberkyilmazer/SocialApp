using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialApp.BusinessLayer.Abstract;
using SocialApp.EntityLayer.Concrete;
using SocialApp.EntityLayer.DTO;
using System.Text.Json.Serialization;

namespace SocialApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUsers()
        {
            //model içinde model getirmek için progrm.cs e eklenir
            //builder.Services.AddControllers().AddJsonOptions(x =>
            //    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            var users = await _userService.GetUsersWithImages();
            var usersDtos = _mapper.Map<List<UserForListDTO>>(users.ToList());

            //foreach (var user in users)
            //{            
            //    usersDtos.Where(x => x.Id == user.Id).FirstOrDefault().Image = _mapper.Map<ImagesForDetails>(user.Images.FirstOrDefault(x => x.IsProfile));

            //} 

            //bunun yerine bu şekillde mapper ekledik.

            //CreateMap<User, UserForListDTO>().ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault(i => i.IsProfile))).ReverseMap();  


            return Ok(usersDtos);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUsersWithImagesById(id);
            var userDto = _mapper.Map<UserForDetailsDTO>(user);
           
            return Ok(userDto);
        }
    }
}
