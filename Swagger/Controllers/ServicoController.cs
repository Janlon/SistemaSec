using Sec.Business;
using Sec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Swagger.Controllers
{
    public class ServicoController : ApiController
    {

        public CrudResult<Servico> Get()
        {
            return Engine.Servicos.List();
        }

        public CrudResult<Servico> Get(int id)
        {
            return Engine.Servicos.Find(new object[] { id });
        }

        public CrudResult<Servico> Post(Servico obj)
        {
            return Engine.Servicos.Insert(obj);
        }

        public CrudResult<Servico> Put(Servico obj)
        {
            return Engine.Servicos.Update(obj);
        }

        public CrudResult<Servico> Delete(int id)
        {
            return Engine.Servicos.Delete(Engine.Servicos.Find(new object[] { id }).Result.FirstOrDefault());
        }
    }
}
