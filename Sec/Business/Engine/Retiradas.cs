namespace Sec.Business
{
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class Retiradas
        {
            public static CrudResult<RetiradaDoItemDaOrdemDeServico> Insert(RetiradaDoItemDaOrdemDeServico value)
            {
                CrudResult<RetiradaDoItemDaOrdemDeServico> ret;
                using (RetiradasFactory db = new RetiradasFactory())
                    ret = db.Create(value);
                return ret;
            }
            public static CrudResult<RetiradaDoItemDaOrdemDeServico> List()
            {
                CrudResult<RetiradaDoItemDaOrdemDeServico> ret;
                using (RetiradasFactory db = new RetiradasFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<RetiradaDoItemDaOrdemDeServico> Filter(Expression<Func<RetiradaDoItemDaOrdemDeServico, bool>> where)
            {
                CrudResult<RetiradaDoItemDaOrdemDeServico> ret;
                using (RetiradasFactory db = new RetiradasFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<RetiradaDoItemDaOrdemDeServico> Find(object[] keys)
            {
                CrudResult<RetiradaDoItemDaOrdemDeServico> ret;
                using (RetiradasFactory db = new RetiradasFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<RetiradaDoItemDaOrdemDeServico> Update(RetiradaDoItemDaOrdemDeServico value)
            {
                CrudResult<RetiradaDoItemDaOrdemDeServico> ret;
                using (RetiradasFactory db = new RetiradasFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<RetiradaDoItemDaOrdemDeServico> Delete(RetiradaDoItemDaOrdemDeServico value)
            {
                CrudResult<RetiradaDoItemDaOrdemDeServico> ret;
                using (RetiradasFactory db = new RetiradasFactory())
                    ret = db.Delete(value);
                return ret;
            }
        }
    }
}