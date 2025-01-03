using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaMedico.DTOs;
using SistemaMedico.Models;
using SistemaMedico.Repositories.Interfaces;
using SistemaMedico.Utilies;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Uol.PagSeguro;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Exception;

namespace SistemaMedico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        private readonly IPagamentoRepository _PagamentoRepository;        

        public PagamentoController(IPagamentoRepository PagamentoRepository, IMapper mapper, IPacienteRepository pacienteRepository)
        {
            _PagamentoRepository = PagamentoRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PagamentoModel>> Search(int id)
        {
            PagamentoEtapaModel Pagamento = await _PagamentoRepository.Search(id);
            return Ok(Pagamento);
        }

        [HttpPost]
        public async Task<ActionResult> Add(string token, PagamentoEtapaDTO pagamentoDTO, int tratamentoId)
        {
            PagamentoResponse response = await _PagamentoRepository.Add(token, pagamentoDTO, tratamentoId);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PagamentoEtapaModel>> Att(PagamentoEtapaModel Pagamento, int id)
        {
            PagamentoEtapaModel PagamentoAtt = await _PagamentoRepository.Att(Pagamento, id);
            return Ok(PagamentoAtt);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PagamentoModel>> Destroy(int id)
        {
            bool destroy = await _PagamentoRepository.Destroy(id);
            return Ok(destroy);
        }
    }
}
