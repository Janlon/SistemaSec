using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiteSec.Models
{
    public class ItemOrdemServico
    {
      
        [ScaffoldColumn(false)]
        public int Id { get; set; } = 0;

     
        [Display(Name = "Empresa", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Razão Social", Description = "Razão Social da Empresa")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public int EmpresaId { get; set; } = 0;
        public Empresa Empresa { get; set; }




        [Display(Name = "Ordem de serviço", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Ordem de serviço", Description = "Número da ordem de serviço")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public int OrdemDeServicoId { get; set; } = 0;
        public OrdemServico OrdemDeServico { get; set; }




        [Display(Name = "Setor", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Setor", Description = "Setor da Empresa")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public int SetorId { get; set; } = 0;
        public Setor Setor { get; set; }




        [Display(Name = "Equipamento", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Equipamento", Description = "Equipamento alvo da ordem de serviço")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public int EquipamentoId { get; set; } = 0;
        public Equipamento Equipamento { get; set; }





        [Display(Name = "Serviço", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Serviço", Description = "Serviço a realizar")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public int ServicoId { get;  set; }
        public Servico Servico { get; set; }





        [ScaffoldColumn(false)]
        public string ListaResultados { get; set; } = "";

    }
}