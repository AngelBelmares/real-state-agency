using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealStateAgency.Models;

namespace RealStateAgency.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class AppointmentsController(RealStateAgencyContext context, IMapper mapper) : ControllerBase
    {
        private readonly RealStateAgencyContext _context = context;

        [HttpGet]
        public async Task<List<AppointmentDto>> GetAppointments([FromQuery] AppointmentFilter filter)
        {
            try
            {
                var query = _context.Appointments
                                    .ProjectTo<AppointmentDto>(mapper.ConfigurationProvider)
                                    .OrderByDescending(a => a.Date)
                                    .AsQueryable();

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
                    Date = DateTime.SpecifyKind(req.Date ?? DateTime.UtcNow, DateTimeKind.Unspecified),
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
