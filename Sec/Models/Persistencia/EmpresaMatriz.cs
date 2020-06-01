using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sec.Models
{
    [Table("Matrizes", Schema = "Sec")]
    public class EmpresaMatriz
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        /// <summary>
        /// representa a empresa matriz
        /// </summary>
        [ScaffoldColumn(false)]
        public int EmpresaId { get; set; }

        [ForeignKey("EmpresaId")]
        public virtual Empresa Empresa { get; set; }

        [Display(Name = "Empresas Filiais")]
        public List<EmpresaFilial> EmpresasFiliais { get; set; } = new List<EmpresaFilial>();

    }
}