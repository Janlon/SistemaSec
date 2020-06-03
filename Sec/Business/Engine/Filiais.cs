namespace Sec.Business
{
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class Filiais
        {
            public static CrudResult<EmpresaFilial> Insert(EmpresaFilial value)
            {
                CrudResult<EmpresaFilial> ret;
                using (FiliaisFactory db = new FiliaisFactory())
                    ret = db.Create(value);
                return ret;
            }
            public static CrudResult<EmpresaFilial> List()
            {
                CrudResult<EmpresaFilial> ret;
                using (FiliaisFactory db = new FiliaisFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<EmpresaFilial> Filter(Expression<Func<EmpresaFilial, bool>> where)
            {
                CrudResult<EmpresaFilial> ret;
                using (FiliaisFactory db = new FiliaisFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<EmpresaFilial> Find(object[] keys)
            {
                CrudResult<EmpresaFilial> ret;
                using (FiliaisFactory db = new FiliaisFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<EmpresaFilial> Update(EmpresaFilial value)
            {
                CrudResult<EmpresaFilial> ret;
                using (FiliaisFactory db = new FiliaisFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<EmpresaFilial> Delete(EmpresaFilial value)
            {
                CrudResult<EmpresaFilial> ret;
                using (FiliaisFactory db = new FiliaisFactory())
                    ret = db.Delete(value);
                return ret;
            }
        }
    }
}