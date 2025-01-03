using SistemaMedico.Utilies;

namespace SistemaMedico.DTOs
{
    public class EtapaPacienteDTO
    {
        public string Titulo { get; set; }
        public string Tratamento { get; set; }
        public string Descricao { get; set; }
        public PagedResult<PacienteDTO>? Pacientes { get; set; }
    }
}
