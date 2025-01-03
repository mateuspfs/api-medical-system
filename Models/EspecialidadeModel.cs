namespace SistemaMedico.Models
{
    public class EspecialidadeModel
    { 
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public ICollection<TratamentoModel>? Tratamentos { get; set; }
        public ICollection<DoutorEspecialidadeModel> DoutorEspecialidades { get; set; }
    }
}
