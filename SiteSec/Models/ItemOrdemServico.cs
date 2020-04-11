using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiteSec.Models
{
    public class ItemOrdemServico
    {

        public int Id { get;  set; }
        public string Equipamento { get;  set; }
        public int EquipamentoId { get;  set; }
        public string Servico { get;  set; }
        public int ServicoId { get;  set; }
        public int OrdemId { get;  set; }
        public string Sigla { get;  set; }
        public string OrdemDeServico { get;  set; }
    }
}