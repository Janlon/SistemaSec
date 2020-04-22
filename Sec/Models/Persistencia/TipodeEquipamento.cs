namespace Sec.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// A ideia é que cada empresa tenha um equipamento  com um tipo de equipamento
    /// exemplo : A santa Casa tem 2 equipamentos do tipo estetoscopio
    /// as diferencas estarao no objeto equipamento
    /// </summary>
    [Table("TiposDeEquipamentos", Schema = "Sec")]
    public class TipoDeEquipamento
    {
        /// <summary>
        /// Identificação do registro.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        /// <summary>
        /// Tipo do equipamento.
        /// </summary>
        [Display(Name = "Tipo de equipamento", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Tipo de equipamento")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(100, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
        [MaxLength(100)]
        [Column("Descricao", TypeName = "VARCHAR")]
        public string Descricao { get; set; }

        /// <summary>
        /// Sigla do equipamento.
        /// </summary>
        [Display(Name = "Sigla", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Sigla", Description = "Sigla do equipamento")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(10, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 2)]
        [MaxLength(10)]
        [Column("Sigla", TypeName = "VARCHAR")]
        public string Sigla { get; set; }

    }
}