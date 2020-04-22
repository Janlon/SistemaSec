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
    public class EmpresaController : ApiController
    {
        public CrudResult<Empresa> Get() => Engine.Empresas.List();

        public CrudResult<Empresa> Get(int id) => Engine.Empresas.Find(new object[] { id });

        [Route("~/api/Empresa/{Id:int}/setores")]
        public CrudResult<Setor> GetSetoresDaEmpresa(int id)
        {
            return Engine.Setores.Filter(p => p.EmpresaId.Equals(id));
        }

        public CrudResult<Empresa> Post(Empresa obj) => Engine.Empresas.Insert(obj);

        public CrudResult<Empresa> Put(Empresa obj) => Engine.Empresas.Update(obj);

        public CrudResult<Empresa> Delete(int id) => Engine.Empresas.Delete(Engine.Empresas.Find(new object[] { id }).Result.FirstOrDefault());
    }
}
