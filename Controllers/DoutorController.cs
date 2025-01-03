using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaMedico.DTOs;
using SistemaMedico.Models;
using SistemaMedico.Repositories.Interfaces;
using SistemaMedico.Utilies;
using System.Text.Json.Serialization;

namespace SistemaMedico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoutorController : ControllerBase
    {
        private readonly IDoutorRepository _DoutorRepository;

        public DoutorController(IDoutorRepository DoutorRepository, IMapper mapper)
        {
            _DoutorRepository = DoutorRepository;
        }

        [HttpGet("filter")]
        public async Task<ActionResult<PagedResult<DoutorDTOView>>> Filter(string? search, string? filterEspecialidade, int pageNumber, int pageSize)
        {
            var Doutores = await _DoutorRepository.Filter( search,  filterEspecialidade, pageNumber, pageSize);
            return Ok(Doutores);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<DoutorDTOView>>> All([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var Doutores = await _DoutorRepository.All(pageNumber, pageSize);
            return Ok(Doutores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoutorDTOView>> Search(int id)
        {
            var Doutor = await _DoutorRepository.Search(id);
            if (Doutor == null)
            {
                return NotFound();
            }

            return Doutor;
        }

        [HttpPost]
        public async Task<ActionResult<DoutorModel>> Add([FromForm] DoutorDTO DoutorModel, [FromQuery] string especialidadeIds)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var novoDoutor = await _DoutorRepository.Add(DoutorModel, especialidadeIds);
            return Ok(novoDoutor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DoutorModel>> Att([FromForm] DoutorDTO Doutor, int id, [FromQuery] string especialidadeIds)
        {
            var DoutorAtt = await _DoutorRepository.Att(Doutor, id, especialidadeIds);
            return Ok(DoutorAtt);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DoutorModel>> Destroy(int id)
        {
            bool destroy = await _DoutorRepository.Destroy(id);
            return Ok(destroy);
        }

        [HttpGet("TratamentosDoutor")]
        public async Task<ActionResult<PagedResult<TratamentoDTO>>> TratamentosDoutor(string token, string? filterNome, int? filterEspecialidade, int pageNumber, int pageSize)
        {
            var tratamentos = await _DoutorRepository.TratamentosDoutor(token, filterNome, filterEspecialidade, pageNumber, pageSize);
            return Ok(tratamentos);
        }

        [HttpGet("EspecialidadesDoutor")]
        public async Task<ActionResult<List<EspecialidadeDTO>>> EspecialidadesDoutor(string token)
        {
            List<EspecialidadeDTO> especilalidades = await _DoutorRepository.EspecialidadesDoutor(token);
            return Ok(especilalidades);
        }

        [HttpGet("imagem/{nomeArquivo}")]
        public async Task<ActionResult> GetImagem(string nomeArquivo)
        {
            var fileStream = await _DoutorRepository.GetImagem(nomeArquivo);
            if (fileStream == null)
            {
                return NotFound();
            }

            var contentType = GetContentType(nomeArquivo);
            return new FileStreamResult(fileStream, contentType);
        }

        private string GetContentType(string nomeArquivo)
        {
            var types = new Dictionary<string, string>
            {
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".png", "image/png" },
            };

            var ext = Path.GetExtension(nomeArquivo).ToLowerInvariant();
            return types.ContainsKey(ext) ? types[ext] : "application/octet-stream";
        }
    }
}
