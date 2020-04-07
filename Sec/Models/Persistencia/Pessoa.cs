namespace Sec.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Pessoas", Schema = "Sec")]
    public class Pessoa : EntidadeBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Cadastro", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Cadastro", Description = "Data de cadastro")]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime")]
        public DateTime Cadastro { get; set; } = DateTime.Now;

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
        [Index(IsUnique = true)]
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
        public virtual string Atividade { get { return Generics.Helpers.Cryptis.Text.AESDecrypt(XAtividade, DataKey); } set { XAtividade = Generics.Helpers.Cryptis.Text.AESEncrypt(value, DataKey); } }

        [Display(Name = "Contatos")]
        public virtual List<Contato> Contatos { get; internal set; } = new List<Contato>();

        [Display(Name = "Imagens")]
        public virtual List<Imagem> Imagens { get; internal set; } = new List<Imagem>();

        [Display(Name = "Ordens de Serviço entregues")]
        public virtual List<EntregaDoItemDaOrdemDeServico> OrdensDeServicoEntregues { get; internal set; } = new List<EntregaDoItemDaOrdemDeServico>();

        [Display(Name = "Ordens de Serviço retiradas")]
        public virtual List<RetiradaDoItemDaOrdemDeServico> OrdensDeServicoRetiradas { get; internal set; } = new List<RetiradaDoItemDaOrdemDeServico>();

        [Display(Name = "Endereços das pessoas")]
        public virtual List<EnderecoDaPessoa> Enderecos { get; internal set; } = new List<EnderecoDaPessoa>();

        [Display(Name = "Documentos das pessoas")]
        public virtual List<Documento> Documentos { get; internal set; } = new List<Documento>();
    }
}