namespace Sec.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ItensDasOrdensDeServicos", Schema = "Sec")]
    public class ItemDaOrdemDeServico
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        
        public int OrdemId { get; set; }

        [ScaffoldColumn(false)]

        public int ServicoId { get; set; }

        [ScaffoldColumn(false)]
        public int EquipamentoId { get; set; }




        [Display(Name = "Ordem de Serviço", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Ordem de Serviço")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [ForeignKey("OrdemId")]
        public virtual OrdemDeServico OrdemDeServico { get; set; }

        [Display(Name = "Tipo de Serviço", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Ordem de Serviço")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [ForeignKey("ServicoId")]
        public virtual Servico Servico { get; set; }

        [Display(Name = "Equipamento", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Equipamento")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [ForeignKey("EquipamentoId")]
        public virtual Equipamento Equipamento { get; set; }
    }
}