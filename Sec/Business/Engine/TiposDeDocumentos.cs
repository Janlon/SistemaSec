namespace Sec.Business
{
    using Generics.Extensoes;
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class TiposDeDocumentos
        {
            public static CrudResult<TipoDeDocumento> Reactivate(TipoDeDocumento value)
            {
                CrudResult<TipoDeDocumento> ret = null;
                if (value != null)
                {
                    using (TiposDeDocumentosFactory db = new TiposDeDocumentosFactory())
                        ret = db.Reactivate(value);
                }
                return ret;
            }
            public static CrudResult<TipoDeDocumento> Deactivate(TipoDeDocumento value)
            {
                CrudResult<TipoDeDocumento> ret=null;
                if (value != null)
                {
                    using (TiposDeDocumentosFactory db = new TiposDeDocumentosFactory())
                        ret = db.Deactivate(value);
                }
                return ret;
            }
            public static CrudResult<TipoDeDocumento> Insert(TipoDeDocumento value) 
            {
                CrudResult<TipoDeDocumento> ret = null;
                if (value != null)
                {
                    using (TiposDeDocumentosFactory db = new TiposDeDocumentosFactory())
                        ret = db.Create(value);
                }
                return ret;
            }
            public static CrudResult<TipoDeDocumento> List()
            {
                CrudResult<TipoDeDocumento> ret;
                using (TiposDeDocumentosFactory db = new TiposDeDocumentosFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<TipoDeDocumento> Filter(Expression<Func<TipoDeDocumento,bool>> where)
            {
                CrudResult<TipoDeDocumento> ret;
                using (TiposDeDocumentosFactory db = new TiposDeDocumentosFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<TipoDeDocumento> Find(object[] keys)
            {
                CrudResult<TipoDeDocumento> ret;
                using (TiposDeDocumentosFactory db = new TiposDeDocumentosFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<TipoDeDocumento> Update(TipoDeDocumento value)
            {
                CrudResult<TipoDeDocumento> ret;
                using (TiposDeDocumentosFactory db = new TiposDeDocumentosFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<TipoDeDocumento> Delete(TipoDeDocumento value)
            {
                CrudResult<TipoDeDocumento> ret;
                try
                {
                    using (TiposDeDocumentosFactory db = new TiposDeDocumentosFactory())
                        ret = db.Delete(value);
                }
                catch (Exception ex) { ex.Log(); ret = new CrudResult<TipoDeDocumento>(value); }
                return ret;
            }
        }
    }
}
