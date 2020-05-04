namespace Sec.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;
    using Sec.IdentityGroup;
    using System.Collections.Generic;
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

        [NotMapped]
        public string Senha { get; set; }

        [NotMapped]
        public string UserName { get; set; }

        [NotMapped]
        public List<IdentityRole> Permissoes { get; set; }

        [NotMapped]
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

    }
}
