using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SiteSec.Models
{
    public partial class  Agenda : ISchedulerEvent
    {
        public int OrdemId { get; set; } = 0;

        [Display(Name = "Ordem de Serviço")]
        public string Title { get; set; } = "";

        [Display(Name = "Observações", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Observações", Description = "Digite se houver uma observação a fazer")]
        [StringLength(100, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        public string Description { get; set; } = "Sem informações complementares";
   

        private DateTime start;
        [Required]
        [Display(Name = "Emissão")]
        public DateTime Start{ get => start;  set => start = value.ToLocalTime();  }

        public string StartTimezone { get; set; } = null;

        private DateTime end;
        [Display(Name = "Validade")]
        [Required]
        public DateTime End { get => end; set => end = value.ToLocalTime(); }

        public string EndTimezone { get; set; } = null;

        [Display(Name = "Recorrente")]
        public string RecurrenceRule { get; set; } = null;

        public int? RecurrenceID { get; set; } = null;

        public string RecurrenceException { get; set; } = null;

        [Display(Name = "Dia Inteiro")]
        public bool IsAllDay { get; set; } = false;

        public string Timezone { get; set; }

        [Display(Name = "Empresa")]
        public int EmpresaId { get; set; } = 0;

        [Display(Name = "Responsável", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Pessoa", Description = "Pessoa(s) que irá assumir o item da ordem de serviço")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public IEnumerable<int> Pessoas { get; set; }

        [Display(Name = "Pessoa")]
        public int PessoaId { get;  set; } = 0;

        [Display(Name = "Equipamento")]
        public int ItemId { get;  set; } = 0;

        [Display(Name = "Equipamentos")]
        public string Itens { get; set; } = "";

        public List<ItemOrdemServico> ItemOrdemServicos { get; set; }
    }

}