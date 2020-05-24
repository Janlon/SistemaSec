using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SiteSec.Models
{
    public class Setor
    {
        #region propriedades de persitência

        [ScaffoldColumn(false)]
        public int Id { get; set; } = 0;

        [Display(Name = "Empresa", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Empresa")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [ScaffoldColumn(false)]
        public int EmpresaId { get; set; } = 0;

        [ScaffoldColumn(false)]
        public int TipoDeSetorId { get; set; } = 0;

        #endregion

        #region propriedades de visualização

        [Display(Name = "Empresa", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Empresa")]
        public string Empresa { get; set; }

        /// <summary>
        /// propriedades de visualizacao da view
        /// </summary>
        [Display(Name = "Setor", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Setor")]
        public string Descricao { get; set; }

        [Display(Name = "Sigla do setor", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Sigla")]
        public string Sigla { get; set; }

        #endregion

        #region propriedades de transferência

        [Display(Name = "Tipo de Setor", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Tipo de Setor")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public List<TipoSetor> TiposDeSetores { get; set; }

        [ScaffoldColumn(false)]
        public List<Equipamento> Equipamentos { get; set; }

        #endregion

    }
}