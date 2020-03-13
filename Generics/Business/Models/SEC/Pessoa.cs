namespace Generic.Business.Models
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
        public static List<Atividade> Atividades(this Sec.Models.Pessoa value)
        {
            return Engine.Cargo
                .Atividades()
                .Where(p => p.PessoaFisica == value.PessoaFisica)
                .ToList();
        }
    }
}

