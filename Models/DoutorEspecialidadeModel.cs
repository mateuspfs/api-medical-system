using Microsoft.AspNetCore.Components.Routing;

namespace SistemaMedico.Models
{
    public class DoutorEspecialidadeModel
    {
        public int Id { get; set; }
        public int DoutorId { get; set; }
        public DoutorModel Doutor { get; set; }
        public int EspecialidadeId { get; set; }
        public EspecialidadeModel Especialidade { get; set; }
    }
}
