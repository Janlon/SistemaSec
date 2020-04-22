using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiteSec.Models
{
    public class Retirada
    {

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Item", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Item", Description = "Item da Ordem de serviço")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public int ItemDaOrdemDeServicoId { get; set; } = 0;

        [Display(Name = "Pessoa", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Pessoa", Description = "Pessoa que irá assumir o item da ordem de serviço")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public int PessoaId { get; set; } = 0;

        [Display(Name = "Observações", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Observações", Description = "Digite se houver uma observação a fazer")]
        public string Descricao { get; set; } = "Sem informações complementares";

    }
}