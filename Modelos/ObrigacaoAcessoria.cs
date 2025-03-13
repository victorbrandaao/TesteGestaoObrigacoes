using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoObrigacoes.Modelos
{
    [Table("obrigacoes_acessorias")]
    public class ObrigacaoAcessoria
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da obrigação é obrigatório")]
        [Column("nome")]
        [MaxLength(200, ErrorMessage = "O nome não pode exceder 200 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A periodicidade é obrigatória")]
        [Column("periodicidade")]
        public Periodicidade Periodicidade { get; set; }

        [Required(ErrorMessage = "A empresa é obrigatória")]
        [Column("empresa_id")]
        public int EmpresaId { get; set; }

        // Relacionamento com Empresa
        public Empresa? Empresa { get; set; }
    }
}