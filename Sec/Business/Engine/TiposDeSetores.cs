namespace Sec.Business
{
    using Generics.Extensoes;
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class TiposDeSetores
        {
            public static CrudResult<TipoDeSetor> Reactivate(TipoDeSetor value)
            {
                CrudResult<TipoDeSetor> ret = null;
                if (value != null)
                {
                    using (TiposDeSetoresFactory db = new TiposDeSetoresFactory())
                        ret = db.Reactivate(value);
                }
                return ret;
            }
            public static CrudResult<TipoDeSetor> Deactivate(TipoDeSetor value)
            {
                CrudResult<TipoDeSetor> ret = null;
                if (value != null)
                {
                    using (TiposDeSetoresFactory db = new TiposDeSetoresFactory())
                        ret = db.Deactivate(value);
                }
                return ret;
            }
            public static CrudResult<TipoDeSetor> Insert(TipoDeSetor value)
            {
                CrudResult<TipoDeSetor> ret = null;
                if (value != null)
                {
                    using (TiposDeSetoresFactory db = new TiposDeSetoresFactory())
                        ret = db.Create(value);
                }
                return ret;
            }
            public static CrudResult<TipoDeSetor> List()
            {
                CrudResult<TipoDeSetor> ret;
                using (TiposDeSetoresFactory db = new TiposDeSetoresFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<TipoDeSetor> Filter(Expression<Func<TipoDeSetor, bool>> where)
            {
                CrudResult<TipoDeSetor> ret;
                using (TiposDeSetoresFactory db = new TiposDeSetoresFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<TipoDeSetor> Find(object[] keys)
            {
                CrudResult<TipoDeSetor> ret;
                using (TiposDeSetoresFactory db = new TiposDeSetoresFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<TipoDeSetor> Update(TipoDeSetor value)
            {
                CrudResult<TipoDeSetor> ret;
                using (TiposDeSetoresFactory db = new TiposDeSetoresFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<TipoDeSetor> Delete(TipoDeSetor value)
            {
                CrudResult<TipoDeSetor> ret;
                try
                {
                    using (TiposDeSetoresFactory db = new TiposDeSetoresFactory())
                        ret = db.Delete(value);
                }
                catch (Exception ex) { ex.Log(); ret = new CrudResult<TipoDeSetor>(value); }
                return ret;
            }
        }
    }
}
