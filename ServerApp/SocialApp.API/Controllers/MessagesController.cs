using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using SocialApp.BusinessLayer.Abstract;
using SocialApp.BusinessLayer.Concrete;
using SocialApp.EntityLayer.Concrete;
using SocialApp.EntityLayer.DTO;
using System.Security.Claims;

namespace SocialApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/{userId}")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessagesController(IMessageService messageService, IMapper mapper, IUserService userService)
        {
            _messageService = messageService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateMessage(int userId,MessageForCreateDTO messageforCreatedDTO)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

           messageforCreatedDTO.SenderId = userId;

           User recipient =await _userService.GetByIdAsync(messageforCreatedDTO.SenderId);

            if (recipient == null)
                return BadRequest("Mesaj göndermek istediğiniz kullanıcı yok.");

            Message message=_mapper.Map<Message>(messageforCreatedDTO);
            
            return Ok(_mapper.Map<MessageForCreateDTO>(await _messageService.AddAsync(message)));
        }
    }
}
