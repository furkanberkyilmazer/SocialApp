using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialApp.BusinessLayer.Abstract;
using SocialApp.EntityLayer.Concrete;

namespace SocialApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public ImagesController(IImageService imageService, IMapper mapper)
        {
            _imageService = imageService;
            _mapper = mapper;
        }


        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddImageForSeed(Image model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);



            var image = await _imageService.AddAsync(model);
 
            return Ok(image);
         

        }
    }
}
