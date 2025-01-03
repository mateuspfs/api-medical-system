using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaMedico.DTOs;
using SistemaMedico.Models;
using SistemaMedico.Repositories.Interfaces;
using SistemaMedico.Utilies;

namespace SistemaMedico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteController(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        [HttpGet("filter")]
        public async Task<ActionResult<PagedResult<PacienteDTO>>> Filter(string? search, int pageNumber, int pageSize)
        {
            var Pacientees = await _pacienteRepository.Filter(search, pageNumber, pageSize);
            return Ok(Pacientees);
        }

        [HttpGet]
        public async Task<ActionResult<List<PacienteModel>>> All([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var pacientes = await _pacienteRepository.All(pageNumber, pageSize);
            return Ok(pacientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteModel>> Search(int id)
        {
            PacienteModel paciente = await _pacienteRepository.Search(id);
            return Ok(paciente);
        }

        [HttpPost]
        public async Task<ActionResult<PacienteModel>> Add(PacienteModel pacienteModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var novoPaciente = await _pacienteRepository.Add(pacienteModel);

            return Ok(novoPaciente);
        }         
        
        [HttpPut("{id}")]
        public async Task<ActionResult<PacienteModel>> Att(PacienteModel paciente, int id)
        {
            var pacienteAtt = await _pacienteRepository.Att(paciente, id);
            return Ok(pacienteAtt);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PacienteModel>> Destroy(int id)
        {
            bool destroy = await _pacienteRepository.Destroy(id);

            return Ok(destroy);
        }
    }
}
