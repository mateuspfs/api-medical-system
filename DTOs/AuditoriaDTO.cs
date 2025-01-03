using SistemaMedico.Models;

namespace SistemaMedico.DTOs
{
    public class AuditoriaDTO
    {
        public int Id { get; set; }
        public string Acao { get; set; }
        public DateTime DataHora { get; set; }
        public string DoutorNome { get; set; }
    }
}
