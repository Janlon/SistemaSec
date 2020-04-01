namespace Sec.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Empresa. Pode ser Empresa física ou jurídica. 
    /// Pode ser também um cliente, um funcionário ou um fornecedor. 
    /// É necessário que haja um campo exclusivo, pois esse cadastro não deve conter registros em duplicidade. 
    /// Para esse fim, vamos precisar manter um documento de identificação OU um endereço de email, por exemplo.
    /// Ocorre que endereços de e-mail, no caso de Empresa jurídica, muitas vezes é o mesmo que o e-mail do proprietário, 
    /// de forma que pode duplicar. Assim vamos escolher a "dupla" CPF/CNPJ, ambos documentos que identificam de forma 
    /// exclusiva, com a mesma finalidade (fiscal) e bem coerentes, portanto, com essa finalidade.
    /// </summary>
    [Table("Empresas", Schema = "Sec")]
    public class Empresa : EntidadeBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public int IdEndereco { get; set; }

        [DataType(DataType.Date, ErrorMessage = "{0} deve ser uma data válida.")]
        public DateTime Nascimento { get; set; } = DateTime.Now.AddYears(-18);

        [ScaffoldColumn(false)]
        public DateTime Cadastro { get; set; } = DateTime.Now;

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

        [ScaffoldColumn(false)]
        [MaxLength(256)]
        [Column("CNPJ", TypeName = "VARCHAR")]
        public string XCNPJ { get; set; } = "";


        [CNPJ()]
        [NotMapped()]
        [StringLength(14, ErrorMessage = "{0} deve ter entre {2} e {1} dígitos/caracteres.", MinimumLength = 1)]
        [Column(TypeName = "VARCHAR")]
        public virtual string CNPJ { get { return Generics.Helpers.Cryptis.Text.AESDecrypt(XCNPJ, DataKey); } set { XCNPJ = Generics.Helpers.Cryptis.Text.AESEncrypt(value, DataKey); } }

        [NotMapped()]
        [StringLength(90, ErrorMessage = "{0} deve ter entre {2} e {1} dígitos/caracteres.", MinimumLength = 1)]
        [Column(TypeName = "VARCHAR")]
        public virtual string Email { get { return Generics.Helpers.Cryptis.Text.AESDecrypt(XEmail, DataKey); } set { XEmail = Generics.Helpers.Cryptis.Text.AESEncrypt(value, DataKey); } }

        [NotMapped()]
        [StringLength(100, ErrorMessage = "{0} deve ter entre {2} e {1} dígitos/caracteres.", MinimumLength = 5)]
        [Column(TypeName = "VARCHAR")]
        public virtual string Nome { get { return Generics.Helpers.Cryptis.Text.AESDecrypt(XNome, DataKey); } set { XNome = Generics.Helpers.Cryptis.Text.AESEncrypt(value, DataKey); } }

        [NotMapped()]
        [Required(AllowEmptyStrings = true)]
        [StringLength(100, ErrorMessage = "{0} deve ter entre {2} e {1} dígitos/caracteres.", MinimumLength = 2)]
        [Column(TypeName = "VARCHAR")]
        public virtual string Apelido { get { return Generics.Helpers.Cryptis.Text.AESDecrypt(XApelido, DataKey); } set { XApelido = Generics.Helpers.Cryptis.Text.AESEncrypt(value, DataKey); } }

        [NotMapped()]
        [Required(AllowEmptyStrings = true)]
        [StringLength(100, ErrorMessage = "{0} deve ter entre {2} e {1} dígitos/caracteres.", MinimumLength = 2)]
        [Column(TypeName = "VARCHAR")]
        public virtual string Atividade { get { return Generics.Helpers.Cryptis.Text.AESDecrypt(XAtividade, DataKey); } set { XAtividade = Generics.Helpers.Cryptis.Text.AESEncrypt(value, DataKey); } }

        [Display(Name = "Contatos")]
        public virtual List<Contato> Contatos { get; internal set; } = new List<Contato>();

        [Display(Name = "Imagens")]
        public virtual List<Imagem> Imagens { get; internal set; } = new List<Imagem>();

        [Display(Name = "Ordens de Serviço entregues")]
        public List<EntregaDoItemDaOrdemDeServico> OrdensDeServicoEntregues { get; internal set; } = new List<EntregaDoItemDaOrdemDeServico>();

        [Display(Name = "Ordens de Serviço retiradas")]
        public List<RetiradaDoItemDaOrdemDeServico> OrdensDeServicoRetiradas { get; internal set; } = new List<RetiradaDoItemDaOrdemDeServico>();

        [Display(Name = "Setores")]
        public virtual List<Setor> Setores { get; internal set; } = new List<Setor>();

        [ForeignKey("IdEndereco")]
        public virtual Endereco Endereco { get; set; }
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

        [ScaffoldColumn(false)]
        [MaxLength(256)]
        [Column("CPF", TypeName = "VARCHAR")]
        public string XCPF { get; set; } = "";


        [CPF()]
        [NotMapped()]
        [StringLength(11, ErrorMessage = "{0} deve ter entre {2} e {1} dígitos/caracteres.", MinimumLength = 1)]
        [Column(TypeName = "VARCHAR")]
        public virtual string CPF { get { return Generics.Helpers.Cryptis.Text.AESDecrypt(XCPF, DataKey); } set { XCPF = Generics.Helpers.Cryptis.Text.AESEncrypt(value, DataKey); } }

        [NotMapped()]
        [StringLength(90, ErrorMessage = "{0} deve ter entre {2} e {1} dígitos/caracteres.", MinimumLength = 1)]
        [Column(TypeName = "VARCHAR")]
        public virtual string Email { get { return Generics.Helpers.Cryptis.Text.AESDecrypt(XEmail, DataKey); } set { XEmail = Generics.Helpers.Cryptis.Text.AESEncrypt(value, DataKey); } }

        [NotMapped()]
        [StringLength(100, ErrorMessage = "{0} deve ter entre {2} e {1} dígitos/caracteres.", MinimumLength = 5)]
        [Column(TypeName = "VARCHAR")]
        public virtual string Nome { get { return Generics.Helpers.Cryptis.Text.AESDecrypt(XNome, DataKey); } set { XNome = Generics.Helpers.Cryptis.Text.AESEncrypt(value, DataKey); } }

        [NotMapped()]
        [Required(AllowEmptyStrings = true)]
        [StringLength(100, ErrorMessage = "{0} deve ter entre {2} e {1} dígitos/caracteres.", MinimumLength = 2)]
        [Column(TypeName = "VARCHAR")]
        public virtual string Apelido { get { return Generics.Helpers.Cryptis.Text.AESDecrypt(XApelido, DataKey); } set { XApelido = Generics.Helpers.Cryptis.Text.AESEncrypt(value, DataKey); } }

        [NotMapped()]
        [Required(AllowEmptyStrings = true)]
        [StringLength(100, ErrorMessage = "{0} deve ter entre {2} e {1} dígitos/caracteres.", MinimumLength = 2)]
        [Column(TypeName = "VARCHAR")]
        public virtual string Atividade { get { return Generics.Helpers.Cryptis.Text.AESDecrypt(XAtividade, DataKey); } set { XAtividade = Generics.Helpers.Cryptis.Text.AESEncrypt(value, DataKey); } }

        [Display(Name = "Contatos")]
        public virtual List<Contato> Contatos { get; internal set; } = new List<Contato>();

        [Display(Name = "Imagens")]
        public virtual List<Imagem> Imagens { get; internal set; } = new List<Imagem>();

        [Display(Name = "Ordens de Serviço entregues")]
        public List<EntregaDoItemDaOrdemDeServico> OrdensDeServicoEntregues { get; internal set; } = new List<EntregaDoItemDaOrdemDeServico>();

        [Display(Name = "Ordens de Serviço retiradas")]
        public List<RetiradaDoItemDaOrdemDeServico> OrdensDeServicoRetiradas { get; internal set; } = new List<RetiradaDoItemDaOrdemDeServico>();

        //[Display(Name = "Setores")]
        //public virtual List<Setor> Setores { get; internal set; } = new List<Setor>();

        public virtual List<EnderecoDaPessoa> Enderecos { get; set; } = new List<EnderecoDaPessoa>();
    }
}