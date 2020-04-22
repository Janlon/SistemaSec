using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sec.Models
{
    [Table("Equipamentos", Schema = "Sec")]
    public class Equipamento
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public int TipoEquipamentoId { get; set; }

        [ScaffoldColumn(false)]
        public int SetorId { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Cadastro", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Cadastro", Description = "Data de cadastro")]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        [ForeignKey("SetorId")]
        public virtual Setor Setor { get; set; }

        [ForeignKey("TipoEquipamentoId")]
        public virtual TipoDeEquipamento TipoDeEquipamento { get; set; }

        [Display(Name = "Imagens")]
        public virtual List<Imagem> Imagens { get; set; } = new List<Imagem>();

        [Display(Name = "Item Em Ordens de Serviço")]
        public virtual List<ItemDaOrdemDeServico> ItemEmOrdensDeServico { get; internal set; } = new List<ItemDaOrdemDeServico>();
    }
}