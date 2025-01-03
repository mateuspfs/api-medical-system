using SistemaMedico.Models;

namespace SistemaMedico.DTOs
{
    public class TratamentoDTO
    {
       public int Id { get; set; }
       public string Nome { get; set; }
       public int Tempo { get; set; }
       public int EspecialidadeId { get; set; }
       public string NomeEspecialidade { get; set; }
       public ICollection<EtapaDTO>? Etapas { get; set; }
    }
}
