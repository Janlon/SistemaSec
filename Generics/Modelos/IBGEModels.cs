namespace Generics.Helpers.IBGE
{
    using Newtonsoft.Json;



    namespace CBO
    {
        /// <summary>
        /// Listagem de ocupações e sinônimos da CBO2002.
        /// </summary>
        public class Cargo
        {
            [JsonProperty("id")]
            public string id { get; set; }

            [JsonProperty("descricao")]
            public string descricao { get; set; }
        }
    }



    namespace CNAE
    {
        /// <summary>
        /// Subclasse CNAE.
        /// </summary>
        public class subclasse
        {
            [JsonProperty("id")]
            public string id { get; set; }

            [JsonProperty("descricao")]
            public string descricao { get; set; }

            [JsonProperty("classe")]
            public classe classe { get; set; }

            [JsonProperty("observacoes")]
            public string[] observacoes { get; set; }
        }


        /// <summary>
        /// Classe CNAE.
        /// </summary>
        public class classe
        {
            [JsonProperty("id")]
            public string id { get; set; }

            [JsonProperty("descricao")]
            public string descricao { get; set; }

            [JsonProperty("grupo")]
            public grupo grupo { get; set; }

            [JsonProperty("observacoes")]
            public string[] observacoes { get; set; }
        }


        /// <summary>
        /// Grupo CNAE.
        /// </summary>
        public class grupo
        {
            [JsonProperty("id")]
            public string id { get; set; }

            [JsonProperty("descricao")]
            public string descricao { get; set; }

            [JsonProperty("divisao")]
            public divisao divisao { get; set; }
        }


        /// <summary>
        /// Divisão CNAE.
        /// </summary>
        public class divisao
        {
            [JsonProperty("id")]
            public string id { get; set; }

            [JsonProperty("descricao")]
            public string descricao { get; set; }

            [JsonProperty("secao")]
            public secao secao { get; set; }
        }


        /// <summary>
        /// Seção CNAE.
        /// </summary>
        public class secao
        {
            [JsonProperty("id")]
            public string id { get; set; }

            [JsonProperty("descricao")]
            public string descricao { get; set; }
        }
    }



    namespace Geo
    {
        /// <summary>
        /// representa um endereço para respsota do serviço de CEP.
        /// </summary>
        public class Endereco
        {
            [JsonProperty("cep")]
            public string CEP { get; set; } = "";

            [JsonProperty("logradouro")]
            public string Logradouro { get; set; } = "";

            [JsonProperty("complemento")]
            public string Complemento { get; set; } = "";

            [JsonProperty("bairro")]
            public string Bairro { get; set; } = "";

            [JsonProperty("localidade")]
            public string Localidade { get; set; } = "";

            [JsonProperty("uf")]
            public string UF { get; set; } = "";

            [JsonProperty("unidade")]
            public string Unidade { get; set; } = "";

            [JsonProperty("idibge")]
            public string Ibge { get; set; } = "";

            [JsonProperty("gia")]
            public string Gia { get; set; } = "";
        }

        /// <summary>
        /// Representa uma mesorregião administrativa do Brasil.
        /// </summary>
        public class Mesorregiao
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("nome")]
            public string Nome { get; set; }

            [JsonProperty("uf")]
            public Uf Uf { get; set; }
        }

        /// <summary>
        /// Representa uma microrregião administrativa.
        /// </summary>
        public class Microrregiao
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("nome")]
            public string Nome { get; set; }

            [JsonProperty("mesorregiao")]
            public Mesorregiao Mesorregiao { get; set; }
        }

        /// <summary>
        /// Representa um distrito.
        /// </summary>
        public class Distrito
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("nome")]
            public string Nome { get; set; }

            [JsonProperty("municipio")]
            public Municipio Municipio { get; set; }
        }

        /// <summary>
        /// Representa um município.
        /// </summary>
        public class Municipio
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("nome")]
            public string Nome { get; set; }

            [JsonProperty("microrregiao")]
            public Microrregiao Microrregiao { get; set; }
        }

        /// <summary>
        /// Representa uma região administrativa do Brasil.
        /// </summary>
        public class Regiao
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("sigla")]
            public string Sigla { get; set; }

            [JsonProperty("nome")]
            public string Nome { get; set; }
        }

        /// <summary>
        /// Representa um subdistrito.
        /// </summary>
        public class Subdistrito
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("nome")]
            public string Nome { get; set; }

            [JsonProperty("distrito")]
            public Distrito Distrito { get; set; }
        }

        /// <summary>
        /// Representa uma UF.
        /// </summary>
        public class Uf
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("sigla")]
            public string Sigla { get; set; }

            [JsonProperty("nome")]
            public string Nome { get; set; }

            [JsonProperty("regiao")]
            public Regiao Regiao { get; set; }
        }
    }
}
