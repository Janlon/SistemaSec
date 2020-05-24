using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteSec.Models
{
    public class Geo
    {
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
        public string codigo { get; internal set; }
    }
}