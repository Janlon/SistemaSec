using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Linq;
using System.Web.Mvc;
using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace SiteSec.Models.Scheduler
{
    public class SchedulerTaskService
    {
        readonly Api api = new Api();
        public async Task<List<TaskViewModel>> GetAll()
        {
            List<TaskViewModel> tasks = new List<TaskViewModel>();

            var apiRetorno = await api.Use(HttpMethod.Get, new OrdemServico(), $"api/OrdemServico");
            var str = JsonConvert.SerializeObject(apiRetorno.result);
            List<OrdemServico> ordemServicos = JsonConvert.DeserializeObject<List<OrdemServico>>(str);

            foreach (var os in ordemServicos)
            {

                apiRetorno = await api.Use(HttpMethod.Get, new ItemOrdemServico(), $"/api/OrdemDeServico/{os.Id}/Itens");
                str = JsonConvert.SerializeObject(apiRetorno.result);
                List<ItemOrdemServico> itens = JsonConvert.DeserializeObject<List<ItemOrdemServico>>(str);
                foreach (var item in itens)
                {
                  
                    //buscar a empresa da ordem de serviço
                    apiRetorno = await api.Use(HttpMethod.Get, new Empresa(), $"api/Empresa/{os.EmpresaId}");
                    str = JsonConvert.SerializeObject(apiRetorno.result);
                    Empresa empresa = JsonConvert.DeserializeObject<List<Empresa>>(str).FirstOrDefault();

                    item.Empresa = empresa.RazaoSocial;

                    //buscar o objeto servicos 
                    apiRetorno = await api.Use(HttpMethod.Get, new Servico(), $"api/Servico/{item.ServicoId}");
                    str = JsonConvert.SerializeObject(apiRetorno.result);
                    Servico servico = JsonConvert.DeserializeObject<List<Servico>>(str).FirstOrDefault();

                    item.Serviço = servico.Descricao;

                    //buscar o ojeto equipamento
                    apiRetorno = await api.Use(HttpMethod.Get, new Equipamento(), $"api/Equipamento/{item.EquipamentoId}");
                    str = JsonConvert.SerializeObject(apiRetorno.result);
                    Equipamento equipamento = JsonConvert.DeserializeObject<List<Equipamento>>(str).FirstOrDefault();

                    //buscar o objeto setor
                    apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/Setor/{equipamento.SetorId}");
                    str = JsonConvert.SerializeObject(apiRetorno.result);
                    Setor setor = JsonConvert.DeserializeObject<List<Setor>>(str).FirstOrDefault();

                    //buscar o objeto tipo de setores
                    apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/TipoSetor/{setor.TipoDeSetorId}/");
                    str = JsonConvert.SerializeObject(apiRetorno.result);
                    TipoSetor tiposetor = JsonConvert.DeserializeObject<List<TipoSetor>>(str).FirstOrDefault();

                    item.Setor = tiposetor.Descricao;

                    //buscar o ojeto tipo de equipamento
                    apiRetorno = await api.Use(HttpMethod.Get, new Setor(), $"api/TipoEquipamento/{equipamento.TipoEquipamentoId}/");
                    str = JsonConvert.SerializeObject(apiRetorno.result);
                    TipoEquipamento tipoEquipamento = JsonConvert.DeserializeObject<List<TipoEquipamento>>(str).FirstOrDefault();

                    item.Equipamento = tipoEquipamento.Descricao;

                    //buscar o objeto retirado 
                    apiRetorno = await api.Use(HttpMethod.Get, new Retirada(), $"api/Retirada/{item.Id}/Item");
                    str = JsonConvert.SerializeObject(apiRetorno.result);
                    Retirada retirada = JsonConvert.DeserializeObject<List<Retirada>>(str).FirstOrDefault();


                    apiRetorno = await api.Use(HttpMethod.Get, new Pessoa(), $"api/Pessoa/{retirada.PessoaId}");
                    str = JsonConvert.SerializeObject(apiRetorno.result);
                    Pessoa pessoa = JsonConvert.DeserializeObject<List<Pessoa>>(str).FirstOrDefault();


                    TaskViewModel taskViewModel = new TaskViewModel()
                    {
                        PessoaId = pessoa.Id,
                        ItemId = item.Id,
                        Title = "Ordem de Serviço n°: " + os.Numero + " [ " + item.Serviço + " ]",
                        Description = item.Serviço + " no " + item.Equipamento  + "\r\n" + "Empresa: " + item.Empresa + "\r\n" + "Setor: " + item.Setor,
                        Start = DateTime.SpecifyKind(os.Emissao, DateTimeKind.Utc),
                        End = DateTime.SpecifyKind(os.Validade, DateTimeKind.Utc)
                    };

                    tasks.Add(taskViewModel);

                }
            }

            return tasks;
        }

      
    }

}