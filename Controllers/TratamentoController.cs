using Microsoft.AspNetCore.Mvc;
using SistemaMedico.DTOs;
using SistemaMedico.Models;
using SistemaMedico.Repositories.Interfaces;
using SistemaMedico.Utilies;

namespace SistemaMedico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TratamentoController : ControllerBase
    {
        private readonly ITratamentoRepository _TratamentoRepository;

        public TratamentoController(ITratamentoRepository TratamentoRepository)
        {
            _TratamentoRepository = TratamentoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<TratamentoModel>>> PaginateAll([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var Tratamentos = await _TratamentoRepository.All(pageNumber, pageSize);
            return Ok(Tratamentos);
        }
        [HttpGet("filter")]
        public async Task<ActionResult<PagedResult<TratamentoModel>>> Filter(string? filterNome, string? filterEspecialidade, int pageNumber, int pageSize)
        {
            var Tratamentos = await _TratamentoRepository.Filter(filterNome, filterEspecialidade, pageNumber, pageSize);
            return Ok(Tratamentos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TratamentoModel>> Search(int id)
        {
            TratamentoModel Tratamento = await _TratamentoRepository.Search(id);
            return Ok(Tratamento);
        }

        [HttpPost]
        public async Task<ActionResult<TratamentoModel>> Add(TratamentoModel TratamentoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var novoTratamento = await _TratamentoRepository.Add(TratamentoModel);

            return Ok(novoTratamento);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TratamentoModel>> Att(TratamentoModel Tratamento, int id)
        {
            var TratamentoAtt = await _TratamentoRepository.Att(Tratamento, id);
            return Ok(TratamentoAtt);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TratamentoModel>> Destroy(int id)
        {
            bool destroy = await _TratamentoRepository.Destroy(id);

            return Ok(destroy);
        }
    }
}
