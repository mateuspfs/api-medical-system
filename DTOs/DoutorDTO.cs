using SistemaMedico.Models;
using System.ComponentModel.DataAnnotations;

namespace SistemaMedico.DTOs
{
    public class DoutorDTO
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Email obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefone obrigatório")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Cpf obrigatório")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Endereço obrigatório")]
        public string Endereco { get; set; }
        
        public string? DocumentoNome { get; set; }

        public IFormFile? Documento { get; set; }
    }
}
