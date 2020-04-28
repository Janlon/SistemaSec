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
        public int OrdemId { get; set; }

        [Display(Name = "Ordem de Serviço")]
        public string Title { get; set; }

        [Display(Name = "Descrição")]
        public string Description { get; set; }
  
        private DateTime start;
        [Display(Name = "Emissão")]
        [Required]
        public DateTime Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value.ToUniversalTime();
            }
        }

        public string StartTimezone { get; set; }

        private DateTime end;
        [Display(Name = "Validade")]
        [Required]
        public DateTime End
        {
            get
            {
                return end;
            }
            set
            {
                end = value.ToUniversalTime();
            }
        }

        public string EndTimezone { get; set; }

        [Display(Name = "Recorrente")]
        public string RecurrenceRule { get; set; }

        public int? RecurrenceID { get; set; }

        public string RecurrenceException { get; set; }

        [Display(Name = "Dia Inteiro")]
        public bool IsAllDay { get; set; }

        public string Timezone { get; set; }

        [Display(Name = "Empresa")]
        public int EmpresaId { get; set; }

        public IEnumerable<int> Pessoas { get; set; }

        [Display(Name = "Pessoa")]
        public int PessoaId { get;  set; }

        [Display(Name = "Item da Ordem de Serviço")]
        public int ItemId { get;  set; }

        [Display(Name = "Itens da Ordem de Serviço")]
        public string Itens { get; set; }

    }

}