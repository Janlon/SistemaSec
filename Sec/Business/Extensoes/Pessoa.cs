namespace Sec.Business.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;

    public static class PessoaExtensions
    {
        /// <summary>
        /// Por instanciamento, esta classe mantém adicionamente 
        /// a lista de atividades à ela permitida dada a sua 
        /// característica de pessoa física ou jurídica.
        /// </summary>
        public static List<Atividade> Atividades()
        {
            return Engine.Cargos.Atividades().ToList();
        }
    }
}

