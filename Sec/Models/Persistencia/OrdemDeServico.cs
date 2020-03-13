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

        //[ScaffoldColumn(false)]
        //public int EquipamentoId { get; set; }

        [ScaffoldColumn(false)]
        public int EnderecoId { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório.")]
        public string Numero { get; set; }

        public DateTime Emissao { get; set; }

        [Display(Name = "Endereco", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Endereco")]
        [ForeignKey("EnderecoId")]
        public virtual Endereco Endereco { get; set; }

        [Display(Name = "Itens da Ordem de Serviço", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Itens da Ordem de Serviço")]
        public List<ItemDaOrdemDeServico> Itens { get; set; } = new List<ItemDaOrdemDeServico>();
    }
}
