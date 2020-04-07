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

        [Display(Name = "Equipamento", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Equipamento", Description = "Nome do Equipamento")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(100, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
        [MaxLength(100)]
        [Column("Descricao", TypeName = "VARCHAR")]
        public string Descricao { get; set; }

        [Display(Name = "Sigla", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Sigla", Description ="Sigla do Equipamento")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(10, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 2)]
        [MaxLength(10)]
        [Column("Sigla", TypeName = "VARCHAR")]
        public string Sigla { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Cadastro", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Cadastro", Description = "Data de cadastro")]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        [ScaffoldColumn(false)]
        public int SetorId { get; set; }

        [ForeignKey("SetorId")]
        public virtual Setor Setor { get; set; }

        [Display(Name = "Imagens")]
        public virtual List<Imagem> Imagens { get; internal set; } = new List<Imagem>();

        [Display(Name = "Item Em Ordens de Serviço")]
        public virtual List<ItemDaOrdemDeServico> ItemEmOrdensDeServico { get; internal set; } = new List<ItemDaOrdemDeServico>();
    }
}