using SistemaMedico.DTOs;
using SistemaMedico.Models;

namespace SistemaMedico.Repositories.Interfaces
{
    public interface IEtapaRepository
    {
        Task<List<EtapaModel>> All();
        Task<EtapaModel> Search(int id);
        Task<EtapaPacienteDTO> PacientesEtapa (string? search, int pageNumber, int pageSize, int id);
        Task<EtapaTratamentoDTO> SearchTratamento(int id);
        Task<EtapaModel> Add(EtapaModel etapa);
        Task<EtapaModel> Att(EtapaModel etapa, int id);
        Task<bool> Destroy(int id);
    }
}
