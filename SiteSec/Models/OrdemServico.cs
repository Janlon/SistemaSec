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
        [ScaffoldColumn(false)]
        public int Id { get; set; } = 0;
        public int PessoaId { get; set; } = 0;

        [Display(Name = "Número", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Número da ordem de serviço")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public string Numero { get; set; }

        [Display(Name = "Emissão", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Emissão", Description = "Data de Emissão")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public DateTime Emissao { get; set; }

    }

}