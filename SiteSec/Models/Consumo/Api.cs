using Kendo.Mvc.UI.Fluent;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SiteSec.Models.Consumo
{

    public class Api
    {
        internal async Task<string> UseSimple<T>(HttpMethod http, T obj, int? id = 0)
        {
            try
            {
                HttpResponseMessage response;
                string controller = obj.GetType().Name;
                string endereço = string.Format("https://{0}api/{1}/{2}", ConfigurationManager.AppSettings["Api"].ToString(), controller, id == 0 ? "" : id.ToString());
                string output = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(output, Encoding.UTF8, "application/json");

                switch (http.Method)
                {
                    case "POST":
                        using (var client = new HttpClient())
                        {
                            response = await client.PostAsync(endereço, content);
                            break;
                        }
                    default:
                        using (var client = new HttpClient())
                        {
                            response = await client.GetAsync(endereço);
                            break;
                        }
                }

                var contents = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject(contents).ToString();
                }

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        internal async Task<bool> Token(Account obj)
        {
            Usuario usuario = new Usuario();
            try
            {
                string url = string.Format("https://{0}{1}", ConfigurationManager.AppSettings["Api"].ToString(), "token");
                Dictionary<string, string> parametros = new Dictionary<string, string>
                {
                    { "grant_type", "password" },
                    { "username", obj.UserName },
                    { "password", obj.Password }
                };
                FormUrlEncodedContent parametrosComEncode = new FormUrlEncodedContent(parametros);
                Uri baseUri = new Uri(url);
                HttpRequestMessage requestToken = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(baseUri, "token"),
                    Content = new StringContent("grant_type=password")
                };

                requestToken.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded") { CharSet = "UTF-8" };
                requestToken.Content = parametrosComEncode;

                using (var client = new HttpClient())
                {
                    var httpResponse = await client.SendAsync(requestToken);
                    string content = httpResponse.Content.ReadAsStringAsync().Result;
                    string bearerData = await httpResponse.Content.ReadAsStringAsync();

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        usuario.AccessTokenFormat = JObject.Parse(bearerData)["access_token"].ToString();
                        usuario.UserName = JObject.Parse(bearerData)["userName"].ToString();
                        return true;
                    }

                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        internal async Task<ApiRetorno> Use<T>(HttpMethod http, T obj, string metodo)
        {
            ApiRetorno apiRetorno = new ApiRetorno();
            try
            {
                Usuario usuario = new Usuario();
                Account account = new Account() { UserName = "janloncavalchi@msn.com", Password = "Senha@123" };
                var ok = Token(account);


                HttpResponseMessage response;
                string url = string.Format("https://{0}{1}", ConfigurationManager.AppSettings["Api"].ToString(), metodo);
                string output = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(output, Encoding.UTF8, "application/json");

                switch (http.Method)
                {
                    case "POST":
                        using (var client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", usuario.Token));
                            response = await client.PostAsync(url, content);
                            break;
                        }
                    case "PUT":
                        using (var client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));
                            response = await client.PutAsync(url, content);
                            break;
                        }
                    case "DELETE":
                        using (var client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));
                            response = await client.DeleteAsync(url);
                            break;
                        }
                    default:
                        using (var client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));
                            response = await client.GetAsync(url);
                            break;
                        }
                }

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    apiRetorno = JsonConvert.DeserializeObject<ApiRetorno>(result);
                    apiRetorno.mensagem = ValidarRetorno(http, response.IsSuccessStatusCode);
                    return apiRetorno;
                };

                apiRetorno.mensagem = ValidarRetorno(http, response.IsSuccessStatusCode);
                return apiRetorno;
            }
            catch (Exception ex)
            {
                apiRetorno.mensagem = ex.Message;
                return apiRetorno;
            }
        }
        internal static string ValidarRetorno(HttpMethod http, bool ok)
        {
            switch (http.Method)
            {
                case "POST":
                    if (ok)
                        return string.Format("Registro {0} com sucesso.", "inserido");
                    else
                        return string.Format("Ocorreu um erro ao {0} o registro.", "inserir");
                case "PUT":
                    if (ok)
                        return string.Format("Registro {0} com sucesso.", "atualizado");
                    else
                        return string.Format("Ocorreu um erro ao {0} o registro.", "atualizar");
                case "DELETE":
                    if (ok)
                        return string.Format("Registro {0} com sucesso.", "excluído");
                    else
                        return string.Format("Ocorreu um erro ao {0} o registro.", "excluir");
                default:
                    return "";
            }
        }
    }
}