namespace Sec.Business
{
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class ItensDasOrdensDeServicos
        {
            public static CrudResult<ItemDaOrdemDeServico> Insert(ItemDaOrdemDeServico value)
            {
                CrudResult<ItemDaOrdemDeServico> ret;
                using (ItensDasOrdensDeServicoFactory db = new ItensDasOrdensDeServicoFactory())
                    ret = db.Create(value);
                return ret;
            }
            public static CrudResult<ItemDaOrdemDeServico> List()
            {
                CrudResult<ItemDaOrdemDeServico> ret;
                using (ItensDasOrdensDeServicoFactory db = new ItensDasOrdensDeServicoFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<ItemDaOrdemDeServico> Filter(Expression<Func<ItemDaOrdemDeServico, bool>> where)
            {
                CrudResult<ItemDaOrdemDeServico> ret;
                using (ItensDasOrdensDeServicoFactory db = new ItensDasOrdensDeServicoFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<ItemDaOrdemDeServico> Find(object[] keys)
            {
                CrudResult<ItemDaOrdemDeServico> ret;
                using (ItensDasOrdensDeServicoFactory db = new ItensDasOrdensDeServicoFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<ItemDaOrdemDeServico> Update(ItemDaOrdemDeServico value)
            {
                CrudResult<ItemDaOrdemDeServico> ret;
                using (ItensDasOrdensDeServicoFactory db = new ItensDasOrdensDeServicoFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<ItemDaOrdemDeServico> Delete(ItemDaOrdemDeServico value)
            {
                CrudResult<ItemDaOrdemDeServico> ret;
                using (ItensDasOrdensDeServicoFactory db = new ItensDasOrdensDeServicoFactory())
                    ret = db.Delete(value);
                return ret;
            }
        }
    }
}