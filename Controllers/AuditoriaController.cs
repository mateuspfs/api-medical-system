using Microsoft.AspNetCore.Mvc;
using SistemaMedico.Models;

namespace SistemaMedico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditoriaController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<AuditoriaModel>> BucarAuditorias()
        {
            return Ok();
        }

    }
}
