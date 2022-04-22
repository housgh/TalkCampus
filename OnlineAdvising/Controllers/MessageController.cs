using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineAdvising.Attributes;
using OnlineAdvising.Core.Interfaces;
using OnlineAdvising.Core.Models;

namespace OnlineAdvising.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
                
        [HttpGet("{chatId}")]
        public async Task<IActionResult> GetMessages(int chatId)
        {
            var messages = await _messageService.GetAllAsync(chatId);
            return Ok(messages.Value);
        }

        [HttpGet("{chatId}/{lastMessageId}")]
        public async Task<IActionResult> GetNewMessages(int chatId, int lastMessageId)
        {
            var messages = await _messageService.GetNewMessagesAsync(chatId, lastMessageId);
            return Ok(messages.Value);
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage(MessageModel model)
        {
            HttpContext.Request.Headers.TryGetValue("userId", out var userIdValue);
            var userId = int.Parse(userIdValue);
            
            var result = await _messageService.AddAsync(model);
            if (!result.IsSucceeded) return BadRequest();
            var newMessage = await _messageService.GetAsync(result.Value);
            return Ok(newMessage);
        }
    }
}