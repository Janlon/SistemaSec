using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiteSec.Models
{
    public class MatrizFilial
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; } = 0;

        [Display(Name = "Empresa Filial", Description = "Razão social da empresa")]
        [ScaffoldColumn(false)]
        public List<int> Filiais { get; set; }

        [Display(Name = "Empresa Filial", Description = "Razão social da empresa")]
        [ScaffoldColumn(false)]
        public int FilialId { get; set; }

        [Display(Name = "Empresa Matriz", Description = "Razão social da empresa")]
        [ScaffoldColumn(false)]
        public int MatrizId { get; set; }

        [ScaffoldColumn(false)]
        public int EmpresaId { get; set; }
    }
}