namespace SistemaMedico.DTOs
{
    public class EtapaTratamentoDTO
    {
        public int Count { get; set; }
        public string TratamentoNome { get; set; }
        public int TratamentoId { get; set; }
        public List<EtapaDTO> Etapas { get; set; }
     }
}
