using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GestaoObrigacoes.Modelos
{
    [Table("empresas")]
    public class Empresa
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da empresa é obrigatório")]
        [Column("nome")]
        [MaxLength(200, ErrorMessage = "O nome não pode exceder 200 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CNPJ é obrigatório")]
        [Column("cnpj")]
        [StringLength(18, MinimumLength = 14, ErrorMessage = "CNPJ inválido")]
        public string CNPJ { get; set; } = string.Empty;

        [Required(ErrorMessage = "O endereço é obrigatório")]
        [Column("endereco")]
        public string Endereco { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [Column("email")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O telefone é obrigatório")]
        [Column("telefone")]
        [Phone(ErrorMessage = "Telefone inválido")]
        public string Telefone { get; set; } = string.Empty;

        // Propriedade de navegação para as obrigações
        [JsonIgnore]
        public ICollection<ObrigacaoAcessoria>? Obrigacoes { get; set; }
    }
}