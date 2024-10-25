using AutoMapper;
using CarDealership.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarDealership.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AppointmentsController(CarDealershipContext context, IMapper mapper) : ControllerBase
    {
        private readonly CarDealershipContext _context = context;

        [HttpGet]
        public async Task<List<Appointment>> GetAppointments([FromQuery] AppointmentFilter filter)
        {
            try
            {
                var query = _context.Appointments.AsQueryable();

                if (filter.AppointmentId.HasValue)
                {
                    query = query.Where(c => c.AppointmentId == filter.AppointmentId);
                }

                if (filter.UserId.HasValue)
                {
                    query = query.Where(c => c.UserId == filter.UserId);
                }

                if (filter.HouseId.HasValue)
                {
                    query = query.Where(c => c.HouseId == filter.HouseId);
                }

                if (filter.AgentId.HasValue)
                {
                    query = query.Where(c => c.AgentId == filter.AgentId);
                }

                var appointments = await query.ToListAsync();

                return appointments;
            }
            catch
            {
                return [];
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAppointment([FromBody] Appointment req)
        {
            try
            {
                if (req == null || req.UserId == null || req.AgentId == null || req.HouseId == null || req.Date == null)
                {
                    return BadRequest("Request inválida");
                }

                Appointment appointment = new()
                {
                    UserId = req.UserId,
                    AgentId = req.AgentId,
                    HouseId = req.HouseId,
                    Date = req.Date,
                    CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified)
                };

                await _context.Appointments.AddAsync(appointment);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch
            {
                return BadRequest("Error al crear la cita");
            }
        }
    }
}
