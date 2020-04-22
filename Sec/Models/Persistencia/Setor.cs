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
        public int EmpresaId { get; set; }

        [ScaffoldColumn(false)]
        public int TipoDeSetorId { get; set; }

        [Display(Name = "Empresa", AutoGenerateField = true, AutoGenerateFilter = true, Description = "Empresa", Prompt = "Empresa")]
        [ForeignKey("EmpresaId")]
        public virtual Empresa Empresa { get; internal set; }

        [Display(Name = "Tipo De Setor", AutoGenerateField = true, AutoGenerateFilter = true, Description = "Tipo de documento", Prompt = "Tipo")]
        [ForeignKey("TipoDeSetorId")]
        public virtual TipoDeSetor TipoDeSetor { get; internal set; }

        public virtual List<Equipamento> Equipamentos { get; internal set; } = new List<Equipamento>();

    }
}