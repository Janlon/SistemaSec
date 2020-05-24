using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiteSec.Models
{
    public class Regra
    {

        [JsonProperty("users")]
        public object[] Users { get; set; }

        [ScaffoldColumn(false)]
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [Display(Name = "Permissão", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Permissão")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(50, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
        [JsonProperty("name")]
        public string Name { get; set; }

    }
}