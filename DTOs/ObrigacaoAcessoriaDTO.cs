using System.ComponentModel.DataAnnotations;
using GestaoObrigacoes.Modelos;

namespace GestaoObrigacoes.DTOs
{
    // DTO para criação
    public class ObrigacaoCriarDTO
    {
        [Required(ErrorMessage = "O nome da obrigação é obrigatório")]
        [MaxLength(200, ErrorMessage = "O nome não pode exceder 200 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A periodicidade é obrigatória")]
        public Periodicidade Periodicidade { get; set; }

        [Required(ErrorMessage = "O ID da empresa é obrigatório")]
        public int EmpresaId { get; set; }
    }

    // DTO para resposta
    public class ObrigacaoRespostaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public Periodicidade Periodicidade { get; set; }
        public int EmpresaId { get; set; }
        public string? EmpresaNome { get; set; }
    }

    // DTO para atualização
    public class ObrigacaoAtualizarDTO
    {
        [MaxLength(200, ErrorMessage = "O nome não pode exceder 200 caracteres")]
        public string? Nome { get; set; }
        public Periodicidade? Periodicidade { get; set; }
        public int? EmpresaId { get; set; }
    }
}