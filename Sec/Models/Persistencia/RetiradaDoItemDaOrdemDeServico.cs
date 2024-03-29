﻿namespace Sec.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RetiradasDasOrdensDeServiços", Schema = "Sec")]
    public class RetiradaDoItemDaOrdemDeServico
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }


        [ScaffoldColumn(false)]
        public int ItemDaOrdemDeServicoId { get; set; }

        /// <summary>
        /// Identificação da pessoa RESPONSÁVEL pela retirada (funcionário que retirou o item).
        /// </summary>
        [ScaffoldColumn(false)]
        public int PessoaId { get; set; }

        [Display(Name = "Observações", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Observações", Description = "Digite se houver uma observação a fazer")]
        [StringLength(100, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [Column(TypeName = "Text")]
        public string Descricao { get; set; } = "Sem informações complementares";

        [ScaffoldColumn(false)]
        [Display(Name = "Cadastro", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Cadastro", Description = "Data de cadastro")]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime")]
        public DateTime Cadastro { get; set; } = DateTime.Now;

        [Display(Name = "Execução", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Execução", Description = "Data de execução")]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime")]
        public DateTime Execucao { get; set; } = DateTime.Now;

        //[Display(Name = "Item", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Item da ordem de serviço")]
        //[Required(ErrorMessage = "{0} é obrigatório.")]
        [ForeignKey("ItemDaOrdemDeServicoId")]
        public virtual ItemDaOrdemDeServico Item { get; set; }

        //[Display(Name = "Pessoa", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Pessoa")]
        //[Required(ErrorMessage = "{0} é obrigatório.")]
        //[ForeignKey("PessoaId")]
        //public virtual Pessoa Pessoa { get; set; }

    }
}
