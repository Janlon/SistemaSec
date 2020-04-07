using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiteSec.Models
{
    public class Cargo
    {
        [ScaffoldColumn(false)]
        public string Id { get; set; } = "";

        [Display(Name = "Atividade", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Atividade")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(50, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
        public string Descricao { get; set; } = "";

        [Display(Name = "Pessoa física", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Pessoa fisica", Description = "Se aplica à pessoas físicas?")]
        public bool PessoaFisica { get; set; } = false;
    }
}