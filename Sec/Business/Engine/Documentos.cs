namespace Sec.Business
{
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class Documentos
        {
            public static CrudResult<Documento> Insert(Documento value)
            {
                CrudResult<Documento> ret;
                using (DocumentosFactory db = new DocumentosFactory())
                    ret = db.Create(value);
                return ret;
            }
            public static CrudResult<Documento> List()
            {
                CrudResult<Documento> ret;
                using (DocumentosFactory db = new DocumentosFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<Documento> Filter(Expression<Func<Documento, bool>> where)
            {
                CrudResult<Documento> ret;
                using (DocumentosFactory db = new DocumentosFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<Documento> Find(object[] keys)
            {
                CrudResult<Documento> ret;
                using (DocumentosFactory db = new DocumentosFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<Documento> Update(Documento value)
            {
                CrudResult<Documento> ret;
                using (DocumentosFactory db = new DocumentosFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<Documento> Delete(Documento value)
            {
                CrudResult<Documento> ret;
                using (DocumentosFactory db = new DocumentosFactory())
                    ret = db.Delete(value);
                return ret;
            }
        }
    }
}