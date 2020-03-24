namespace Sec.Business
{
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class Setores
        {
            public static CrudResult<Setor> Insert(Setor value)
            {
                CrudResult<Setor> ret;
                using (SetoresFactory db = new SetoresFactory())
                    ret = db.Create(value);
                return ret;
            }
            public static CrudResult<Setor> List()
            {
                CrudResult<Setor> ret;
                using (SetoresFactory db = new SetoresFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<Setor> Filter(Expression<Func<Setor, bool>> where)
            {
                CrudResult<Setor> ret;
                using (SetoresFactory db = new SetoresFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<Setor> Find(object[] keys)
            {
                CrudResult<Setor> ret;
                using (SetoresFactory db = new SetoresFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<Setor> Update(Setor value)
            {
                CrudResult<Setor> ret;
                using (SetoresFactory db = new SetoresFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<Setor> Delete(Setor value)
            {
                CrudResult<Setor> ret;
                using (SetoresFactory db = new SetoresFactory())
                    ret = db.Delete(value);
                return ret;
            }
        }
    }
}