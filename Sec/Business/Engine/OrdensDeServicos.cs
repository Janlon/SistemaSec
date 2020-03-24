namespace Sec.Business
{
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class OrdensDeServicos
        {
            public static CrudResult<OrdemDeServico> Insert(OrdemDeServico value)
            {
                CrudResult<OrdemDeServico> ret;
                using (OrdensDeServicoFactory db = new OrdensDeServicoFactory())
                    ret = db.Create(value);
                return ret;
            }
            public static CrudResult<OrdemDeServico> List()
            {
                CrudResult<OrdemDeServico> ret;
                using (OrdensDeServicoFactory db = new OrdensDeServicoFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<OrdemDeServico> Filter(Expression<Func<OrdemDeServico, bool>> where)
            {
                CrudResult<OrdemDeServico> ret;
                using (OrdensDeServicoFactory db = new OrdensDeServicoFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<OrdemDeServico> Find(object[] keys)
            {
                CrudResult<OrdemDeServico> ret;
                using (OrdensDeServicoFactory db = new OrdensDeServicoFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<OrdemDeServico> Update(OrdemDeServico value)
            {
                CrudResult<OrdemDeServico> ret;
                using (OrdensDeServicoFactory db = new OrdensDeServicoFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<OrdemDeServico> Delete(OrdemDeServico value)
            {
                CrudResult<OrdemDeServico> ret;
                using (OrdensDeServicoFactory db = new OrdensDeServicoFactory())
                    ret = db.Delete(value);
                return ret;
            }
        }
    }
}