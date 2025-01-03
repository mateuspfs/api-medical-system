using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using SistemaMedico.Data;
using SistemaMedico.Models;
using SistemaMedico.Utilies;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SistemaMedico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SistemaMedicoDBContex _dbContext;
        private readonly string _jwtSecret;


        public AuthController(SistemaMedicoDBContex dbContext)
        {
            _dbContext = dbContext;
            _jwtSecret = "2o3@%7slh2kVdF9&nD$";
        }

        [HttpPost("login")]
        public ActionResult<AuthReturn> Login(string token)
        {
            try
            {
                // Valida o token com o Google
                var payload = GoogleJsonWebSignature.ValidateAsync(token).Result;
                var userEmail = payload.Email;

                var doctor = _dbContext.Doutores.FirstOrDefault(x => x.Email == userEmail);
                var admin = _dbContext.Admins.FirstOrDefault(x => x.Email == userEmail);

                if (doctor != null)
                {
                    return Ok(new AuthReturn
                    {
                        AccessToken = GenerateJwtToken(doctor.Id, "doutor"),
                        Role = "doutor"
                    });
                }
                else if (admin != null)
                {
                    return Ok(new AuthReturn
                    {
                        AccessToken = GenerateJwtToken(admin.Id, "admin"),
                        Role = "admin"
                    });
                }
                else
                {
                    return Unauthorized("Usuário não autorizado.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Falha ao validar o token: " + ex.Message);
            }
        }

        private string GenerateJwtToken(int userId, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, userId.ToString()),
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpGet]
        [Route("admin/protected")]
        [JwtProtectRoute("admin")]
        public IActionResult ProtectedAdminRoute()
        {
            return Ok();
        }

        [HttpGet]
        [Route("doutor/protected")]
        [JwtProtectRoute("doutor")]
        public IActionResult ProtectedDoutorRoute()
        {
            return Ok();
        }
    }
}
