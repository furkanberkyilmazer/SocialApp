using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialApp.EntityLayer.Concrete;
using SocialApp.EntityLayer.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager; //bu identitynin kendi manager ı bizim mimariyle ilgili değil.
       
        private readonly IMapper _mapper;

        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<User> userManager, IMapper mapper,SignInManager<User> signInManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(UserForRegisterDTO model)
        {
            //        var user = new User
            //        {
            //            UserName = model.UserName,
            //            Name = model.Name,
            //            Email = model.Email,
            //            Gender = model.Gender,

            //}; // eski mapleme

            if (!ModelState.IsValid)            
                return BadRequest(ModelState);
            
           
            var newUser = _mapper.Map<User>(model);
            newUser.Created = DateTime.Now; //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); postgresql de datetime kullanmak için startup a bunu ekle
            newUser.LastActive = DateTime.Now;
            var user = await _userManager.CreateAsync(newUser, model.Password);


            if (user.Succeeded)
            {
                var userDto = _mapper.Map<UserForRegisterDTO>(newUser);
                return Ok(userDto);
            }
            return BadRequest(user.Errors);

        }
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> MultiRegisterForSeed(List<User> models )
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           
            foreach (var model in models)
            {
                var newUser = _mapper.Map<User>(model);
                model.Created = DateTime.Now;
                model.LastActive = DateTime.Now;
                model.LastActive = DateTime.Now;
                var user = await _userManager.CreateAsync(newUser, model.Password);
                if (!user.Succeeded)              
                    return BadRequest(user.Errors);
                
            }
            
                var userDtos = _mapper.Map<List<UserForRegisterDTO>>(models.ToList());
                return Ok(userDtos);
            
        

        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserForLoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var user=await _userManager.FindByNameAsync(model.UserName);

            if (user==null)
            {
                return BadRequest(new
                {
                   message = "username is incorrect"
                });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user,model.Password,false);

            if (result.Succeeded)
            {
                //login
                return Ok(new
                {
                    token= GenerateJwtToken(user)
                });
            }
            return Unauthorized();

        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes( _configuration.GetSection("AppSettings:Secret").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName),
                }),
                //Issuer="furkanberkyilmazer.com",//tokenın nerden geldiği bilgisi
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials= new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256)
            };

            var token=tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);


        }
    }
}
