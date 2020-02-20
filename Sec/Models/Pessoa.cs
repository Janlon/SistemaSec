using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sec.Models
{

    /// <summary>
    /// Enumeração para booleanos. Sim = 1, não =0.
    /// </summary>
    public enum Resposta { Não = 0, Sim = 1 }


    /// <summary>
    /// Pessoa. Pode ser pessoa física ou jurídica. 
    /// Pode ser também um cliente, um funcionário ou um fornecedor.
    /// </summary>
    [Table("Pessoas", Schema = "dbo")]
    public class Pessoa : IEntidade
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Nome Completo", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Nome Completo")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,100}$", ErrorMessage = "Números e caracteres especiais não são permitidos no nome.")]
        [StringLength(100, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        public string Nome { get; set; }

        [Display(Name = "Ativo", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Está ativo?")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [DefaultValue(true)]
        public bool Ativo { get; set; } = true;

        [Display(Name = "Pessoa Fisica", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Informe se é pessoa física ou não")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [DefaultValue(true)]
        public Resposta PessoaFisica { get; set; } = Resposta.Sim;

        [ScaffoldColumn(false)]
        [NotMapped]
        public DateTime Cadastro { get; set; } = DateTime.Now;



        [Display(Name = "Documentos", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Documentos da pessoa")]
        public virtual List<Documento> Documentos { get; set; } = new List<Documento>();
    }
}