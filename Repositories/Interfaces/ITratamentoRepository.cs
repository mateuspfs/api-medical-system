using Microsoft.AspNetCore.Mvc;
using SistemaMedico.DTOs;
using SistemaMedico.Models;
using SistemaMedico.Utilies;

namespace SistemaMedico.Repositories.Interfaces
{
    public interface ITratamentoRepository
    {
        Task<PagedResult<TratamentoDTO>> Filter(string? filterNome, string? filterEspecialidade, int pageNumber, int pageSize);
        Task<PagedResult<TratamentoDTO>> All([FromQuery] int pageNumber, [FromQuery] int pageSize);
        Task<TratamentoModel> Search(int id);
        Task<TratamentoModel> Add([FromBody] TratamentoModel Tratamento);
        Task<TratamentoModel> Att(TratamentoModel Tratamento, int id);
        Task<bool> Destroy(int id);
    }
}
