namespace Sec.Models
{
    using Generics;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public interface IEntidadeBase
    {
        string DataKey { get; set; }
        string LifeKey { get; set; }
    }

    public abstract class EntidadeBase : IEntidadeBase
    {
        [ScaffoldColumn(false)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(256)]
        public string DataKey { get; set; } = SuperKey.Create();

        [ScaffoldColumn(false)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(256)]
        public string LifeKey { get; set; } = SuperKey.Create();
    }

    /// <summary>
    /// Pessoa. Pode ser pessoa física ou jurídica. 
    /// Pode ser também um cliente, um funcionário ou um fornecedor. 
    /// É necessário que haja um campo exclusivo, pois esse cadastro não deve conter registros em duplicidade. 
    /// Para esse fim, vamos precisar manter um documento de identificação OU um endereço de email, por exemplo.
    /// Ocorre que endereços de e-mail, no caso de pessoa jurídica, muitas vezes é o mesmo que o e-mail do proprietário, 
    /// de forma que pode duplicar. Assim vamos escolher a "dupla" CPF/CNPJ, ambos documentos que identificam de forma 
    /// exclusiva, com a mesma finalidade (fiscal) e bem coerentes, portanto, com essa finalidade.
    /// </summary>
    [Table("Pessoas", Schema = "Sec")]
    public class Pessoa : EntidadeBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [DataType(DataType.Date, ErrorMessage = "{0} deve ser uma data válida.")]
        public DateTime Nascimento { get; set; } = DateTime.Now.AddYears(-18);

        [ScaffoldColumn(false)]
        public DateTime Cadastro { get; set; } = DateTime.Now;

        [DefaultValue(true)]
        public bool PessoaFisica { get; set; } = true;

        [DefaultValue(true)]
        public bool Ativo { get; set; } = true;

        [ScaffoldColumn(false)]
        [MaxLength(256)]
        [Column("Email", TypeName = "VARCHAR")]
        public string XEmail { get; set; } = "";

        [ScaffoldColumn(false)]
        [MaxLength(256)]
        [Column("Nome", TypeName = "VARCHAR")]
        public string XNome { get; set; } = "";

        [ScaffoldColumn(false)]
        [MaxLength(256)]
        [Column("Apelido", TypeName = "VARCHAR")]
        public string XApelido { get; set; } = "";

        [ScaffoldColumn(false)]
        [MaxLength(256)]
        [Column("Atividade", TypeName = "VARCHAR")]
        public string XAtividade { get; set; } = "";





        [NotMapped()]
        [StringLength(90, ErrorMessage = "{0} deve ter entre {2} e {1} dígitos/caracteres.", MinimumLength = 1)]
        [Column(TypeName = "VARCHAR")]
        public virtual string Email { get { return Generics.Cryptis.Text.AESDecrypt(XEmail, DataKey); } set { XEmail = Generics.Cryptis.Text.AESEncrypt(value, DataKey); } }

        [NotMapped()]
        [StringLength(100, ErrorMessage = "{0} deve ter entre {2} e {1} dígitos/caracteres.", MinimumLength = 5)]
        [Column(TypeName = "VARCHAR")]
        public virtual string Nome { get { return Generics.Cryptis.Text.AESDecrypt(XNome, DataKey); } set {XNome = Generics.Cryptis.Text.AESEncrypt(value, DataKey); } }

        [NotMapped()]
        [Required(AllowEmptyStrings =true)]
        [StringLength(100, ErrorMessage = "{0} deve ter entre {2} e {1} dígitos/caracteres.", MinimumLength = 2)]
        [Column(TypeName = "VARCHAR")]
        public virtual string Apelido { get { return Generics.Cryptis.Text.AESDecrypt(XApelido, DataKey); } set { XApelido = Generics.Cryptis.Text.AESEncrypt(value, DataKey); } }

        [NotMapped()]
        [Required(AllowEmptyStrings = true)]
        [StringLength(100, ErrorMessage = "{0} deve ter entre {2} e {1} dígitos/caracteres.", MinimumLength = 2)]
        [Column(TypeName = "VARCHAR")]
        public virtual string Atividade { get { return Generics.Cryptis.Text.AESDecrypt(XAtividade, DataKey); } set { XAtividade = Generics.Cryptis.Text.AESEncrypt(value, DataKey); } }

        [Display(Name ="Contatos")]
        public virtual List<Contato> Contatos { get; internal set; } = new List<Contato>();

        [Display(Name = "Imagens")]
        public virtual List<Imagem> Imagens { get; internal set; } = new List<Imagem>();
    }
}