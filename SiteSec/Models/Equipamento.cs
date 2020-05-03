using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace SiteSec.Models
{
    public class Equipamento
    {

        #region propriedade de persistência

        [ScaffoldColumn(false)]
        public int Id { get; set; } = 0;

        [Display(Name = "Empresa", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Empresa")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [ScaffoldColumn(false)]
        public int EmpresaId { get; set; } = 0;

        [ScaffoldColumn(false)]
        public int SetorId { get; set; } = 0;

        [ScaffoldColumn(false)]
        public int TipoEquipamentoId { get; set; } = 0;

        #endregion

        #region propriedades de visualização

        [Display(Name = "Empresa", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Empresa")]
        public string Empresa { get; set; }

        [Display(Name = "Setor", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Setor")]
        public string Setor { get; set; }

        /// <summary>
        /// propriedade somente de visualizacao da view
        /// nao carrega valor
        /// </summary>
        [Display(Name = "Equipamento", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Equipamento")]
        public string Descricao { get; set; }

        [Display(Name = "Sigla do equipamento", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Sigla")]
        public string Sigla { get; set; }

        /// <summary>
        /// usado no controller [ImagemEquipamento]
        /// </summary>
        [JsonProperty("imagens")]
        public List<Imagem> Imagens { get; set; } = new List<Imagem>();

        #endregion

        #region propriedades de transferência

        [Display(Name = "Imagem", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Imagem")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public byte[] Files { get; set; }

        [Display(Name = "Tipos de equipamentos", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Tipo de equipamento")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public List<TipoEquipamento> TiposEquipamentos { get; set; }

        [Display(Name = "Setor", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Setor")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [ScaffoldColumn(false)]
        public int TipoSetorId { get; set; } = 0;

        /// <summary>
        /// usado no controller [itemordemServico]
        /// </summary>
        [ScaffoldColumn(false)]
        public List<Servico> Servicos { get; set; }
       
        #endregion

    }
}