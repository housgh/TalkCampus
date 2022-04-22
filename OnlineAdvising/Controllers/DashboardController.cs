using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using OnlineAdvising.Attributes;
using OnlineAdvising.Core.Interfaces;

namespace OnlineAdvising.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IPsychologistService _psychologistService;
        private readonly IUserService _userService;

        public DashboardController( IPsychologistService psychologistService, IUserService userService)
        {
            _psychologistService = psychologistService;
            _userService = userService;
        }
        
        [HttpGet("Psychologist")]
        public async Task<IActionResult> GetPsychologistDashboard()
        {
            HttpContext.Request.Headers.TryGetValue("userId", out var psychologistIdValue);
            var psychologistId = int.Parse(psychologistIdValue.ToString());
            var dashboard = await _psychologistService.GetPsychologistDashboard(psychologistId);
            if (!dashboard.IsSucceeded) return NotFound($"Psychologist with ID {psychologistId} Does not exist");
            return Ok(dashboard.Value);
        }

        [HttpGet("Patient")]
        public async Task<IActionResult> GetPatientDashboard()
        {
            HttpContext.Request.Headers.TryGetValue("userId", out var patientIdValue);
            var patientId = int.Parse(patientIdValue);
            var patient = await _userService.GetAsync(patientId);
            if (!patient.IsSucceeded) return NotFound($"Patient with ID {patientId} Does not exist");
            return Ok(new
            {
                Name = patient.Value.FirstName + " " + patient.Value.LastName,
                Email = patient.Value.Email,
                HasAppointment = patient.Value.HasAppointment,
                UpcomingChatId = patient.Value.UpcomingChatId
            });
        }
        
    }
}