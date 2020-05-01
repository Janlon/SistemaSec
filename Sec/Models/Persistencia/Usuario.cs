namespace Sec.Models
{
    using Sec.IdentityGroup;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PessoasUsuarios", Schema ="Sec")]
    public class Usuario
    {
        [Key(),DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order =1), ScaffoldColumn(false)]
        public string UserId { get; set; }

        [Key(), DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 2), ScaffoldColumn(false)]
        public int PessoaId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public string Senha { get; set; }

        [ForeignKey("PessoaId")]
        public virtual Pessoa Pessoa { get; set; }

    }
}
