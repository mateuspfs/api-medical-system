using Microsoft.AspNetCore.Mvc;
using SistemaMedico.DTOs;
using SistemaMedico.Models;
using SistemaMedico.Repositories.Interfaces;
using SistemaMedico.Utilies;

namespace SistemaMedico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadeController : ControllerBase
    {
        private readonly IEspecialidadeRepository _especialidadeRepository;

        public EspecialidadeController(IEspecialidadeRepository especialidadeRepository)
        {
            _especialidadeRepository = especialidadeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<EspecialidadeDTO>>> All()
        {
            List<EspecialidadeModel> especialidades = await _especialidadeRepository.All();
            return Ok(especialidades);
        } 

        [HttpGet("filter")]
        public async Task<ActionResult<List<EspecialidadeDTO>>> Filter(string? search, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var especialidades = await _especialidadeRepository.Filter(search, pageNumber, pageSize);
            return Ok(especialidades);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<EspecialidadeDTO>> Search(int id)
        {
            EspecialidadeModel especialidade = await _especialidadeRepository.Search(id);
            return Ok(especialidade);
        }

        [HttpPost]
        public async Task<ActionResult<EspecialidadeDTO>> Add(string nome)
        {
            EspecialidadeModel novaEspecialidade = await _especialidadeRepository.Add(nome);
            return Ok(novaEspecialidade);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EspecialidadeDTO>> Att(string nome, int id)
        {
            EspecialidadeModel especialidadeAtt = await _especialidadeRepository.Att(nome, id);
            return Ok(especialidadeAtt);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EspecialidadeDTO>> Destroy(int id)
        {
            bool destroy = await _especialidadeRepository.Destroy(id);
            return Ok(destroy);
        }
    }
}
