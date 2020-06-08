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
    public class EmpresaController : ApiController
    {
        public CrudResult<Empresa> Get() => Engine.Empresas.List();

        public CrudResult<Empresa> Get(int id) => Engine.Empresas.Find(new object[] { id });

        [Route("~/api/Empresa/{Id:int}/setores")]
        public CrudResult<Setor> GetSetoresDaEmpresa(int Id) => Engine.Setores.Filter(p => p.EmpresaId.Equals(Id));

        public CrudResult<Empresa> Post(Empresa obj) => Engine.Empresas.Insert(obj);

        [Route("~/api/Empresa/Matriz")]
        public CrudResult<EmpresaMatriz> PostMatriz(EmpresaMatriz obj) => Engine.Matrizes.Insert(obj);

        [Route("~/api/Empresa/Filial")]
        public CrudResult<EmpresaFilial> PostFilial(EmpresaFilial obj) => Engine.Filiais.Insert(obj);

        [Route("~/api/Empresa/MatrizFilial")]
        public CrudResult<EmpresaMatrizFilial> PostMatrizFilial(EmpresaMatrizFilial obj) => Engine.MatrizesFiliais.Insert(obj);

        public CrudResult<Empresa> Put(Empresa obj) => Engine.Empresas.Update(obj);

        public CrudResult<Empresa> Delete(int id) => Engine.Empresas.Delete(Engine.Empresas.Find(new object[] { id }).Result.FirstOrDefault());
    }
}
