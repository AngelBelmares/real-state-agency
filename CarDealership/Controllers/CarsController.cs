using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarDealership.Models;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace CarDealership.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController(CarDealershipContext context, IMapper mapper) : ControllerBase
    {
        private readonly CarDealershipContext _context = context;
        private readonly IMapper _mapper = mapper;

        [HttpGet()]
        public async Task<List<CarDto>> GetCars([FromQuery] CarFilter filter)
        {
            try
            {
                var query = _context.Cars.AsQueryable();

                if (filter.CarId.HasValue)
                {
                    query = query.Where(c => c.CarId == filter.CarId);
                }

                if(filter.DealershipId.HasValue)
                {
                    query = query.Where(c => c.DealershipId == filter.DealershipId);
                }

                var cars = await query.ProjectTo<CarDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return cars;
            }
            catch
            {
                return [];
            }
        }
    }
}
