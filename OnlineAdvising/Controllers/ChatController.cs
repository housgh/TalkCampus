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
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;
        private readonly IReportingService _reportingService;
        private readonly IRatingService _ratingService;

        public ChatController(
            IChatService chatService,
            IReportingService reportingService,
            IRatingService ratingService)
        {
            _chatService = chatService;
            _reportingService = reportingService;
            _ratingService = ratingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetChats()
        {
            HttpContext.Request.Headers.TryGetValue("userId", out var userIdValue);
            var userId = int.Parse(userIdValue);
            var chats = await _chatService.GetAllAsync(userId);
            return Ok(chats.Value);
        }

        [HttpGet("{chatId}")]
        public async Task<IActionResult> GetChat(int chatId)
        {
            var chat = await _chatService.GetAsync(chatId);
            return Ok(chat.Value);
        }

        [HttpPost("Report")]
        public async Task<IActionResult> ReportChat(ReportModel model)
        {
            HttpContext.Request.Headers.TryGetValue("userId", out var userIdValue);
            var userId = int.Parse(userIdValue);
            var result = await _reportingService.AddAsync(model, userId);
            if (!result.IsSucceeded) return BadRequest();
            return Ok($"User {model.ReportedId} Reported Successfully");
        }
        
        [HttpPost("Rate")]
        public async Task<IActionResult> RateChat(RateModel model)
        {
            HttpContext.Request.Headers.TryGetValue("userId", out var userIdValue);
            var userId = int.Parse(userIdValue);
            var result = await _ratingService.AddAsync(model, userId);
            if (!result.IsSucceeded) return BadRequest();
            return Ok($"User {model.RatedId} Rated Successfully");
        }
    }
}