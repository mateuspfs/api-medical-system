using SistemaMedico.Data.Map;

namespace SistemaMedico.Models
{
    public class DoutorModel
    {
        public int Id { get; set;}
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Endereco { get; set; }
        public string DocumentoNome { get; set; }
        public ICollection<DoutorEspecialidadeModel>? DoutorEspecialidades { get; set; }
    }
}
