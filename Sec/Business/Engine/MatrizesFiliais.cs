namespace Sec.Business
{
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class MatrizesFiliais
        {
            public static CrudResult<EmpresaMatrizFilial> Insert(EmpresaMatrizFilial value)
            {
                CrudResult<EmpresaMatrizFilial> ret;
                using (MatrizesFiliaisFactory db = new MatrizesFiliaisFactory())
                    ret = db.Create(value);
                return ret;
            }
            public static CrudResult<EmpresaMatrizFilial> List()
            {
                CrudResult<EmpresaMatrizFilial> ret;
                using (MatrizesFiliaisFactory db = new MatrizesFiliaisFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<EmpresaMatrizFilial> Filter(Expression<Func<EmpresaMatrizFilial, bool>> where)
            {
                CrudResult<EmpresaMatrizFilial> ret;
                using (MatrizesFiliaisFactory db = new MatrizesFiliaisFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<EmpresaMatrizFilial> Find(object[] keys)
            {
                CrudResult<EmpresaMatrizFilial> ret;
                using (MatrizesFiliaisFactory db = new MatrizesFiliaisFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<EmpresaMatrizFilial> Update(EmpresaMatrizFilial value)
            {
                CrudResult<EmpresaMatrizFilial> ret;
                using (MatrizesFiliaisFactory db = new MatrizesFiliaisFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<EmpresaMatrizFilial> Delete(EmpresaMatrizFilial value)
            {
                CrudResult<EmpresaMatrizFilial> ret;
                using (MatrizesFiliaisFactory db = new MatrizesFiliaisFactory())
                    ret = db.Delete(value);
                return ret;
            }
        }
    }
}