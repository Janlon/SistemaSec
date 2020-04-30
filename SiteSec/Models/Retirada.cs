using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiteSec.Models
{
    public class Retirada
    {
        #region propriedades de persitência

        [ScaffoldColumn(false)]
        public int Id { get; set; } = 0;

        [ScaffoldColumn(false)]
        public int ItemDaOrdemDeServicoId { get; set; } = 0;

        [Display(Name = "Empresa", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Empresa")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [ScaffoldColumn(false)]
        public int EmpresaId { get; set; } = 0;

        [Display(Name = "Pessoa", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Pessoa", Description = "Pessoa que irá assumir o item da ordem de serviço")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [ScaffoldColumn(false)]
        public int PessoaId { get; set; } = 0;

        [Display(Name = "Observações", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Observações", Description = "Digite se houver uma observação a fazer")]
        [StringLength(100, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        public string Descricao { get; set; } = "Sem informações complementares";

        #endregion

        #region propriedades de visualização

        /// <summary>
        /// proprieddae somente de visualizacao
        /// nao carrega valor
        /// </summary>
        public string Empresa { get; set; }

        /// <summary>
        /// proprieddae somente de visualizacao
        /// nao carrega valor
        /// </summary>
        public string Pessoa { get; set; }

        /// <summary>
        /// proprieddae somente de visualizacao
        /// nao carrega valor
        /// </summary>
        public DateTime Cadastro { get; set; }


        /// <summary>
        /// proprieddae somente de visualizacao
        /// nao carrega valor
        /// </summary>
        [Display(Name = "Execução")]
        public DateTime Execucao { get; set; }

        #endregion
    }
}