namespace SistemaMedico.Models
{
    public class PagamentoEtapaModel
    {   
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public bool Pago { get; set; }
        public string? UrlCheck { get; set; }
        public int PagamentoId { get; set;}
        public int EtapaId { get; set;}
        public EtapaModel? Etapa { get; set;}
        public PagamentoModel? Pagamento { get; set;}
    }
}
