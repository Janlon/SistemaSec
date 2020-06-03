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
        public CrudResult<Setor> GetSetoresDaEmpresa(int id)
        {
            return Engine.Setores.Filter(p => p.EmpresaId.Equals(id));
        }

        public CrudResult<Empresa> Post(Empresa obj) => Engine.Empresas.Insert(obj);

        [Route("~/api/Empresa/Filial")]
        public CrudResult<Empresa> PostMatrizFilial(EmpresaFilial obj)
        {

            Empresa empresa = Engine.Empresas.Filter(p => p.Id.Equals(obj.MatrizId)).Result.FirstOrDefault();
            empresa.EhMatriz = true;
            var ret = Engine.Empresas.Update(empresa);

            EmpresaMatriz empresaM = new EmpresaMatriz()
            {
                EmpresaId = obj.MatrizId
            };
            int empresaMId = Engine.Matrizes.Insert(empresaM).Origin.Id;
          
            foreach (var filialId in obj.FilialId)
            {
                Empresa empresaF = Engine.Empresas.Filter(p => p.Id.Equals(filialId)).Result.FirstOrDefault();
                empresaF.EhMatriz = false;
                ret = Engine.Empresas.Update(empresaF);

                EmpresaFilial empresaFilial = new EmpresaFilial()
                {
                    MatrizId = empresaMId,
                    EmpresaId = filialId
                };

                var rett = Engine.Filiais.Insert(obj);
            }

            return Engine.Empresas.List();

        }

        public CrudResult<Empresa> Put(Empresa obj)
        {
            return Engine.Empresas.Update(obj);
        }

        public CrudResult<Empresa> Delete(int id) => Engine.Empresas.Delete(Engine.Empresas.Find(new object[] { id }).Result.FirstOrDefault());
    }
}
