using System.ComponentModel.DataAnnotations;

namespace GestaoObrigacoes.DTOs
{
    // DTO para criação
    public class EmpresaCriarDTO
    {
        [Required(ErrorMessage = "O nome da empresa é obrigatório")]
        [MaxLength(200, ErrorMessage = "O nome não pode exceder 200 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CNPJ é obrigatório")]
        [StringLength(18, MinimumLength = 14, ErrorMessage = "CNPJ inválido")]
        public string CNPJ { get; set; } = string.Empty;

        [Required(ErrorMessage = "O endereço é obrigatório")]
        public string Endereco { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O telefone é obrigatório")]
        [Phone(ErrorMessage = "Telefone inválido")]
        public string Telefone { get; set; } = string.Empty;
    }

    // DTO para resposta
    public class EmpresaRespostaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
    }

    // DTO para atualização
    public class EmpresaAtualizarDTO
    {
        [MaxLength(200, ErrorMessage = "O nome não pode exceder 200 caracteres")]
        public string? Nome { get; set; }

        [StringLength(18, MinimumLength = 14, ErrorMessage = "CNPJ inválido")]
        public string? CNPJ { get; set; }

        public string? Endereco { get; set; }

        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Telefone inválido")]
        public string? Telefone { get; set; }
    }
}