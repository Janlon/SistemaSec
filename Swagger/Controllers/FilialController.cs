using Sec.Business;
using Sec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace Swagger.Controllers
{
    public class FilialController : ApiController
    {
        public CrudResult<EmpresaFilial> Get() => Engine.Filiais.List();

        public CrudResult<EmpresaFilial> Get(int id) => Engine.Filiais.Find(new object[] { id });

        public CrudResult<EmpresaFilial> Post(EmpresaFilial obj) => Engine.Filiais.Insert(obj);

        public CrudResult<Empresa> Put(Empresa obj) => Engine.Empresas.Update(obj);

        public CrudResult<Empresa> Delete(int id) => Engine.Empresas.Delete(Engine.Empresas.Find(new object[] { id }).Result.FirstOrDefault());
    }
}
