using SistemaMedico.Models;

namespace SistemaMedico.DTOs
{
    public class EtapaDTO
    {
        public int Id { get; set; }
        public int? Numero { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
    }
}
