using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class Api
{
    internal async Task<string> Use<T>(HttpMethod metodo, T obj, int id = 0)
    {
        try
        {
            HttpResponseMessage response;
            string controller = obj.GetType().Name;
            string endereço = string.Format("https://{0}api/{1}/{2}", ConfigurationManager.AppSettings["Api"].ToString(), controller, id == 0 ? "" : id.ToString());
            string output = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(output, Encoding.UTF8, "application/json");

            switch (metodo.Method)
            {
                case "Post":
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
    internal async Task<ApiRetorno> Use<T>(HttpMethod http, T obj, string metodo)
    {
        ApiRetorno apiRetorno = new ApiRetorno();
        try
        {
            HttpResponseMessage response;
            string url = string.Format("https://{0}{1}", ConfigurationManager.AppSettings["Api"].ToString(), metodo );
            string output = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(output, Encoding.UTF8, "application/json");
           
            switch (http.Method)
            {
                case "POST":
                    using (var client = new HttpClient())
                    {
                        response = await client.PostAsync(url, content);
                        break;
                    }
                case "PUT":
                    using (var client = new HttpClient())
                    {
                        response = await client.PutAsync(url, content);
                        break;
                    }
                case "DELETE":
                    using (var client = new HttpClient())
                    {
                        response = await client.DeleteAsync(url);
                        break;
                    }
                default:
                    using (var client = new HttpClient())
                    {
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
                if(ok)
                    return string.Format("Registro {0} com sucesso.", "inserido");
                else
                    return string.Format("Ocorreu um erro ao {0} o registro.", "inserir");
            case "PUT":
                if(ok)
                    return string.Format("Registro {0} com sucesso.", "atualizado");
                else
                    return string.Format("Ocorreu um erro ao {0} o registro.", "atualizar");
            case "DELETE":
                if(ok)
                    return string.Format("Registro {0} com sucesso.", "excluído");
                else
                    return string.Format("Ocorreu um erro ao {0} o registro.", "excluir");
            default:
                return "";
        }
    }
}

