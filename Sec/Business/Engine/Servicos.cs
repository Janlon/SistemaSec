namespace Sec.Business
{
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class Servicos
        {
            public static CrudResult<Servico> Insert(Servico value)
            {
                CrudResult<Servico> ret;
                using (ServicosFactory db = new ServicosFactory())
                    ret = db.Create(value);
                return ret;
            }
            public static CrudResult<Servico> List()
            {
                CrudResult<Servico> ret;
                using (ServicosFactory db = new ServicosFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<Servico> Filter(Expression<Func<Servico, bool>> where)
            {
                CrudResult<Servico> ret;
                using (ServicosFactory db = new ServicosFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<Servico> Find(object[] keys)
            {
                CrudResult<Servico> ret;
                using (ServicosFactory db = new ServicosFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<Servico> Update(Servico value)
            {
                CrudResult<Servico> ret;
                using (ServicosFactory db = new ServicosFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<Servico> Delete(Servico value)
            {
                CrudResult<Servico> ret;
                using (ServicosFactory db = new ServicosFactory())
                    ret = db.Delete(value);
                return ret;
            }
        }
    }
}