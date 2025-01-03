using SistemaMedico.Models;

namespace SistemaMedico.DTOs
{
    public class ArquivosTratamentoPacienteDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataUpload { get; set; }
    }
}
