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
        #region propriedades para uso do task


        public string Title { get; set; }


        public string Description { get; set; }

  
        public string StartTimezone { get; set; }

  
        public DateTime Start { get; set; }

    
        public DateTime End { get; set; }


        public string EndTimezone { get; set; }


        public string RecurrenceRule { get; set; }


        public string RecurrenceException { get; set; }


        public bool IsAllDay { get; set; }

        #endregion

        #region propriedades para uso do custom editor

        /// <summary>
        /// id da ordem de serviço
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Item da ordem se serviço
        /// </summary>
        public int ItemId { get; set; }

        public int PessoaId { get; set; }

        public int EmpresaId { get; set; }

        [ScaffoldColumn(false)]
        public string ListaItens { get; set; } = "";

        #endregion
    }
}