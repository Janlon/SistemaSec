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
    public class MatrizFilialController : ApiController
    {
        public CrudResult<EmpresaMatrizFilial> Get() => Engine.MatrizesFiliais.List();

        public CrudResult<EmpresaMatrizFilial> Get(int id) => Engine.MatrizesFiliais.Find(new object[] { id });

        public CrudResult<EmpresaMatrizFilial> Post(EmpresaMatrizFilial obj) => Engine.MatrizesFiliais.Insert(obj);

        public CrudResult<EmpresaMatrizFilial> Put(EmpresaMatrizFilial obj) => Engine.MatrizesFiliais.Update(obj);

        public CrudResult<EmpresaMatrizFilial> Delete(int id) => Engine.MatrizesFiliais.Delete(Engine.MatrizesFiliais.Find(new object[] { id }).Result.FirstOrDefault());
    }
}
