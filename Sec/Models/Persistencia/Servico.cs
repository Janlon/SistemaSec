namespace Sec.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Servicos", Schema = "Sec")]
    public class Servico
    {
        /// <summary>
        /// Identificação do registro.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage ="{0} é requerido")]
        [StringLength(100, ErrorMessage ="{0} deve ter entre {2} e {1} caracteres", MinimumLength = 3)]
        [MaxLength(100)]
        [Column("Descricao", TypeName = "VARCHAR")]
        public string Descricao { get; set; }

        [Display(Name = "Itens da Ordem de Serviço", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Itens da Ordem de Serviço")]
        public virtual List<ItemDaOrdemDeServico> Itens { get; set; } = new List<ItemDaOrdemDeServico>();

    }
}