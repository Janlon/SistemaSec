using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SiteSec.Models
{
    public class OrdemServico
    {
        #region propriedades de persitência

        [ScaffoldColumn(false)]
        public int Id { get; set; } = 0;

        [Display(Name = "Empresa", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Empresa")]
        [JsonProperty("empresaId")]
        [ScaffoldColumn(false)]
        public int EmpresaId { get; set; } = 0;

        [Display(Name = "Número", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Número da ordem de serviço")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public string Numero { get; set; } = "";

        #endregion



        #region propriedades de visualização

        [Display(Name = "Empresa", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Empresa")]
        public string Empresa { get; internal set; }

        [Display(Name = "Emissão", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Emissão", Description = "Data de emissão")]
        public DateTime Emissao { get; set; } = DateTime.Now;

        [Display(Name = "Validade", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Validade", Description = "Data de validade")]
        public DateTime Validade { get; set; } = DateTime.Now.AddDays(3);

        [Display(Name = "Situação", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Situação")]
        public string Situacao { get; set; } = Status.DentroDoPrazo.ToString();

        public enum Status
        {
            Atrasado,
            DentroDoPrazo,
            Fechado
        }
        #endregion


        #region propriedades de transferência

        [ScaffoldColumn(false)]
        public List<ItemOrdemServico> ItensOrdemservico { get; set; }    

        #endregion

    }

}