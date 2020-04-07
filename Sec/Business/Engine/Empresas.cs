namespace Sec.Business
{
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class Empresas
        {
            public static CrudResult<Empresa> Insert(Empresa value)
            {
                CrudResult<Empresa> ret;
                using (EmpresasFactory db = new EmpresasFactory())
                    ret = db.Create(value);
                return ret;
            }
            public static CrudResult<Empresa> List()
            {
                CrudResult<Empresa> ret;
                using (EmpresasFactory db = new EmpresasFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<Empresa> Filter(Expression<Func<Empresa, bool>> where)
            {
                CrudResult<Empresa> ret;
                using (EmpresasFactory db = new EmpresasFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<Empresa> Find(object[] keys)
            {
                CrudResult<Empresa> ret;
                using (EmpresasFactory db = new EmpresasFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<Empresa> Update(Empresa value)
            {
                CrudResult<Empresa> ret;
                using (EmpresasFactory db = new EmpresasFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<Empresa> Delete(Empresa value)
            {
                CrudResult<Empresa> ret;
                using (EmpresasFactory db = new EmpresasFactory())
                    ret = db.Delete(value);
                return ret;
            }
        }
    }
}