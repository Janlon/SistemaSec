using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Sec.Business;
using Sec.Models;

namespace Swagger.Controllers
{
    public class ItemDaOrdemDeServicoController : ApiController
    {
        public CrudResult<ItemDaOrdemDeServico> Get()
        {
            return Engine.ItensDasOrdensDeServicos.List();
        }

        public CrudResult<ItemDaOrdemDeServico> Get(int id)
        {
            return Engine.ItensDasOrdensDeServicos.Find(new object[] { id });
        }

        public CrudResult<ItemDaOrdemDeServico> Post(ItemDaOrdemDeServico obj)
        {
            return Engine.ItensDasOrdensDeServicos.Insert(obj);
        }

        public CrudResult<ItemDaOrdemDeServico> Put(ItemDaOrdemDeServico obj)
        {
            return Engine.ItensDasOrdensDeServicos.Update(obj);
        }

        public CrudResult<ItemDaOrdemDeServico> Delete(int id)
        {
            return Engine.ItensDasOrdensDeServicos.Delete(Engine.ItensDasOrdensDeServicos.Find(new object[] { id }).Result.FirstOrDefault());
        }
    }
}