﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiteSec.Models
{
    public class Documento
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Número", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Número", Description = "Documento")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public string Numero { get; set; }

        [Display(Name = "Série", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Série", Description = "Série")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public string Serie { get; set; }

        [Display(Name = "Emissão", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Emissão", Description = "Data de Emissão")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public DateTime Emissao { get; set; }

        [Display(Name = "Validade", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Validade", Description = "Data de Validade")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public DateTime Validade { get; set; } 

        public int TipoDeDocumentoId { get; set; }
        public virtual TipoDocumento Tipo { get; set; }

        public int PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }
    }
}