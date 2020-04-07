using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiteSec.Models
{
    public class Imagem
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nome", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Nome da imagem")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Referência", AutoGenerateField = true, AutoGenerateFilter = true, Description = "Imagem de referência", Prompt = "Principal")]
        public bool Principal { get; set; } = false;


        [Required]
        [Display(Name = "Imagem", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Imagem")]
        public string Foto64 { get; set; }

    }
}