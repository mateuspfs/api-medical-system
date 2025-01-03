using Microsoft.AspNetCore.Mvc;
using SistemaMedico.DTOs;
using SistemaMedico.Models;
using SistemaMedico.Utilies;

namespace SistemaMedico.Repositories.Interfaces
{
    public interface ITratamentoPacienteRepository
    {
        Task<PagedResult<TratamentoPacienteListDTO>> DoctorPacientes(string token, int pageNumber, int pageSize, string? search, string? filterTratamento);
        Task<TratamentoPacienteModel> Add(string token, [FromBody] TratamentoPacienteAddDTO TP);
        Task<TratamentoPacienteModel> Att(List<IFormFile> Arquivos, int id, string token, int? novaEtapaId);
        Task<bool> Destroy(int id);
        Task<TratamentoPacienteViewDTO> GetTratamentoPaciente(string token, int tratamentoPacienteId);
        Task<FileStream> GetArquivo(string nomeArquivo);
    }
}
