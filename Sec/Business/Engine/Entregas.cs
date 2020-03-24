namespace Sec.Business
{
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class Entregas
        {
            public static CrudResult<EntregaDoItemDaOrdemDeServico> Insert(EntregaDoItemDaOrdemDeServico value)
            {
                CrudResult<EntregaDoItemDaOrdemDeServico> ret;
                using (EntregasFactory db = new EntregasFactory())
                    ret = db.Create(value);
                return ret;
            }
            public static CrudResult<EntregaDoItemDaOrdemDeServico> List()
            {
                CrudResult<EntregaDoItemDaOrdemDeServico> ret;
                using (EntregasFactory db = new EntregasFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<EntregaDoItemDaOrdemDeServico> Filter(Expression<Func<EntregaDoItemDaOrdemDeServico, bool>> where)
            {
                CrudResult<EntregaDoItemDaOrdemDeServico> ret;
                using (EntregasFactory db = new EntregasFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<EntregaDoItemDaOrdemDeServico> Find(object[] keys)
            {
                CrudResult<EntregaDoItemDaOrdemDeServico> ret;
                using (EntregasFactory db = new EntregasFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<EntregaDoItemDaOrdemDeServico> Update(EntregaDoItemDaOrdemDeServico value)
            {
                CrudResult<EntregaDoItemDaOrdemDeServico> ret;
                using (EntregasFactory db = new EntregasFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<EntregaDoItemDaOrdemDeServico> Delete(EntregaDoItemDaOrdemDeServico value)
            {
                CrudResult<EntregaDoItemDaOrdemDeServico> ret;
                using (EntregasFactory db = new EntregasFactory())
                    ret = db.Delete(value);
                return ret;
            }
        }
    }
}