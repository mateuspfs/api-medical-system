using SistemaMedico.Models;

namespace SistemaMedico.DTOs
{
    public class EspecialidadeDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public int CountTratamentos { get; set; }  
        public int CountDoutores { get; set; }  
    }
}
