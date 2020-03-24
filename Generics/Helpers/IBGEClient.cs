namespace Generics.Helpers
{
    using Generics.Extensoes;
    using Generics.Helpers.Errors;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Espaço de trabalho para componentes de consultas online ao IBGE.
    /// </summary>
    namespace IBGE
    {
        /// <summary>
        /// Auxiliar para consultas à API de localidades do IBGE.
        /// </summary>
        public class IbgeClient : IDisposable
        {
            #region Manutenção
            /// <summary>
            /// Mantém a instância do cliente HTTP, para agilizar as consultas.
            /// </summary>
            private readonly HttpClient Cliente;

            /// <summary>
            /// Mantém o status de chamadas à destruição.
            /// </summary>
            private bool disposedValue;
            #endregion

            #region Instância
            private void CleanUp() { try { if (Cliente != null) Cliente.Dispose(); } catch { } }
            /// <summary>
            /// Ao instanciar, prepara o cliente Http para que saiba que os dados chegarão comprimidos (<see cref="GZipWrapperStream"/>).
            /// </summary>
            public IbgeClient()
            {
                disposedValue = false;
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
            protected virtual void Dispose(bool disposing) { if (!disposedValue) { if (disposing) { CleanUp(); } disposedValue = true; } }
            ~IbgeClient() { Dispose(false); }
            public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }
            #endregion

            #region Métodos públicos


            #region DISTRITOS
            /// <summary>
            /// Obtém o conjunto de distritos do Brasil.
            /// </summary>
            /// <returns>Lista do tipo <see cref="Geo.Distrito"/></returns>
            public List<Geo.Distrito> Distritos()
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/distritos";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Distrito>>(jsonResponse);
                    }
                }
                catch { return new List<Geo.Distrito>(); }
            }

            /// <summary>
            /// Obtém oS dados de distritos do Brasil a partir de seus respectivos identificadores
            /// </summary>
            /// <param name="distritoId">Identificvador do distrito</param>
            /// <returns>Lista do tipo <see cref="Geo.Distrito"/></returns>
            public Geo.Distrito DistritoPorId(int distritoId)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/distritos/{distritoId}/";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<Geo.Distrito>(jsonResponse);
                    }
                }
                catch { return new Geo.Distrito(); }
            }

            /// <summary>
            /// Obtém o conjunto de distritos de uma unidade federativa a partir de seus respectivos identificadores
            /// </summary>
            /// <param name="ufId">Identificador da Unidade Federativa.</param>
            /// <returns>Lista do tipo <see cref="Geo.Distrito"/></returns>
            public List<Geo.Distrito> DistritoPorUf(int ufId)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{ufId}/distritos";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Distrito>>(jsonResponse);
                    }
                }
                catch { return new List<Geo.Distrito>(); }
            }

            /// <summary>
            /// Obtém o conjunto de distritos de uma mesorregião a partir de seus respectivos identificadores
            /// </summary>
            /// <param name="mesorregiaoId">Identificador da mesorregião</param>
            /// <returns>Lista do tipo <see cref="Geo.Distrito"/></returns>
            public List<Geo.Distrito> DistritosPorMesorregiao(int mesorregiaoId)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/mesorregioes/{mesorregiaoId}/distritos";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Distrito>>(jsonResponse);
                    }
                }
                catch { return new List<Geo.Distrito>(); }
            }

            /// <summary>
            /// Obtém o conjunto de distritos de uma microrregião a partir de seus respectivos identificadores
            /// </summary>
            /// <param name="mesorregiaoId">Identificador da microrregião</param>
            /// <returns>Lista do tipo <see cref="Geo.Distrito"/></returns>
            public List<Geo.Distrito> DistritosPorMicrorregiao(int microrregiaoId)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/microrregioes/{microrregiaoId}/distritos";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Distrito>>(jsonResponse);
                    }
                }
                catch { return new List<Geo.Distrito>(); }
            }

            /// <summary>
            /// Obtém o conjunto de distritos de um município a partir de seus respectivos identificadores
            /// </summary>
            /// <param name="mesorregiaoId">Identificador do município</param>
            /// <returns>Lista do tipo <see cref="Geo.Distrito"/></returns>
            public List<Geo.Distrito> DistritosPorMunicipio(int municipioId)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/municipios/{municipioId}/distritos";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Distrito>>(jsonResponse);
                    }
                }
                catch { return new List<Geo.Distrito>(); }
            }

            /// <summary>
            /// Obtém o conjunto de distritos de uma região a partir de seus respectivos identificadores
            /// </summary>
            /// <param name="mesorregiaoId">Identificador da região</param>
            /// <returns>Lista do tipo <see cref="Geo.Distrito"/></returns>
            public List<Geo.Distrito> DistritosPorRegiao(int regiaoId)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/regioes/{regiaoId}/distritos";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Distrito>>(jsonResponse);
                    }
                }
                catch { return new List<Geo.Distrito>(); }
            }
            #endregion
            #region  MESORREGIÕES
            /// <summary>
            /// Obtém o conjunto de mesorregiões do Brasil
            /// </summary>
            /// <returns>Lista do tipo <see cref="Geo.Mesorregiao"/></returns>
            public List<Geo.Mesorregiao> Mesorregioes()
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/mesorregioes";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Mesorregiao>>(jsonResponse);
                    }
                }
                catch { return new List<Geo.Mesorregiao>(); }
            }

            /// <summary>
            /// Obtém o conjunto de mesorregiões do Brasil em uma unidade federativa.
            /// </summary>
            /// <param name="ufId">Identificador da unidade federativa</param>
            /// <returns>Lista do tipo <see cref="Geo.Mesorregiao"/></returns>
            public List<Geo.Mesorregiao> MesorregioesPorUf(int ufId)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{ufId}/mesorregioes";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Mesorregiao>>(jsonResponse);
                    }
                }
                catch { return new List<Geo.Mesorregiao>(); }
            }

            /// <summary>
            /// Obtém o conjunto de dados de uma mesorregião do Brasil por seu respecitivo identificador.
            /// </summary>
            /// <param name="mesorregiaoId">Identificador da mesorregiao</param>
            /// <returns>Lista do tipo <see cref="Geo.Mesorregiao"/></returns>
            public Geo.Mesorregiao MesorregioesPorId(int mesorregiaoId)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/mesorregioes/{mesorregiaoId}";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<Geo.Mesorregiao>(jsonResponse);
                    }
                }
                catch { return new Geo.Mesorregiao(); }
            }

            /// <summary>
            /// Obtém o conjunto de mesorregiões do Brasil em uma região.
            /// </summary>
            /// <param name="regiaoId">Identificador da mesorregiao</param>
            /// <returns>Lista do tipo <see cref="Geo.Mesorregiao"/></returns>
            public List<Geo.Mesorregiao> MesorregioesPorRegiao(int regiaoId)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/regioes/{regiaoId}/mesorregioes";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Mesorregiao>>(jsonResponse);
                    }
                }
                catch { return new List<Geo.Mesorregiao>(); }
            }
            #endregion
            #region MICRORREGIÕES
            /// <summary>
            /// Obtém o conjunto de microrregiões do Brasil.
            /// </summary>
            /// <returns>Lista do tipo <see cref="Geo.Microrregiao"/></returns>
            public List<Geo.Microrregiao> Microrregioes()
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/microrregioes";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Microrregiao>>(jsonResponse);
                    }
                }
                catch { return new List<Geo.Microrregiao>(); }
            }

            /// <summary>
            /// Obtém o conjunto de microrregiões do Brasil em uma unidade federativa.
            /// </summary>
            /// <param name="ufId">Identificador da unidade federativa</param>
            /// <returns>Lista do tipo <see cref="Geo.Microrregiao"/></returns>
            public List<Geo.Microrregiao> MicrorregioesPorUf(int ufId)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/regioes/{ufId}/mesorregioes";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Microrregiao>>(jsonResponse);
                    }
                }
                catch { return new List<Geo.Microrregiao>(); }
            }

            /// <summary>
            /// Obtém o conjunto de microrregiões do Brasil em uma mesorregião.
            /// </summary>
            /// <param name="mesorregiaoId">Identificador da mesorregião</param>
            /// <returns>Lista do tipo <see cref="Geo.Microrregiao"/></returns>
            public List<Geo.Microrregiao> MicrorregioesPorMesorregiao(int mesorregiaoId)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/mesorregioes/{mesorregiaoId}/microrregioes";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Microrregiao>>(jsonResponse);
                    }
                }
                catch { return new List<Geo.Microrregiao>(); }
            }

            /// <summary>
            /// Obtém o conjunto de microrregiões do Brasil em uma região.
            /// </summary>
            /// <param name="regiaoId">Identificador da região</param>
            /// <returns>Lista do tipo <see cref="Geo.Microrregiao"/></returns>
            public List<Geo.Microrregiao> MicrorregioesPorRegiao(int regiaoId)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/regioes/{regiaoId}/microrregioes";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Microrregiao>>(jsonResponse);
                    }
                }
                catch { return new List<Geo.Microrregiao>(); }
            }

            /// <summary>
            /// Obtém os dados de uma microrregião do Brasil.
            /// </summary>
            /// <param name="microrregiaoId">Identificador da microrregião</param>
            /// <returns>Lista do tipo <see cref="Geo.Microrregiao"/></returns>
            public Geo.Microrregiao Microrregioes(int microrregiaoId)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/microrregioes/{microrregiaoId}";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<Geo.Microrregiao>(jsonResponse);
                    }
                }
                catch { return new Geo.Microrregiao(); }
            }
            #endregion
            #region MUNICÍPIOS (Localidades, na verdade)

            /// <summary>
            /// Lista todos os municípios da UF indicada (identificação/código, e não a sigla).
            /// </summary>
            /// <param name="ufId">Inteiro. Identificador da unidade federativa. Padrão 35, que é São Paulo.</param>
            /// <returns>Lista de municípios da UF.</returns>
            public IEnumerable<Geo.Municipio> GetMunicipiosDaUf(int ufId = 35)
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
                        return JsonConvert.DeserializeObject<List<Geo.Municipio>>(jsonResponse);
                    }
                }
                catch { return new List<Geo.Municipio>(); }
            }

            /// <summary>
            /// Lista todos os municípios do país.
            /// </summary>
            /// <returns>Lista de municípios.</returns>
            public IEnumerable<Geo.Municipio> GetMunicipios()
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
                        return JsonConvert.DeserializeObject<List<Geo.Municipio>>(jsonResponse);
                    }
                }
                catch { return new List<Geo.Municipio>(); }
            }

            /// <summary>
            /// Obtém o conjunto de municípios do Brasil
            /// </summary>
            /// <returns>Lista do tipo <see cref="Geo.Municipio"/></returns>
            public List<Geo.Municipio> Municipios()
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
                        return JsonConvert.DeserializeObject<List<Geo.Municipio>>(jsonResponse);
                    }
                }
                catch (Exception ex) { ex.Log(); return new List<Geo.Municipio>(); }
            }

            /// <summary>
            /// Obtém o conjunto de municípios do Brasil em uma mesorregião.
            /// </summary>
            /// <param name="mesorregiaoId">Identificador da mesorregiao.</param>
            /// <returns>Lista do tipo <see cref="Geo.Municipio"/></returns>
            public List<Geo.Municipio> MunicipiosPorMesorregiao(int mesorregiaoId)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/mesorregioes/{mesorregiaoId}/municipios";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Municipio>>(jsonResponse);
                    }
                }
                catch (Exception ex) { ex.Log(); return new List<Geo.Municipio>(); }
            }

            /// <summary>
            /// Obtém o conjunto de municípios do Brasil em umamicrorregião.
            /// </summary>
            /// <param name="microrregiaoId">Identificador da microrregiao.</param>
            /// <returns>Lista do tipo <see cref="Geo.Municipio"/></returns>
            public List<Geo.Municipio> MunicipiosPorMicrorregiao(int microrregiaoId)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/microrregioes/{microrregiaoId}/municipios";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Municipio>>(jsonResponse);
                    }
                }
                catch (Exception ex) { ex.Log(); return new List<Geo.Municipio>(); }
            }

            /// <summary>
            /// Obtém os dados de municípios do Brasil por seu respectivo identificador.
            /// </summary>
            /// <param name="Id">Identificador do município.</param>
            /// <returns>Objeto do tipo <see cref="Geo.Municipio"/></returns>
            public Geo.Municipio MunicipiosPorId(int Id)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/municipios/{Id}";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<Geo.Municipio>(jsonResponse);
                    }
                }
                catch (Exception ex) { ex.Log(); return new Geo.Municipio(); }
            }

            /// <summary>
            /// Obtém o conjunto de municípios do Brasil em uma região.
            /// </summary>
            /// <param name="regiaoId">Identificador da região.</param>
            /// <returns>Lista do tipo <see cref="Geo.Municipio"/></returns>
            public List<Geo.Municipio> MunicipiosPorRegiao(int regiaoId)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/regioes/{regiaoId}/municipios";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Municipio>>(jsonResponse);
                    }
                }
                catch (Exception ex) { ex.Log(); return new List<Geo.Municipio>(); }
            }
            #endregion
            #region REGIÕES

            /// <summary>
            /// Lista todas as regiões do Brasil.
            /// </summary>
            /// <returns>Lista de regiões do Brasil.</returns>
            public IEnumerable<Geo.Regiao> GetRegioesDoBrasil()
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
                        return JsonConvert.DeserializeObject<List<Geo.Regiao>>(jsonResponse);
                    }
                }
                catch { return new List<Geo.Regiao>(); }
            }

            /// <summary>
            /// Obtém o conjunto de regiões do Brasil
            /// </summary>
            /// <returns>Lista do tipo <see cref="Geo.Regiao"/></returns>
            public List<Geo.Regiao> Regioes()
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
                        return JsonConvert.DeserializeObject<List<Geo.Regiao>>(jsonResponse);
                    }
                }
                catch (Exception ex) { ex.Log(); return new List<Geo.Regiao>(); }
            }

            /// <summary>
            /// Obtém os dados de uma regiões do Brasil por seu identificador
            /// </summary>
            /// <param name="id">Identificado da região.</param>
            /// <returns>Objeto do tipo <see cref="Geo.Regiao"/></returns>
            public Geo.Regiao Regioes(int id)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/regioes/{id}";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<Geo.Regiao>(jsonResponse);
                    }
                }
                catch (Exception ex) { ex.Log(); return new Geo.Regiao(); }
            }
            #endregion
            #region UFS
            /// <summary>
            /// Obtém o conjunto de unidades federativas do Brasil
            /// </summary>
            /// <returns>Lista do tipo <see cref="Geo.Uf"/></returns>
            /// <summary>
            /// Lista todas as unidades federativas.
            /// </summary>
            /// <returns>Lista de UFs.</returns>
            public IEnumerable<Geo.Uf> GetUFs()
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
                        return JsonConvert.DeserializeObject<List<Geo.Uf>>(jsonResponse);
                    }
                }
                catch { return new List<Geo.Uf>(); }
            }
            public List<Geo.Uf> Ufs()
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
                        return JsonConvert.DeserializeObject<List<Geo.Uf >> (jsonResponse);
                    }
                }
                catch (Exception ex) { ex.Log(); return new List<Geo.Uf > (); }
            }

            /// <summary>
            /// Obtém os dados de uma unidade federativa do Brasil por seu identificador
            /// </summary>
            /// <param name="id">Identificado da unidade federativa.</param>
            /// <returns>Objeto do tipo <see cref="Geo.Uf"/></returns>
            public Geo.Uf Ufs(int id)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{id}";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<Geo.Uf>(jsonResponse);
                    }
                }
                catch (Exception ex) { ex.Log(); return new Geo.Uf(); }
            }

            /// <summary>
            /// Obtém o conjunto de unidades federativas do Brasil em uma região
            /// </summary>
            /// <param name="regiaoId">Identificador da região.</param>
            /// <returns>Lista do tipo <see cref="Geo.Uf"/></returns>
            public List<Geo.Uf> UfsPorRegiao(int regiaoId)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/regioes/{regiaoId}/estados";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Uf>>(jsonResponse);
                    }
                }
                catch (Exception ex) { ex.Log(); return new List<Geo.Uf>(); }
            }
            #endregion
            #region SUBDISTRITOS
            /// <summary>
            /// Obtém o conjunto de unidades federativas do Brasil
            /// </summary>
            /// <returns>Lista do tipo <see cref="Geo.Subdistrito"/></returns>
            public List<Geo.Subdistrito> Subdistritos()
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/subdistritos";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Subdistrito>>(jsonResponse);
                    }
                }
                catch (Exception ex) { ex.Log(); return new List<Geo.Subdistrito>(); }
            }

            /// <summary>
            /// Obtém os dados de um subdisrtrito do Brasil por sua respectiva identificação.
            /// </summary>
            /// <param name="id">Identificação do subdistrito.</param>
            /// <returns>Lista do tipo <see cref="Geo.Subdistrito"/></returns>
            public Geo.Subdistrito SubdistritosPorId(int id)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/subdistritos/{id}/";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<Geo.Subdistrito>(jsonResponse);
                    }
                }
                catch (Exception ex) { ex.Log(); return new Geo.Subdistrito(); }
            }

            /// <summary>
            /// Obtém o conjunto de unidades federativas do Brasil
            /// </summary>
            /// <param name="distritoId">Identificação do distrito.</param>
            /// <returns>Lista do tipo <see cref="Geo.Subdistrito"/></returns>
            public List<Geo.Subdistrito> SubdistritosPorDistrito(int distritoId)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/distritos/{distritoId}/subdistritos";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Subdistrito>>(jsonResponse);
                    }
                }
                catch (Exception ex) { ex.Log(); return new List<Geo.Subdistrito>(); }
            }

            /// <summary>
            /// Obtém o conjunto de unidades federativas do Brasil de uma unidade federativa
            /// </summary>
            /// <param name="ufId">Identificação da unidade federativa.</param>
            /// <returns>Lista do tipo <see cref="Geo.Subdistrito"/></returns>
            public List<Geo.Subdistrito> SubdistritosPorUf(int ufId)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{ufId}/subdistritos";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Subdistrito>>(jsonResponse);
                    }
                }
                catch (Exception ex) { ex.Log(); return new List<Geo.Subdistrito>(); }
            }

            /// <summary>
            /// Obtém o conjunto de unidades federativas do Brasil de uma mesorregião
            /// </summary>
            /// <param name="mesorregiaoId">Identificação da mesorregião.</param>
            /// <returns>Lista do tipo <see cref="Geo.Subdistrito"/></returns>
            public List<Geo.Subdistrito> SubdistritosPorMesorregiao(int mesorregiaoId)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/mesorregioes/{mesorregiaoId}/subdistritos";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Subdistrito>>(jsonResponse);
                    }
                }
                catch (Exception ex) { ex.Log(); return new List<Geo.Subdistrito>(); }
            }

            /// <summary>
            /// Obtém o conjunto de unidades federativas do Brasil de uma mesorregião
            /// </summary>
            /// <param name="microrregiaoId">Identificação da mesorregião.</param>
            /// <returns>Lista do tipo <see cref="Geo.Subdistrito"/></returns>
            public List<Geo.Subdistrito> SubdistritosPorMicrorregiao(int microrregiaoId)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/microrregioes/{microrregiaoId}/subdistritos";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Subdistrito>>(jsonResponse);
                    }
                }
                catch (Exception ex) { ex.Log(); return new List<Geo.Subdistrito>(); }
            }

            /// <summary>
            /// Obtém o conjunto de unidades federativas do Brasil de uma região
            /// </summary>
            /// <param name="regiaoId">Identificação da mesorregião.</param>
            /// <returns>Lista do tipo <see cref="Geo.Subdistrito"/></returns>
            public List<Geo.Subdistrito> SubdistritosPorRegiao(int regiaoId)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/regioes/{regiaoId}/subdistritos";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Subdistrito>>(jsonResponse);
                    }
                }
                catch (Exception ex) { ex.Log(); return new List<Geo.Subdistrito>(); }
            }

            /// <summary>
            /// Obtém o conjunto de unidades federativas do Brasil de um município
            /// </summary>
            /// <param name="municipioId">Identificação do município.</param>
            /// <returns>Lista do tipo <see cref="Geo.Subdistrito"/></returns>
            public List<Geo.Subdistrito> SubdistritosPorMunicipio(int municipioId)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/municipios/{municipioId}/subdistritos";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Geo.Subdistrito>>(jsonResponse);
                    }
                }
                catch (Exception ex) { ex.Log(); return new List<Geo.Subdistrito>(); }
            }
            #endregion

            #region CEP

            /// <summary>
            /// Retorna um <see cref="Endereco"/> com base no CEP informado.
            /// </summary>
            /// <param name="cep">CEP á ser localizado.</param>
            /// <returns>Objeto do tipo <see cref="Endereco"/>.</returns>
            public Geo.Endereco EnderecoDoCEP(string cep)
            {
                Geo.Endereco ret = null;
                try
                {
                    string cp = cep.JustNumbers();
                    // EBCT:
                    try
                    {
                        string baseUrl = $"http://www.buscacep.correios.com.br/servicos/dnec/consultaLogradouroAction.do?Metodo=listaLogradouro&CEP={cp}&TipoConsulta=cep";
                        HttpWebRequest requisicao = (HttpWebRequest)WebRequest.Create(baseUrl);
                        HttpWebResponse resposta = (HttpWebResponse)requisicao.GetResponse();
                        int cont;
                        byte[] buffer = new byte[1000];
                        StringBuilder sb = new StringBuilder();
                        string temp;
                        Stream stream = resposta.GetResponseStream();
                        do
                        {
                            cont = stream.Read(buffer, 0, buffer.Length);
                            temp = System.Text.Encoding.Default.GetString(buffer, 0, cont).Trim();
                            sb.Append(temp);

                        } while (cont > 0);
                        string pagina = sb.ToString();
                        if (pagina.IndexOf("<font color=\"black\">CEP NAO ENCONTRADO</font>") <= 0)
                        {
                            ret = new Geo.Endereco()
                            {
                                CEP = cp,
                                Complemento = "",
                                Gia = "",
                                Ibge = "",
                                Unidade = "",
                                Logradouro = Regex.Match(pagina, "<td width=\"268\" style=\"padding: 2px\">(.*)</td>").Groups[1].Value,
                                Bairro = Regex.Matches(pagina, "<td width=\"140\" style=\"padding: 2px\">(.*)</td>")[0].Groups[1].Value,
                                Localidade = Regex.Matches(pagina, "<td width=\"140\" style=\"padding: 2px\">(.*)</td>")[1].Groups[1].Value,
                                UF = Regex.Match(pagina, "<td width=\"25\" style=\"padding: 2px\">(.*)</td>").Groups[1].Value
                            };
                        }
                    }
                    catch (Exception ex) { ex.Log(); ret = null; }


                    if (cp.Length < 8) cp = cp.PadRight(8, '0');
                    // ViaCEP:
                    if (ret == null)
                    {
                        var url = $"https://viacep.com.br/ws/{cp}/json/";
                        using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                        {
                            var response = Cliente.SendAsync(request).Result;
                            if (!response.IsSuccessStatusCode)
                                return null;
                            var jsonResponse = response.Content.ReadAsStringAsync().Result;
                            ret = JsonConvert.DeserializeObject<Geo.Endereco>(jsonResponse);
                        }
                    }
                }
                catch (Exception ex) { ex.Log(); ret = new Geo.Endereco(); }
                return ret;
            }

            public List<Geo.Uf> UfDoCep(string cep)
            {
                List<Geo.Uf> ret = new List<Geo.Uf>();
                try
                {
                    string[] itens = new string[] { };
                    List<Geo.Uf> ufs = new List<Geo.Uf>();
                    ufs.AddRange(GetUFs());
                    string cp = cep.JustNumbers();
                    if (cp.Length == 8) cp = cp.PadRight(8, '0');
                    switch (cp.Substring(0, 1))
                    {
                        default: break;

                        case "0": itens = new string[] { "São Paulo" }; break;
                        case "1": itens = new string[] { "São Paulo" }; break;
                        case "2": itens = new string[] { "Rio de Janeiro", "Espírito Santo" }; break;
                        case "3":
                            itens = new string[] { "Minas Gerais" }; break;
                        case "4":
                            itens = new string[] { "Bahia", "Sergipe" }; break;
                        case "5":
                            itens = new string[] { "Pernambuco", "Alagoas", "Paraíba", "Rio Grande do Norte" }; break;
                        case "6":
                            itens = new string[] { "Ceará", "Piauí", "Maranhão", "Pará", "Amazonas", "Acre", "Amapá", "Roraima" }; break;
                        case "7":
                            itens = new string[] { "Goiás", "Tocantins", "Mato Grosso", "Mato Grosso do Sul", "Rondônia", "DF" }; break;
                        case "8":
                            itens = new string[] { "Paraná", "Santa Catarina" }; break;
                        case "9":
                            itens = new string[] { "Rio Grande do Sul" }; break;
                    }
                    foreach (string item in itens)
                        ret.AddRange(ufs.Where(p => p.Nome.ToUpper().Trim().Contains(item.ToUpper().Trim())));
                }
                catch (Exception ex) { ex.Log(); ret = new List<Geo.Uf>(); }
                return ret;
            }
            #endregion

            #region CARGOS E ATIVIDADES
            /// <summary>
            /// Retorna a lista completa de classes CNAE.
            /// </summary>
            /// <returns></returns>
            public List<CNAE.classe> CNAE()
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v2/cnae/classes";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<CNAE.classe>>(jsonResponse);
                    }
                }
                catch { return new List<CNAE.classe>(); }
            }
            /// <summary>
            /// Retorna a lista completa de classes CNAE.
            /// </summary>
            /// <returns></returns>
            public List<CNAE.subclasse> CNAE(CNAE.classe classe)
            {
                try
                {
                    var url = $"https://servicodados.ibge.gov.br/api/v2/cnae/classes/{classe.id}/subclasses";
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        var response = Cliente.SendAsync(request).Result;
                        if (!response.IsSuccessStatusCode)
                            return null;
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<CNAE.subclasse>>(jsonResponse);
                    }
                }
                catch { return new List<CNAE.subclasse>(); }
            }
            /// <summary>
            /// Retorna a lista basica de cargos para a tabela CBO-2002, originada 
            /// por meio do arquivo de texto obtido junto ao Ministério do Trabalho.
            /// </summary>
            public static IEnumerable<CBO.Cargo> CBO2002()
            {
                try
                {
                    string json = Properties.Resources.cbo2002;
                    return JsonConvert
                        .DeserializeObject<List<CBO.Cargo>>(json)
                        .Where(p => !string.IsNullOrEmpty(p.descricao));
                }
                catch (Exception ex) { ex.Log(); return new List<CBO.Cargo>(); }
            }
            #endregion

            #endregion
        }
    }
}
