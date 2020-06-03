using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiteSec.Models
{
    public class EmpresaFilial
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; } = 0;

        [Display(Name ="Empresa Filial", Description ="Razão social da empresa")]
        [ScaffoldColumn(false)]
        public List<int> FilialId { get; set; }

        [Display(Name = "Empresa Matriz", Description = "Razão social da empresa")]
        [ScaffoldColumn(false)]
        public int MatrizId { get; set; }

        /// <summary>
        /// representa a empresa matriz
        /// </summary>
        [ScaffoldColumn(false)]
        public int EmpresaId  => MatrizId;

    }
}