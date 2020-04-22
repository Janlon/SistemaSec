using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiteSec.Models
{
    public class ItemOrdemServico
    {

        #region propriedades de persitência

        /// <summary>
        /// Identificador do objeto ItemOrdemServico
        /// </summary>
        [ScaffoldColumn(false)]
        public int Id { get; set; } = 0;

        /// <summary>
        /// representa o Indetificador da Ordem de serviço que esta associado
        /// </summary>
        [ScaffoldColumn(false)]
        public int OrdemDeServicoId { get; set; } = 0;

        [ScaffoldColumn(false)]
        public int EquipamentoId { get;  set; }

        [ScaffoldColumn(false)]
        public int ServicoId { get;  set; }

        #endregion

        #region propriedades de tranferência

        [ScaffoldColumn(false)]
        public string ListaResultados { get; set; } = "";

        #endregion

        #region propriedade de vizualizacao

        /// <summary>
        /// É usado no template no objeto treeview
        /// não retorna valor
        /// </summary>
        public List<ItemSetor> ItensOrdemServico { get; set; }

        /// <summary>
        /// é usado na view ItemOrdemServico
        /// nao retorna valor
        /// </summary>
        [Display(Name = "Ordem de serviço", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Ordem de serviço")]
        public string OS { get; internal set; }

        /// <summary>
        /// é usado na view ItemOrdemServico
        /// nao retorna valor
        /// </summary>
        public string Equipamento { get; set; }

        /// <summary>
        /// é usado na view ItemOrdemServico
        /// nao retorna valor
        /// </summary>
        public string Setor { get; set; }

        /// <summary>
        /// é usado na view ItemOrdemServico
        /// nao retorna valor
        /// </summary>
        public string Serviço { get; internal set; }

        /// <summary>
        /// é usado na view ItemOrdemServico
        /// nao retorna valor
        /// </summary>
        public string Empresa { get; internal set; }

        #endregion
    }

    public class ItemSetor
    {
        /// <summary>
        /// representa o identificador do objeto Setor
        /// </summary>
        [ScaffoldColumn(false)]
        public int SetorId { get; set; }

        /// <summary>
        /// representa a descricao do objeto tipo de setor
        /// </summary>
        [ScaffoldColumn(false)]
        public string TipoSetorDescricao { get; set; }

        public List<ItemEquipamento> ItensEquipamentos { get; set; }
    }

    public class ItemEquipamento
    {
        /// <summary>
        /// Representa o identificador do objeto equipamento
        /// </summary>
        [ScaffoldColumn(false)]
        public int EquipamentoId { get; set; }

        /// <summary>
        /// Representa a descrição do objeto do tipo de equipamento
        /// </summary>
        [ScaffoldColumn(false)]
        public string TipoEquipamentoDescricao { get; set; }

        /// <summary>
        /// Lista de serviços
        /// </summary>
        public List<Servico> Servicos { get; set; }
    }

}