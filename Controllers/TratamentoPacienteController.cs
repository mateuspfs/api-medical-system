using Microsoft.AspNetCore.Mvc;
using SistemaMedico.DTOs;
using SistemaMedico.Models;
using SistemaMedico.Repositories.Interfaces;
using SistemaMedico.Utilies;

namespace SistemaMedico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TratamentoPacienteController : ControllerBase
    {
        private readonly ITratamentoPacienteRepository _TratamentoPacienteRepository;

        public TratamentoPacienteController(ITratamentoPacienteRepository TratamentoPacienteRepository)
        {
            _TratamentoPacienteRepository = TratamentoPacienteRepository;
        }

        [HttpGet("/DoutorPacientes")]
        public async Task<ActionResult<PagedResult<TratamentoPacienteListDTO>>> DoctorPacientes(string token, int pageNumber, int pageSize, string? search, string? filterTratamento)
        {
            var Pacientes = await _TratamentoPacienteRepository.DoctorPacientes(token, pageNumber, pageSize, search, filterTratamento);
            return Ok(Pacientes);
        }

        [HttpPost]
        public async Task<ActionResult<TratamentoPacienteModel>> Add(string token, [FromForm] TratamentoPacienteAddDTO TP)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var novoTratamento = await _TratamentoPacienteRepository.Add(token, TP);

            return Ok(novoTratamento);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TratamentoPacienteModel>> Att([FromForm] List<IFormFile> Arquivos, int id, string token, int? novaEtapaId)
        {
            var TratamentoAtt = await _TratamentoPacienteRepository.Att(Arquivos, id, token, novaEtapaId);
            return Ok(TratamentoAtt);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TratamentoPacienteModel>> Destroy(int id)
        {
            bool destroy = await _TratamentoPacienteRepository.Destroy(id);
            return Ok(destroy);
        }

        [HttpGet("/IntegraTratamentoPaciente/{tratamentoPacienteId}")]
        public async Task<ActionResult<TratamentoPacienteViewDTO>> GetTratamentoPaciente(string token, int tratamentoPacienteId)
        {
            TratamentoPacienteViewDTO tratamento = await _TratamentoPacienteRepository.GetTratamentoPaciente(token, tratamentoPacienteId);
            return Ok(tratamento);
        }

        [HttpGet("arquivo/{nomeArquivo}")]
        public async Task<ActionResult> GetArquivo(string nomeArquivo)
        {
            var fileStream = await _TratamentoPacienteRepository.GetArquivo(nomeArquivo);
            if (fileStream == null)
            {
                return NotFound();
            }

            var contentType = GetContentType(nomeArquivo);
            return File(fileStream, contentType, nomeArquivo);
        }

        private string GetContentType(string nomeArquivo)
        {
            var types = new Dictionary<string, string>
            {
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".png", "image/png" },
                { ".pdf", "application/pdf" },
            };

            var ext = Path.GetExtension(nomeArquivo).ToLowerInvariant();
            return types.ContainsKey(ext) ? types[ext] : "application/octet-stream";
        }
    }
}
