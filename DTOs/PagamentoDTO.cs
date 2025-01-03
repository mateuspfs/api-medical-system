using SistemaMedico.Models;

namespace SistemaMedico.DTOs
{
    public class PagamentoDTO
    {
        public int? Id { get; set; }
        public DateTime Updated_at { get; set; }
        public DateTime Created_at { get; set; }
        public ICollection<PagamentoEtapaDTO>? PagamentoEtapas { get; set; }
    }
}
