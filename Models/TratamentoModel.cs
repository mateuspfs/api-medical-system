namespace SistemaMedico.Models
{
    public class TratamentoModel
    {
       public int Id { get; set; }
       public string Nome { get; set; }
       public int Tempo { get; set; }
       public int EspecialidadeId { get; set; }
       public EspecialidadeModel? Especialidade { get; set; }
       public ICollection<EtapaModel>? Etapas { get; set; }
    }
}
