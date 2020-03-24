using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sec.Business.Models
{

    public enum CrudAction
    {
        /// <summary>
        /// Nenhuma ação.
        /// </summary>
        [Display(Name="Nenhuma ação")]
        None,
        /// <summary>
        /// Ação de inclusão.
        /// </summary>
        [Display(Name = "Inclusão de registros")]
        Insert,
        /// <summary>
        /// Ação de alteração dos dados.
        /// </summary>
        [Display(Name = "Atualização de registros")]
        Update,
        /// <summary>
        /// Ação de listagem completa.
        /// </summary>
        [Display(Name = "Lista completa de registros")]
        List,
        /// <summary>
        /// Ação de seleção de um registro.
        /// </summary>
        [Display(Name = "Seleção de um único registro")]
        Select,
        /// <summary>
        /// Ação de exclusão física (permanente) de um registro.
        /// </summary>
        [Display(Name = "Exclusão física de um registro")]
        Delete,
        /// <summary>
        /// Ação de exclusão lógica (reversível) de um registro.
        /// </summary>
        [Display(Name = "Exclusão lógica de um registro")]
        Deactivate,
        /// <summary>
        /// Ação de reversão de exclusão lógica de um registro.
        /// </summary>
        [Display(Name = "Restauração lógica de um registro")]
        Reactivate,
        /// <summary>
        /// Ação de filtragem de registros.
        /// </summary>
        [Display(Name = "Lista filtrada de registros")]
        Filter,
        /// <summary>
        /// Ações relacionadas à DML (CREATE).
        /// </summary>
        [Display(Name = "Criação de uma entidade de persistência (tabela, indice, visualização, procedimento etc)")]
        Create,
        /// <summary>
        /// Ações relacionadas à DML (DROP).
        /// </summary>
        [Display(Name = "Exclusão de uma entidade de persistência (tabela, indice, visualização, procedimento etc)")]
        Drop,
        /// <summary>
        /// Ações relacionadas à DIL (Importação/exportação).
        /// </summary>
        [Display(Name = "Exportação dos dados de uma consulta")]
        Export,
        /// <summary>
        /// Ações relacionadas à DIL (Importação/exportação).
        /// </summary>
        [Display(Name = "Exportação de todos os dados de um catálogo")]
        FullExport,
        /// <summary>
        /// Ações relacionadas à DIL (Importação/exportação).
        /// </summary>
        [Display(Name = "Importação dos dados de um arquivo")]
        Import,
        /// <summary>
        /// Ações relacionadas à DIL (Importação/exportação).
        /// </summary>
        [Display(Name = "Geração de uma cópia de segurança")]
        Backup
    }

    public class ActionResult<T> where T : class
    {
        public DateTime Creation { get; set; } = DateTime.Now;
        public CrudAction Action { get; set; }
        public TimeSpan Delay { get; set; }
        public bool Sucess { get { return (Errors.Count == 0); } }
        public int Affected { get; set; }
        public dynamic Result { get; set; }
        Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
    }



    public class CEP
    {
        public string Codigo { get; set; }
    }

    public class FiltroViewModel
    {
        public string Descricao { get; set; } = "";
        public StringFilter Filtro { get; set; } = StringFilter.Contains;
        public int StartAt { get; set; } = -1;
        public int PageSize { get; set; } = -1;
    }
    public class FiltroDeColunaViewModel
    {
        public string Descricao { get; set; } = "";
        public StringFilter Filtro { get; set; } = StringFilter.Contains;
    }
    public class FiltroDePaginacaoViewModel
    {
        public int StartAt { get; set; } = -1;
        public int PageSize { get; set; } = -1;
    }
}
