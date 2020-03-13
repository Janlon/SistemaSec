using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EnderecosDasPessoas", Schema = "Sec")]
    public class EnderecoDaPessoa
    {
        [ScaffoldColumn(false), Key(), DatabaseGenerated(DatabaseGeneratedOption.None), Column(Order = 1)]
        public int PessoaId { get; set; }

        [ScaffoldColumn(false), Key(), DatabaseGenerated(DatabaseGeneratedOption.None), Column(Order = 2)]
        public int EnderecoId { get; set; }

        [ForeignKey("EnderecoId")]
        public virtual Endereco Endereco { get; set; }

        [ForeignKey("PessoaId")]
        public virtual Pessoa Pessoa { get; set; }
    }
}
