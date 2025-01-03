using SistemaMedico.DTOs;

namespace SistemaMedico.Utilies
{
    public class PagamentoResponse
    {
        public PagamentoEtapaDTO PagamentoEtapa { get; set; }
        public dynamic responsePagseguro { get; set; } 
    }
}
