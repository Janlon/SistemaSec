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
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public int MatrizId { get; set; }

        /// <summary>
        /// Representa a empresa filial
        /// </summary>
        [ScaffoldColumn(false)]
        public int EmpresaId { get; set; }

        [ForeignKey("MatrizId")]
        public virtual EmpresaMatriz Matriz { get; set; }

        [ForeignKey("EmpresaId")]
        public virtual Empresa Empresa { get; set; }

        [NotMapped]
        public virtual List<int> FilialId { get; set; }

    }
}