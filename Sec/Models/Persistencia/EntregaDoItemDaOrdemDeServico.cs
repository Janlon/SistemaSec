namespace Sec.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    [Table("EntregasDasOrdensDeServiços", Schema = "Sec")]
    public class EntregaDoItemDaOrdemDeServico
    {
        /// <summary>
        /// Identificação do registro.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        /// <summary>
        /// Identificação do item.
        /// </summary>
        [ScaffoldColumn(false)]
        public int ItemId { get; set; }

        /// <summary>
        /// Identificação da pessoa RESPONSÁVEL pela entrega (funcionário que entregou o item).
        /// </summary>
        [ScaffoldColumn(false)]
        public int PessoaId { get; set; }

        /// <summary>
        /// Observações ocorridas durabnte a entrega (o entregador se perdeu).
        /// </summary>
        [Display(Name = "Observações", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Observações", Description = "Digite se houver uma observação a fazer")]
        [StringLength(100, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [MaxLength(100)]
        [Column("Descricao", TypeName = "VARCHAR")]
        public string Descricao { get; set; }

        /// <summary>
        /// Data de cadastro do registro de entrega (data do cadastro da entrega).
        /// </summary>
        [Display(Name = "Cadastro", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Cadastro", Description = "Data de cadastro")]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime")]
        public DateTime Cadastro { get; set; } = DateTime.Now;

        /// <summary>
        /// Data dde entrega (data da entrega).
        /// </summary>
        [Display(Name = "Execução", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Execução", Description = "Data de execução")]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime")]
        public DateTime Execucao { get; set; } = DateTime.Now;

        /// <summary>
        /// Registro de item.
        /// </summary>
        [ForeignKey("ItemId")]
        public virtual ItemDaOrdemDeServico Item { get; set; }

        /// <summary>
        /// Registro do entregador.
        /// </summary>
        [ForeignKey("PessoaId")]
        public virtual Pessoa Pessoa { get; set; }
    }   
}