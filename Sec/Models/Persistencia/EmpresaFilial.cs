using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sec.Models
{
    [Table("Filiais", Schema = "Sec")]
    public class EmpresaFilial
    {

        [ScaffoldColumn(false), Key(), DatabaseGenerated(DatabaseGeneratedOption.None), Column(Order = 1)]
        public int EmpresaMatrizId { get; set; }

        [ScaffoldColumn(false), Key(), DatabaseGenerated(DatabaseGeneratedOption.None), Column(Order = 2)]
        public int EmpresaId { get; set; }

        [ForeignKey("EmpresaMatrizId")]
        public virtual EmpresaMatriz EmpresaMatriz { get; set; }

        [ForeignKey("EmpresaId")]
        public virtual Empresa Empresa { get; set; }

    }
}