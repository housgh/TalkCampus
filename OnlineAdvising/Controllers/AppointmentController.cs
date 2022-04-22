using System;
using System.Linq;
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
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointments()
        {
            HttpContext.Request.Headers.TryGetValue("userId", out var userIdValue);
            var userId = int.Parse(userIdValue);
            var appointments = await _appointmentService.GetPerUserAsync(userId);
            if (!appointments.IsSucceeded) return BadRequest();
            return Ok(new
            {
                Today = appointments.Value.Where(x => x.StartDate.Date == DateTime.Now.Date).ToList(),
                Tomorrow = appointments.Value.Where(x => x.StartDate.Date == DateTime.Now.Date.AddDays(1)).ToList(),
                ThisWeek = appointments.Value.Where(x => x.StartDate.Date >= DateTime.Now.Date.AddDays(2) 
                                                         && x.StartDate.Date < DateTime.Now.Date.AddDays(8)).ToList()
            });
        }
        
        [HttpPost]
        public async Task<IActionResult> AddAppointment(AppointmentModel model)
        {
            HttpContext.Request.Headers.TryGetValue("userId", out var userIdValue);
            var userId = int.Parse(userIdValue);
            model.PatientId = userId;
            var result = await _appointmentService.AddAsync(model);
            if (!result.IsSucceeded) return BadRequest();
            return Ok($"{result.Value} Appointments Added");
        }

        [HttpPut("Decline/{id}")]
        public async Task<IActionResult> DeclineAppointment(int id)
        {
            var result = await _appointmentService.DeclineAppointment(id);
            if (!result.IsSucceeded) return BadRequest();
            return Ok($"Appointment with ID {id} Declined");
        }

        [HttpPut("End/{id}")]
        public async Task<IActionResult> EndAppointment(int id)
        {
            var result = await _appointmentService.EndAppointment(id);
            if (!result.IsSucceeded) return BadRequest();
            return Ok($"Appointment with ID {id} Declined");
        }
    }
}