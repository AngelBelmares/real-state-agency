using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using RealStateAgency.Models;

namespace RealStateAgency.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class HousesController(RealStateAgencyContext context, IMapper mapper) : ControllerBase
    {
        private readonly RealStateAgencyContext _context = context;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<List<House>> GetHouses([FromQuery] HouseFilter filter)
        {
            try
            {
                var query = _context.Houses.AsQueryable();

                if (filter.HouseId.HasValue)
                {
                    query = query.Where(c => c.HouseId == filter.HouseId);
                }

                var houses = await query.ToListAsync();

                return houses;
            }
            catch
            {
                return [];
            }
        }
    }
}
