namespace Sec.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("OrdensDeServicos", Schema = "Sec")]
    public class OrdemDeServico
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public int EmpresaId { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório.")]
        public string Numero { get; set; }

        [DataType(DataType.Date)]
        public DateTime Emissao { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime Validade { get; set; } = DateTime.Now.AddDays(7);


        [Display(Name = "Empresa", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Empresa")]
        [ForeignKey("EmpresaId")]
        public virtual Empresa Empresa { get; set; }

        [Display(Name = "Itens da Ordem de Serviço", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Itens da Ordem de Serviço")]
        public virtual List<ItemDaOrdemDeServico> Itens { get; set; } = new List<ItemDaOrdemDeServico>();
    }
}
