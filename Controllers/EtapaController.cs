using Microsoft.AspNetCore.Mvc;
using SistemaMedico.DTOs;
using SistemaMedico.Models;
using SistemaMedico.Repositories.Interfaces;

namespace SistemaMedico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtapaController : ControllerBase
    {
        private readonly IEtapaRepository _EtapaRepository;

        public EtapaController(IEtapaRepository EtapaRepository)
        {
            _EtapaRepository = EtapaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<EtapaModel>>> All()
        {
            List<EtapaModel> Etapas = await _EtapaRepository.All();
            return Ok(Etapas);
        }

        [HttpGet("Pacientes")]
        public async Task<ActionResult<List<EtapaModel>>> PacientesEtapa(string? search, int pageNumber, int pageSize, int id)
        {
            EtapaPacienteDTO result= await _EtapaRepository.PacientesEtapa(search,  pageNumber,  pageSize, id);
            return Ok(result);
        }

        [HttpGet("tratamento/{id}")]
        public async Task<ActionResult<EtapaTratamentoDTO>> SearchTratamento(int id)
        {
            EtapaTratamentoDTO Etapas = await _EtapaRepository.SearchTratamento(id);
            return Ok(Etapas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EtapaModel>> Search(int id)
        {
            EtapaModel Etapa = await _EtapaRepository.Search(id);
            return Ok(Etapa);
        }

        [HttpPost]
        public async Task<ActionResult<EtapaModel>> Add(EtapaModel Etapa)
        {
            EtapaModel novaEtapa = await _EtapaRepository.Add(Etapa);
            return Ok(novaEtapa);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EtapaModel>> Att(EtapaModel Etapa, int id)
        {
            EtapaModel EtapaAtt = await _EtapaRepository.Att(Etapa, id);
            return Ok(EtapaAtt);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EtapaModel>> Destroy(int id)
        {
            bool destroy = await _EtapaRepository.Destroy(id);
            return Ok(destroy);
        }
    }
}
