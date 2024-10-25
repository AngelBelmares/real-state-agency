using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using RealStateAgency.Models;
using RealStateAgency.Services;

namespace RealStateAgency.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly RealStateAgencyContext _context;
        private readonly IMapper _mapper;
        private readonly JwtTokenService _jwtTokenService;

        public UsersController(RealStateAgencyContext context, IMapper mapper, JwtTokenService jwtTokenService)
        {
            _context = context;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> CreateUser([FromBody] Login req)
        {
            try
            {
                string emailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

                if (req == null || req.Mail == null || req.Username == null)
                {
                    return BadRequest("Request inválida");
                }
                else if (req.Username?.Length < 8)
                {
                    return BadRequest("El usuario debe tener al menos 8 carácteres");
                }
                else if (!Regex.IsMatch(req.Mail, emailRegex))
                {
                    return BadRequest("Correo inválido");
                }
                else if (req.Password.Length < 8)
                {
                    return BadRequest("La contraseña debe tener al menos 8 carácteres");
                }

                User user = new()
                {
                    Password = req.Password,
                    Username = req.Username!,
                    Name = "",
                    Lastname = "",
                    Mail = req.Mail,
                    CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified)
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return Ok("Usuario creado exitosamente");
            }
            catch
            {
                return StatusCode(500, "Ocurrio un error inesperado");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Login req)
        {
            try
            {
                if (req == null)
                {
                    return BadRequest();
                }

                User? user = await _context.Users
                                        .Where(u => u.Mail.ToLower() == req.Mail.ToLower())
                                        .Where(u => u.Password.ToLower() == req.Password.ToLower())
                                        .FirstOrDefaultAsync();

                if (user == null)
                {
                    return NotFound("Correo o contraseña incorrectos");
                }

                Session session = new()
                {
                    Token = _jwtTokenService.GenerateJwtToken(user.Username),
                    User = _mapper.Map<UserDto>(user)
                };

                return Ok(session);
            }
            catch
            {
                return StatusCode(500, "Ocurrio un error inesperado");
            }
        }
    }
}