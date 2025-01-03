using Microsoft.AspNetCore.Mvc;
using SistemaMedico.DTOs;
using SistemaMedico.Models;
using SistemaMedico.Utilies;

namespace SistemaMedico.Repositories.Interfaces
{
    public interface IPagamentoRepository
    {
        Task<PagamentoEtapaModel> Search(int id);
        Task<PagamentoResponse> Add(string token, PagamentoEtapaDTO pagamentoDTO, int tratamentoId);
        Task<PagamentoEtapaModel> Att(PagamentoEtapaModel pagamento, int id);
        Task<bool> Destroy(int id);
    }
}
