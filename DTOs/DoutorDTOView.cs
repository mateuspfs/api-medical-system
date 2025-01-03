using Microsoft.AspNetCore.Connections.Features;
using SistemaMedico.Models;
using System.ComponentModel.DataAnnotations;

namespace SistemaMedico.DTOs
{
    public class DoutorDTOView
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

        [Required(ErrorMessage = "Documento obrigatório")]
        public string DocumentoNome { get; set; }

        [Required(ErrorMessage = "Especialidades obrigatório")]
        public string Especialidades { get; set; }
    }
}
