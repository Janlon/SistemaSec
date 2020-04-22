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
    public class SetorController : ApiController
    {

        public CrudResult<Setor> Get()
        {
            return Engine.Setores.List();
        }
       
        public CrudResult<Setor> Get(int id)
        {
            return Engine.Setores.Find(new object[] { id });
        }

        [Route("~/api/Setor/{Id:int}/Equipamentos")]
        public CrudResult<Equipamento> GetEquipamentosDoSetor(int Id)
        {
            return Engine.Equipamentos.Filter(p => p.SetorId.Equals(Id));
        }

        public CrudResult<Setor> Post(Setor obj)
        {
            return Engine.Setores.Insert(obj);
        }

        public CrudResult<Setor> Put(Setor obj)
        {
            return Engine.Setores.Update(obj);
        }

        public CrudResult<Setor> Delete(int id)
        {
            return Engine.Setores.Delete(Engine.Setores.Find(new object[] { id }).Result.FirstOrDefault());
        }

    }
}
