using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HealthAids.Services;

namespace HealthAids.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;

        public AppointmentController(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateAppointmentRequest request)
        {
            

            var appointment = _appointmentService.Create(
                request.ClientId,
                request.DoctorName,
                request.Specialty,
                request.Date,
                request.Time,
                request.Reason
            );

           

            return Ok(new { success = true, appointment });
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetByUser(int userId)
        {
            
            var appointments = _appointmentService.GetByUser(userId);
          
            return Ok(new { appointments });
        }

        [HttpGet("upcoming/{userId}")]
        public IActionResult GetUpcoming(int userId)
        {
            var appointments = _appointmentService.GetUpcoming(userId);
            return Ok(new { appointments });
        }

        [HttpGet("history/{userId}")]
        public IActionResult GetHistory(int userId)
        {
            var appointments = _appointmentService.GetHistory(userId);
            return Ok(new { appointments });
        }

        [HttpPatch("{id}/cancel")]
        public IActionResult Cancel(int id)
        {
            var result = _appointmentService.Cancel(id);
            if (!result)
                return NotFound(new { success = false, message = "Cita no encontrada" });

            return Ok(new { success = true, message = "Cita cancelada" });
        }
    }

    public class CreateAppointmentRequest
    {
        public int ClientId { get; set; }  
        public string DoctorName { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
    }
}