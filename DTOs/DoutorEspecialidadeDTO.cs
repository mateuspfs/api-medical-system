using SistemaMedico.Models;

namespace SistemaMedico.DTOs
{
    public class DoutorEspecialidadeDTO
    {
        public int Id { get; set; }
        public int DoutorId { get; set; }
        public DoutorModel Doutor { get; set; }
        public int EspecialidadeId { get; set; }
        public EspecialidadeModel Especialidade { get; set; }
    
    }
}
