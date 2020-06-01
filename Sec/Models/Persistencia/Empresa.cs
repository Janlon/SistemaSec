namespace Sec.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Empresas", Schema = "Sec")]
    public class Empresa : EntidadeBase
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public int IdEndereco { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Cadastro", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Cadastro", Description = "Data de cadastro")]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime")]
        public DateTime Cadastro { get; set; } = DateTime.Now;

        [ScaffoldColumn(false)]
        [MaxLength(256)]
        [Column("RazaoSocial", TypeName = "VARCHAR")]
        public string XRazaoSocial { get; set; } = "";

        [ScaffoldColumn(false)]
        [MaxLength(256)]
        [Column("NomeFantasia", TypeName = "VARCHAR")]
        public string XNomeFantasia { get; set; } = "";

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
        [StringLength(100, ErrorMessage = "{0} deve ter entre {2} e {1} dígitos/caracteres.", MinimumLength = 5)]
        [Column(TypeName = "VARCHAR")]
        public virtual string RazaoSocial { get { return Generics.Helpers.Cryptis.Text.AESDecrypt(XRazaoSocial, DataKey); } set { XRazaoSocial = Generics.Helpers.Cryptis.Text.AESEncrypt(value, DataKey); } }

        [NotMapped()]
        [Required(AllowEmptyStrings = true)]
        [StringLength(100, ErrorMessage = "{0} deve ter entre {2} e {1} dígitos/caracteres.", MinimumLength = 2)]
        [Column(TypeName = "VARCHAR")]
        public virtual string NomeFantasia { get { return Generics.Helpers.Cryptis.Text.AESDecrypt(XNomeFantasia, DataKey); } set { XNomeFantasia = Generics.Helpers.Cryptis.Text.AESEncrypt(value, DataKey); } }

        [NotMapped()]
        [Required(AllowEmptyStrings = true)]
        [StringLength(100, ErrorMessage = "{0} deve ter entre {2} e {1} dígitos/caracteres.", MinimumLength = 2)]
        [Column(TypeName = "VARCHAR")]
        public virtual string Atividade { get { return Generics.Helpers.Cryptis.Text.AESDecrypt(XAtividade, DataKey); } set { XAtividade = Generics.Helpers.Cryptis.Text.AESEncrypt(value, DataKey); } }

        [Required(AllowEmptyStrings = true)]
        public bool EhMatriz { get; set; } = true;

        [Display(Name = "Setores")]
        public virtual List<Setor> Setores { get; internal set; } = new List<Setor>();

        [ForeignKey("IdEndereco")]
        public virtual Endereco Endereco { get; set; }
    }
}
