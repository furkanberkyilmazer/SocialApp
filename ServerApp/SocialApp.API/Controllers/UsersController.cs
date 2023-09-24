using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialApp.API.Helpers;
using SocialApp.BusinessLayer.Abstract;
using SocialApp.EntityLayer.Concrete;
using SocialApp.EntityLayer.DTO;
using System.Security.Claims;

namespace SocialApp.API.Controllers
{
    [Authorize] 
    [Route("api/[controller]")] //direkt route dan userId
    [ApiController]
    [ServiceFilter(typeof(LastActiveActionFilter))]
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
        public async Task<IActionResult> GetUsers([FromQuery] UserQueryParams userParams)
        {
            //model içinde model getirmek için progrm.cs e eklenir
            //builder.Services.AddControllers().AddJsonOptions(x =>
            //    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            userParams.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var users = await _userService.GetUsersWithImages(userParams);
            var usersDtos = _mapper.Map<List<UserForListDTO>>(users.ToList());

            //foreach (var user in users)
            //{            
            //    usersDtos.Where(x => x.Id == user.Id).FirstOrDefault().Image = _mapper.Map<ImagesForDetails>(user.Images.FirstOrDefault(x => x.IsProfile));

            //} 

            //bunun yerine bu şekillde mapper ekledik.

            //CreateMap<User, UserForListDTO>().ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault(i => i.IsProfile))).ReverseMap();  


            return Ok(usersDtos);
        }
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDTO model)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("not valid request");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User user = await _userService.GetByIdAsync(id);
            if (user == null)
                return BadRequest("Olmayan kullanıcı.");
           

                user = new () { Country = model.Country, City = model.City, Hobbies = model.Hobbies, Introduction = model.Introduction };



            _ = _userService.UpdateAsync(user);
            return Ok(model);

            

        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            User user = await _userService.GetUserWithImagesById(id);
            Boolean allowToFollow = await _userService.AllowToFollow(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), id);
            var userDto = _mapper.Map<UserForDetailsDTO>(user);
           // userDto.ProfileImageUrl = user.Images[0].Name;  bunun yerine automapperda yaptım 
           
            userDto.FollowTittle = allowToFollow ? "Takip Et" : "Takibi Bırak";
            return Ok(userDto);
        }

        //users/1/FollowToUser/3
        [HttpPost("{followerUserId}/[action]/{userId}")]
        public async Task<IActionResult> FollowToUser(int followerUserId, int userId)
        {
            if (followerUserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            if (followerUserId == userId)
                return BadRequest("Kendizi takip edemezsiniz");

            if (await _userService.GetByIdAsync(userId) == null)
                return NotFound();

            UserToUser follow = new UserToUser()
            {
                UserId = userId,
                FollowerId = followerUserId
            };

            UserToUser newFollow = await _userService.Follow(follow);
            if (newFollow != null)
               return Ok(newFollow);
            

            return BadRequest();

        }
        //users/1/UnfollowToUser/3
        [HttpDelete("{unfollowerUserId}/[action]/{userId}")]
        public async Task<IActionResult> UnfollowToUser(int unfollowerUserId, int userId)
        {
            if (unfollowerUserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            if (unfollowerUserId == userId)
                return BadRequest("Kendizi takipten çıkamazsınız.");

            if (await _userService.GetByIdAsync(userId) == null)
                return NotFound();

            UserToUser unfollow = new UserToUser()
            {
               UserId = userId,
                FollowerId = unfollowerUserId
            };

            await _userService.UnFollow(unfollow);
            return Ok();

        }
    }
}
