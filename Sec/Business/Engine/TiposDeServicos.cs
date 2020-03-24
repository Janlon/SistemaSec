namespace Sec.Business
{
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class TiposDeServicos
        {
            public static CrudResult<TipoDeServico> Insert(TipoDeServico value)
            {
                CrudResult<TipoDeServico> ret;
                using (TiposDeServicosFactory db = new TiposDeServicosFactory())
                    ret = db.Create(value);
                return ret;
            }
            public static CrudResult<TipoDeServico> List()
            {
                CrudResult<TipoDeServico> ret;
                using (TiposDeServicosFactory db = new TiposDeServicosFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<TipoDeServico> Filter(Expression<Func<TipoDeServico, bool>> where)
            {
                CrudResult<TipoDeServico> ret;
                using (TiposDeServicosFactory db = new TiposDeServicosFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<TipoDeServico> Find(object[] keys)
            {
                CrudResult<TipoDeServico> ret;
                using (TiposDeServicosFactory db = new TiposDeServicosFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<TipoDeServico> Update(TipoDeServico value)
            {
                CrudResult<TipoDeServico> ret;
                using (TiposDeServicosFactory db = new TiposDeServicosFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<TipoDeServico> Delete(TipoDeServico value)
            {
                CrudResult<TipoDeServico> ret;
                using (TiposDeServicosFactory db = new TiposDeServicosFactory())
                    ret = db.Delete(value);
                return ret;
            }
        }
    }
}
