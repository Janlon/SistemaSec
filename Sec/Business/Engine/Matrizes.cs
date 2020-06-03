namespace Sec.Business
{
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class Matrizes
        {
            public static CrudResult<EmpresaMatriz> Insert(EmpresaMatriz value)
            {
                CrudResult<EmpresaMatriz> ret;
                using (MatrizesFactory db = new MatrizesFactory())
                    ret = db.Create(value);
                return ret;
            }
            public static CrudResult<EmpresaMatriz> List()
            {
                CrudResult<EmpresaMatriz> ret;
                using (MatrizesFactory db = new MatrizesFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<EmpresaMatriz> Filter(Expression<Func<EmpresaMatriz, bool>> where)
            {
                CrudResult<EmpresaMatriz> ret;
                using (MatrizesFactory db = new MatrizesFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<EmpresaMatriz> Find(object[] keys)
            {
                CrudResult<EmpresaMatriz> ret;
                using (MatrizesFactory db = new MatrizesFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<EmpresaMatriz> Update(EmpresaMatriz value)
            {
                CrudResult<EmpresaMatriz> ret;
                using (MatrizesFactory db = new MatrizesFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<EmpresaMatriz> Delete(EmpresaMatriz value)
            {
                CrudResult<EmpresaMatriz> ret;
                using (MatrizesFactory db = new MatrizesFactory())
                    ret = db.Delete(value);
                return ret;
            }
        }
    }
}