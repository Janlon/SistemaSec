using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;

public class Api
{
    internal ApiRetorno ConsumirApiLista<T>(HttpMethod metodo, T obj, int id = 0)
    {

        ApiRetorno apiRetorno = new ApiRetorno();
        try
        {
            HttpResponseMessage response;
            string controller = obj.GetType().Name;
            string endereço = string.Format("https://{0}/{1}/{2}", ConfigurationManager.AppSettings["Api"].ToString(), controller, id == 0 ? "" : id.ToString());
            string output = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(output, Encoding.UTF8, "application/json");

            switch (metodo.Method)
            {
                case "Post":
                    using (var client = new HttpClient())
                    {
                        response = client.PostAsync(endereço, content).Result;
                        break;
                    }
                default:
                    using (var client = new HttpClient())
                    {
                        response = client.GetAsync(endereço).Result;
                        break;
                    }
            }

            var contents = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                apiRetorno.mensagem = JsonConvert.DeserializeObject(contents).ToString();
                return apiRetorno;
            };

            return apiRetorno;
        }
        catch (Exception ex)
        {
            apiRetorno.mensagem = ex.Message;
            return apiRetorno;
        }
    }
    internal ApiRetorno Use<T>(HttpMethod http, T obj, string metodo)
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
                        response = client.PostAsync(url, content).Result;
                        break;
                    }
                case "PUT":
                    using (var client = new HttpClient())
                    {
                        response = client.PutAsync(url, content).Result;
                        break;
                    }
                case "DELETE":
                    using (var client = new HttpClient())
                    {
                        response =  client.DeleteAsync(url).Result;
                        break;
                    }
                default:
                    using (var client = new HttpClient())
                    {
                        response =  client.GetAsync(url).Result;
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

