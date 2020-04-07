using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiteSec.Models
{
    public class Contato
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public bool Preferencial { get; set; }
    }
}