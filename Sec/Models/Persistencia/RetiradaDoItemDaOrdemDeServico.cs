namespace Sec.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RetiradasParaAsOrdensDeServiço", Schema = "Sec")]
    public class RetiradaDoItemDaOrdemDeServico
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public int ItemId { get; set; }

        [ScaffoldColumn(false)]
        public int PessoaId { get; set; }

        [Display(Name = "Observações", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Observações", Description = "Digite se houver uma observação a fazer")]
        [StringLength(100, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [Column(TypeName = "Text")]
        public string Descricao { get; set; }

        [Display(Name = "Cadastro", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Cadastro", Description = "Data de cadastro")]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime")]
        DateTime Cadastro { get; set; }

        [Display(Name = "Execução", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Execução", Description = "Data de execução")]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime")]
        DateTime Execucao { get; set; }



        [ForeignKey("ItemId")]
        public virtual ItemDaOrdemDeServico Item { get; set; }

        [ForeignKey("PessoaId")]
        public virtual Pessoa Pessoa { get; set; }
    }
}
