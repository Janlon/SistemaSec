namespace Generic.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Setores", Schema = "Sec")]
    public class Setor
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        //Hummm...
        [Display(Name = "Setor", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Setor")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(50, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
        [Index(IsUnique = true )]
        public string Descricao { get; set; } //Uma descrição de setor apenas.

        [Display(Name = "Sigla", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Sigla")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(10, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 2)]
        [Index(IsUnique = true)]
        public string Sigla { get; set; }
    }
}