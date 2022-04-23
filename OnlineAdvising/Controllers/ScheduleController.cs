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
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            HttpContext.Request.Headers.TryGetValue("userId", out var psychologistIdValue);
            var psychologistId = int.Parse(psychologistIdValue.ToString());
            var schedule = await _scheduleService.GetAsync(psychologistId);
            if (!schedule.IsSucceeded) return Ok(new ScheduleModel());
            return Ok(schedule.Value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ScheduleModel model)
        {
            HttpContext.Request.Headers.TryGetValue("userId", out var psychologistIdValue);
            model.PsychologistId = int.Parse(psychologistIdValue.ToString());
            var result = await _scheduleService.AddAsync(model);
            if (!result.IsSucceeded) return BadRequest();
            return Ok($"Schedule Created!");
        }
    }
}