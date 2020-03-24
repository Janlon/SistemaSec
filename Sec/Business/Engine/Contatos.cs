namespace Sec.Business
{
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class Contatos
        {
            public static CrudResult<Contato> Insert(Contato value)
            {
                CrudResult<Contato> ret;
                using (ContatosFactory db = new ContatosFactory())
                    ret = db.Create(value);
                return ret;
            }
            public static CrudResult<Contato> List()
            {
                CrudResult<Contato> ret;
                using (ContatosFactory db = new ContatosFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<Contato> Filter(Expression<Func<Contato, bool>> where)
            {
                CrudResult<Contato> ret;
                using (ContatosFactory db = new ContatosFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<Contato> Find(object[] keys)
            {
                CrudResult<Contato> ret;
                using (ContatosFactory db = new ContatosFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<Contato> Update(Contato value)
            {
                CrudResult<Contato> ret;
                using (ContatosFactory db = new ContatosFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<Contato> Delete(Contato value)
            {
                CrudResult<Contato> ret;
                using (ContatosFactory db = new ContatosFactory())
                    ret = db.Delete(value);
                return ret;
            }
        }
    }
}