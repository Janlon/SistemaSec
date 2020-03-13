using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sec.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DocumentosDasPessoas", Schema = "Sec")]
    public class DocumentoDaPessoa
    {
        [ScaffoldColumn(false), Key(),DatabaseGenerated(DatabaseGeneratedOption.None), Column(Order = 1)]
        public int PessoaId { get; set; }

        [ScaffoldColumn(false), Key(), DatabaseGenerated(DatabaseGeneratedOption.None), Column(Order =2)]
        public int DocumentoId { get; set; }

        [ForeignKey("DocumentoId")]
        public virtual Documento Documento { get; set; }

        [ForeignKey("PessoaId")]
        public virtual Pessoa Pessoa { get; set; }
    }

}
