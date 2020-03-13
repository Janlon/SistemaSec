namespace Generic.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// representa um endereço para respsota do serviço de CEP.
    /// </summary>
    [Table("Enderecos", Schema = "Sec")]
    public class Endereco
    {
        public Endereco() { }
        internal Endereco(Helpers.IBGE.Geo.Endereco endereco)
        {
            Bairro = endereco.Bairro;
            CEP = endereco.CEP;
            Complemento = endereco.Complemento;
            Localidade = endereco.Localidade;
            Logradouro = endereco.Logradouro;
            UF = endereco.UF;
        }

        [ScaffoldColumn(false)]
        [Key(), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Código postal")]
        [StringLength(8, ErrorMessage = "{0} deve ter {1} dígitos numéricos.", MinimumLength = 8)]
        [Column(TypeName = "VARCHAR")]
        public string CEP { get; set; } = "";

        [Display(Name = "Logradouro")]
        [StringLength(50, ErrorMessage = "{0} deve ter entre {2} e {1} dígitos/caracteres.", MinimumLength = 1)]
        [Column(TypeName = "VARCHAR")]
        public string Logradouro { get; set; } = "";

        [Display(Name = "Complementos")]
        [MaxLength(50, ErrorMessage = "{0} deve ter até {1} dígitos/caracteres.")]
        [Column(TypeName = "VARCHAR")]
        public string Complemento { get; set; } = "";

        [Display(Name = "")]
        [StringLength(50, ErrorMessage = "{0} deve ter entre {2} e {1} dígitos/caracteres.", MinimumLength = 1)]
        [Column(TypeName = "VARCHAR")]
        public string Bairro { get; set; } = "";

        [Display(Name = "Localidade")]
        [StringLength(50, ErrorMessage = "{0} deve ter entre {2} e {1} dígitos/caracteres.", MinimumLength = 1)]
        [Column(TypeName = "VARCHAR")]
        public string Localidade { get; set; } = "";

        [Display(Name = "Unidade Federativa")]
        [StringLength(2, ErrorMessage = "{0} deve ter {1} dígitos numéricos.", MinimumLength = 2)]
        [Column(TypeName = "VARCHAR")]
        public string UF { get; set; } = "";
    }
}
