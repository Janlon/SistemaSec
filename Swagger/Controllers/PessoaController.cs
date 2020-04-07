using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sec.Business;
using Sec.Models;

namespace Swagger.Controllers
{
    public class PessoaController : ApiController
    {
        public CrudResult<Pessoa> Get()
        {
            return Engine.Pessoas.List();
        }

        public CrudResult<Pessoa> Get(int id)
        {
            return Engine.Pessoas.Find(new object[] { id });
        }

        public CrudResult<Pessoa> Post(Pessoa obj)
        {
            return Engine.Pessoas.Insert(obj);
        }

        public CrudResult<Pessoa> Put(Pessoa obj)
        {
            return Engine.Pessoas.Update(obj);
        }

        public CrudResult<Pessoa> Delete(int id)
        {
            return Engine.Pessoas.Delete(Engine.Pessoas.Find(new object[] { id }).Result.FirstOrDefault());
        }
    }
}
