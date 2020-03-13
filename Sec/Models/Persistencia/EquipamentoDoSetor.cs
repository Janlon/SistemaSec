namespace Sec.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EquipamentosDosSetores", Schema = "Sec")]
    public class EquipamentoDoSetor
    {
        [ScaffoldColumn(false), Key(), DatabaseGenerated(DatabaseGeneratedOption.None), Column(Order = 2)]
        public int SetorId { get; set; }

        [ScaffoldColumn(false), Key(), DatabaseGenerated(DatabaseGeneratedOption.None), Column(Order = 1)]
        public int EquipamentoId { get; set; }

        [ForeignKey("SetorId")]
        public virtual Setor Setor { get; set; }

        [ForeignKey("EquipamentoId")]
        public virtual Equipamento Equipamento { get; set; }
    }
}
