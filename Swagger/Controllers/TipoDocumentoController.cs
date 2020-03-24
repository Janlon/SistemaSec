using Sec;
using Sec.Business;
using Sec.Models;

using System.Linq;
using System.Web.Http;

namespace Swagger.Controllers
{
    [RoutePrefix("api/TipoDocumento")]
    public class TipoDocumentoController : ApiController
    {
        [Route("All")]
        public CrudResult<TipoDeDocumento> All()
        {
            return Engine.TiposDeDocumentos.List();
        }
        [Route("Find")]
        public CrudResult<TipoDeDocumento> Find(int id)
        {
            return Engine.TiposDeDocumentos.Find(new object[] { id });
        }

        [Route("Insert")]
        public CrudResult<TipoDeDocumento> Insert(string name, string shortName, bool personal)
        {
            return Engine.TiposDeDocumentos.Insert(new TipoDeDocumento()
            {
                Descricao = (string.IsNullOrEmpty(name) ? "" : name).Trim().ToUpper(),
                PessoaFisica = personal,
                Sigla = shortName
            });
        }

        [Route("Filter")]
        public CrudResult<TipoDeDocumento> Filter(string what, StringFilter filter)
        {
            CrudResult<TipoDeDocumento> ret = null;
            switch (filter)
            {
                case StringFilter.Contains:
                    ret = Engine.TiposDeDocumentos.Filter(p => p.Descricao.Contains(what));
                    break;
                case StringFilter.Different:
                    ret = Engine.TiposDeDocumentos.Filter(p => (!p.Descricao.Equals(what)));
                    break;
                case StringFilter.EndsWith:
                    ret = Engine.TiposDeDocumentos.Filter(p => p.Descricao.EndsWith(what));
                    break;
                case StringFilter.Equals:
                    ret = Engine.TiposDeDocumentos.Filter(p => p.Descricao.Equals(what));
                    break;
                case StringFilter.IsEmpty:
                    ret = Engine.TiposDeDocumentos.Filter(p => p.Descricao.Equals(""));
                    break;
                case StringFilter.IsNull:
                    ret = Engine.TiposDeDocumentos.Filter(p => (p.Descricao==null));
                    break;
                case StringFilter.IsNullOrEmpty:
                    ret = Engine.TiposDeDocumentos.Filter(p => (p.Descricao == null||p.Descricao.Equals("")));
                    break;
                case StringFilter.NotContains:
                    ret = Engine.TiposDeDocumentos.Filter(p => (!p.Descricao.Contains(what)));
                    break;
                case StringFilter.NotEndsWith:
                    ret = Engine.TiposDeDocumentos.Filter(p => (!p.Descricao.EndsWith(what)));
                    break;
                case StringFilter.NotIsEmpty:
                    ret = Engine.TiposDeDocumentos.Filter(p => (!p.Descricao.Equals("")));
                    break;
                case StringFilter.NotIsNull:
                    ret = Engine.TiposDeDocumentos.Filter(p => (p.Descricao != null));
                    break;
                case StringFilter.NotIsNullOrEmpty:
                    ret = Engine.TiposDeDocumentos.Filter(p => (!(p.Descricao == null || p.Descricao.Equals(""))));
                    break;
                case StringFilter.NotStartsWith:
                    ret = Engine.TiposDeDocumentos.Filter(p => (!p.Descricao.StartsWith(what)));
                    break;
                case StringFilter.StartsWith:
                    ret = Engine.TiposDeDocumentos.Filter(p => p.Descricao.StartsWith(what));
                    break;
            }
            return ret;
        }

        [Route("Delete")]
        public CrudResult<TipoDeDocumento> Delete(int id)
        {
            CrudResult<TipoDeDocumento> ret = null;
            ret = Engine.TiposDeDocumentos.Delete( Engine.TiposDeDocumentos.Find(new object[] { id }).Result.FirstOrDefault() );
            return ret;
        }
        [Route("Reactivate")]
        public CrudResult<TipoDeDocumento> Reactivate(int id)
        {
            CrudResult<TipoDeDocumento> ret = null;
            ret = Engine.TiposDeDocumentos.Reactivate(Engine.TiposDeDocumentos.Find(new object[] { id }).Result.FirstOrDefault());
            return ret;
        }
        [Route("Deactivate")]
        public CrudResult<TipoDeDocumento> Deactivate(int id)
        {
            CrudResult<TipoDeDocumento> ret = null;
            ret = Engine.TiposDeDocumentos.Deactivate(Engine.TiposDeDocumentos.Find(new object[] { id }).Result.FirstOrDefault());
            return ret;
        }
    }
}
