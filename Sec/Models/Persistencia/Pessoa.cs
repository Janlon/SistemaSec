namespace Sec.Models
{
    using Sec.Abstratos;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Pessoa. Pode ser pessoa física ou jurídica. 
    /// Pode ser também um cliente, um funcionário ou um fornecedor. 
    /// É necessário que haja um campo exclusivo, pois esse cadastro não deve conter registros em duplicidade. 
    /// Para esse fim, vamos precisar manter um documento de identificação OU um endereço de email, por exemplo.
    /// Ocorre que endereços de e-mail, no caso de pessoa jurídica, muitas vezes é o mesmo que o e-mail do proprietário, 
    /// de forma que pode duplicar. Assim vamos escolher a "dupla" CPF/CNPJ, ambos documentos que identificam de forma 
    /// exclusiva, com a mesma finalidade (fiscal) e bem coerentes, portanto, com essa finalidade.
    /// </summary>
    [Table("Pessoas", Schema = "dbo")]
    public class Pessoa
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        
        [StringLength(20, MinimumLength = 11)]
        [Encrypted]
        public string Documento { get; set; }

        [StringLength(100, MinimumLength = 6)]
        [Encrypted]
        public string Nome { get; set; }

        [StringLength(100, MinimumLength = 2)]
        [Encrypted]
        public string Apelido { get; set; }

        [DataType(DataType.Date, ErrorMessage = "{0} deve ser uma data válida.")]
        public DateTime Nascimento { get; set; } = DateTime.Now.AddYears(-18);

        [ScaffoldColumn(false)]
        public DateTime Cadastro { get; set; } = DateTime.Now;

        [DefaultValue(true)]
        public bool PessoaFisica { get; set; } = true;

        [DefaultValue(true)]
        public bool Ativo { get; set; } = true;



        
        public virtual List<Documento> Documentos { get; set; } = new List<Documento>();
    }
}