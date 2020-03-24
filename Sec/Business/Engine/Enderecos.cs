namespace Sec.Business
{
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class Enderecos
        {
            public static CrudResult<Endereco> Insert(Endereco value)
            {
                CrudResult<Endereco> ret;
                using (EnderecosFactory db = new EnderecosFactory())
                    ret = db.Create(value);
                return ret;
            }
            public static CrudResult<Endereco> List()
            {
                CrudResult<Endereco> ret;
                using (EnderecosFactory db = new EnderecosFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<Endereco> Filter(Expression<Func<Endereco, bool>> where)
            {
                CrudResult<Endereco> ret;
                using (EnderecosFactory db = new EnderecosFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<Endereco> Find(object[] keys)
            {
                CrudResult<Endereco> ret;
                using (EnderecosFactory db = new EnderecosFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<Endereco> Update(Endereco value)
            {
                CrudResult<Endereco> ret;
                using (EnderecosFactory db = new EnderecosFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<Endereco> Delete(Endereco value)
            {
                CrudResult<Endereco> ret;
                using (EnderecosFactory db = new EnderecosFactory())
                    ret = db.Delete(value);
                return ret;
            }
        }
    }
}