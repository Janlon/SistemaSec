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
    public class DocumentoController : ApiController
    {
        public CrudResult<Documento> Get()
        {
            return Engine.Documentos.List();
        }

        public CrudResult<Documento> Get(int id)
        {
           return Engine.Documentos.Find( new object[] { id });

        }

        [Route("~/api/Documento/{pessoaId:int}/documentos")]
        public CrudResult<Documento> GetDocumentosDaPessoa(int pessoaId)
        {
             return Engine.Documentos.Filter(p => p.PessoaId.Equals(pessoaId));
        }

        public CrudResult<Documento> Post(Documento obj)
        {
            return Engine.Documentos.Insert(obj);
        }

        public CrudResult<Documento> Put(Documento obj)
        {
            return Engine.Documentos.Update(obj);
        }

        public CrudResult<Documento> Delete(int id)
        {
            return Engine.Documentos.Delete(Engine.Documentos.Find(new object[] { id }).Result.FirstOrDefault());
        }

    }
}
