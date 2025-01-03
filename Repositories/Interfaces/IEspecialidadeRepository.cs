using Microsoft.AspNetCore.Mvc;
using SistemaMedico.DTOs;
using SistemaMedico.Models;
using SistemaMedico.Utilies;

namespace SistemaMedico.Repositories.Interfaces
{
    public interface IEspecialidadeRepository
    {
        Task<List<EspecialidadeModel>> All();
        Task<PagedResult<EspecialidadeDTO>> paginateAll([FromQuery] int pageNumber, [FromQuery] int pageSize);
        Task<PagedResult<EspecialidadeDTO>> Filter(string? search, [FromQuery] int pageNumber, [FromQuery] int pageSize);
        Task<EspecialidadeModel> Search(int id);
        Task<EspecialidadeModel> Add(string nome);
        Task<EspecialidadeModel> Att(string nome, int id);
        Task<bool> Destroy(int id);
    }
}
