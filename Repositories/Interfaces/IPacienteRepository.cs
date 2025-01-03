using Microsoft.AspNetCore.Mvc;
using SistemaMedico.DTOs;
using SistemaMedico.Models;
using SistemaMedico.Utilies;

namespace SistemaMedico.Repositories.Interfaces
{
    public interface IPacienteRepository
    {
        Task<PagedResult<PacienteDTO>> Filter(string? search, int pageNumber, int pageSize);
        Task<PagedResult<PacienteModel>> All([FromQuery] int pageNumber, [FromQuery] int pageSize);
        Task<PacienteModel> Search(int id);
        Task<PacienteModel> Add([FromBody] PacienteModel paciente);
        Task<PacienteModel> Att(PacienteModel Paciente, int id);
        Task<bool> Destroy(int id);
    }
}
