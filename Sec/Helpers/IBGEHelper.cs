namespace Sec.Helpers
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;

    /// <summary>
    /// Espaço de trabalho para componentes de consultas ao IBGE.
    /// </summary>
    namespace IBGE
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


        /// <summary>
        /// Auxiliar para consultas à API de localidades do IBGE.
        /// </summary>
        public class IbgeClient
        {
            /// <summary>
            /// Mantém a instância do cliente HTTP, para agilizar as consultas.
            /// </summary>
            private readonly HttpClient Cliente;

            /// <summary>
            /// Ao instanciar, prepara o cliente Http para que saiba que os dados chegarão comprimidos (<see cref="GZipWrapperStream"/>).
            /// </summary>
            public IbgeClient()
            {
                try
                {
                    HttpClientHandler handler = new HttpClientHandler()
                    {
                        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                    };
                    Cliente = new HttpClient(handler);
                }
                catch { }
            }

            #region Métodos públicos
            /// <summary>
            /// Retorna um <see cref="Endereco"/> com base no CEP informado.
            /// </summary>
            /// <param name="cep">CEP á ser localizado.</param>
            /// <returns>Objeto do tipo <see cref="Endereco"/>.</returns>
            public Endereco EnderecoDoCEP(string cep)
            {
                try
                {
                    string cp = cep.JustNumbers();
                    var url = $"https://viacep.com.br/ws/{cp}/json/";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<Endereco>(jsonResponse);
                    }
                }
                catch { return new Endereco(); }

            }


            /// <summary>
            /// Lista todas as regiões do Brasil.
            /// </summary>
            /// <returns>Lista de regiões do Brasil.</returns>
            public IEnumerable<Regiao> GetRegioesDoBrasil()
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/regioes";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Regiao>>(jsonResponse);
                    }
                }
                catch { return new List<Regiao>(); }
            }

            /// <summary>
            /// Lista todos os municípios da UF indicada (identificação/código, e não a sigla).
            /// </summary>
            /// <param name="ufId">Inteiro. Identificador da unidade federativa. Padrão 35, que é São Paulo.</param>
            /// <returns>Lista de municípios da UF.</returns>
            public IEnumerable<Municipio> GetMunicipiosDaUf(int ufId = 35)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{ufId}/municipios";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Municipio>>(jsonResponse);
                    }
                }
                catch { return new List<Municipio>(); }
            }

            /// <summary>
            /// Lista todos os municípios do país.
            /// </summary>
            /// <returns>Lista de municípios.</returns>
            public IEnumerable<Municipio> GetMunicipios()
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/municipios";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Municipio>>(jsonResponse);
                    }
                }
                catch { return new List<Municipio>(); }
            }

            /// <summary>
            /// Lista todas as unidades federativas.
            /// </summary>
            /// <returns>Lista de UFs.</returns>
            public IEnumerable<Uf> GetUFs()
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/estados";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Uf>>(jsonResponse);
                    }
                }
                catch { return new List<Uf>(); }
            }
            #endregion
        }
    }
}
