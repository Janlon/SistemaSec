namespace Sec.Business
{
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class Pessoas
        {
            public static CrudResult<Pessoa> Insert(Pessoa value)
            {
                CrudResult<Pessoa> ret;
                using (PessoasFactory db = new PessoasFactory())
                    ret = db.Create(value);
                return ret;
            }
            public static CrudResult<Pessoa> List()
            {
                CrudResult<Pessoa> ret;
                using (PessoasFactory db = new PessoasFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<Pessoa> Filter(Expression<Func<Pessoa, bool>> where)
            {
                CrudResult<Pessoa> ret;
                using (PessoasFactory db = new PessoasFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<Pessoa> Find(object[] keys)
            {
                CrudResult<Pessoa> ret;
                using (PessoasFactory db = new PessoasFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<Pessoa> Update(Pessoa value)
            {
                CrudResult<Pessoa> ret;
                using (PessoasFactory db = new PessoasFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<Pessoa> Delete(Pessoa value)
            {
                CrudResult<Pessoa> ret;
                using (PessoasFactory db = new PessoasFactory())
                    ret = db.Delete(value);
                return ret;
            }
        }
    }
}