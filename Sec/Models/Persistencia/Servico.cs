namespace Sec.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Servicos", Schema = "Sec")]
    public class Servico
    {
        /// <summary>
        /// Identificação do registro.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage ="{0} é requerido")]
        [StringLength(80, ErrorMessage ="{0} deve ter entre {2} e {1} caracteres", MinimumLength = 1)]
        [Column(TypeName ="varchar")]
        public string Descricao { get; set; }

    }
}