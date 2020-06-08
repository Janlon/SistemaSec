using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sec.Models
{
    [Table("MatrizesFiliais", Schema = "Sec")]
    public class EmpresaMatrizFilial
    {
        [Key(), DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 1), ScaffoldColumn(false)]
        public int MatrizId { get; set; }

        [Key(), DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 2), ScaffoldColumn(false)]
        public int FilialId { get; set; }
    }
}