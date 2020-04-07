namespace Sec.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Setores", Schema = "Sec")]
    public class Setor
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public int IdEmpresa { get; set; }

        [Display(Name = "Setor", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Setor")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(100, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
        [MaxLength(100)]
        [Column("Descricao", TypeName = "VARCHAR")]
        public string Descricao { get; set; }

        [Display(Name = "Sigla", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Sigla")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(10, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 2)]
        [MaxLength(10)]
        [Column("Sigla", TypeName = "VARCHAR")]
        public string Sigla { get; set; }

        [ForeignKey("IdEmpresa")]
        public virtual Empresa Empresa { get; internal set; }

        public virtual List<Equipamento> Equipamentos { get; internal set; } = new List<Equipamento>();
    }
}