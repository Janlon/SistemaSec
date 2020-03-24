namespace Sec.Business
{
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class EnderecosDasPessoas
        {
            public static CrudResult<EnderecoDaPessoa> Insert(EnderecoDaPessoa value)
            {
                CrudResult<EnderecoDaPessoa> ret;
                using (EnderecosDasPessoasFactory db = new EnderecosDasPessoasFactory())
                    ret = db.Create(value);
                return ret;
            }
            public static CrudResult<EnderecoDaPessoa> List()
            {
                CrudResult<EnderecoDaPessoa> ret;
                using (EnderecosDasPessoasFactory db = new EnderecosDasPessoasFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<EnderecoDaPessoa> Filter(Expression<Func<EnderecoDaPessoa, bool>> where)
            {
                CrudResult<EnderecoDaPessoa> ret;
                using (EnderecosDasPessoasFactory db = new EnderecosDasPessoasFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<EnderecoDaPessoa> Find(object[] keys)
            {
                CrudResult<EnderecoDaPessoa> ret;
                using (EnderecosDasPessoasFactory db = new EnderecosDasPessoasFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<EnderecoDaPessoa> Update(EnderecoDaPessoa value)
            {
                CrudResult<EnderecoDaPessoa> ret;
                using (EnderecosDasPessoasFactory db = new EnderecosDasPessoasFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<EnderecoDaPessoa> Delete(EnderecoDaPessoa value)
            {
                CrudResult<EnderecoDaPessoa> ret;
                using (EnderecosDasPessoasFactory db = new EnderecosDasPessoasFactory())
                    ret = db.Delete(value);
                return ret;
            }
        }
    }
}