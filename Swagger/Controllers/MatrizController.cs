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
    public class MatrizController : ApiController
    {
        public CrudResult<EmpresaMatriz> Get() => Engine.Matrizes.List();

        public CrudResult<EmpresaMatriz> Get(int id) => Engine.Matrizes.Find(new object[] { id });

        public CrudResult<EmpresaMatriz> Post(EmpresaMatriz obj) => Engine.Matrizes.Insert(obj);

        public CrudResult<EmpresaMatriz> Put(EmpresaMatriz obj) => Engine.Matrizes.Update(obj);

        public CrudResult<EmpresaMatriz> Delete(int id) => Engine.Matrizes.Delete(Engine.Matrizes.Find(new object[] { id }).Result.FirstOrDefault());
    }
}
