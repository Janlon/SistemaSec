using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sec.Models
{
    [Table("Contatos", Schema = "Sec")]
    public class Contato
    {
        /// <summary>
        /// Identificação do registro.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public int PessoaId { get; set; }

        [Display(Name = "Descrição", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Descrição", Description = "Número do telefone, endereço do whatsApp etc.")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(50, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
        public string Descricao { get; set; }

        [Display(Name = "Tipo de Contato", AutoGenerateField = true, AutoGenerateFilter = true, Prompt ="Tipo de Contato", Description = "Telefone, WhatsApp...")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(50, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
        public string Tipo { get; set; }

        [Display(Name = "Preferencial", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Preferencial", Description = "É o meio de contato preferencal?")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [DefaultValue(true)]
        public bool Preferencial { get; set; } = true;


        //    public Contato() { Pessoa = new HashSet<Pessoa>(); }
        //    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //    [ScaffoldColumn(false)]
        //    public int Id { get; set; }
        //    [Display(Name = "Tipo de Contato", AutoGenerateField = true, AutoGenerateFilter = true, Prompt ="Tipo de Contato", Description = "Telefone, WhatsApp...")]
        //    [Required(ErrorMessage = "{0} é obrigatório.")]
        //    [StringLength(50, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
        //    public string Descricao { get; set; }

        [ForeignKey("PessoaId")]
        public virtual Pessoa Pessoa { get; set; }
    }
}