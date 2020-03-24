namespace Sec.Business
{
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class DocumentoDaPessoasDasPessoas
        {
            public static CrudResult<DocumentoDaPessoa> Insert(DocumentoDaPessoa value)
            {
                CrudResult<DocumentoDaPessoa> ret;
                using (DocumentosDasPessoasFactory db = new DocumentosDasPessoasFactory())
                    ret = db.Create(value);
                return ret;
            }
            public static CrudResult<DocumentoDaPessoa> List()
            {
                CrudResult<DocumentoDaPessoa> ret;
                using (DocumentosDasPessoasFactory db = new DocumentosDasPessoasFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<DocumentoDaPessoa> Filter(Expression<Func<DocumentoDaPessoa, bool>> where)
            {
                CrudResult<DocumentoDaPessoa> ret;
                using (DocumentosDasPessoasFactory db = new DocumentosDasPessoasFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<DocumentoDaPessoa> Find(object[] keys)
            {
                CrudResult<DocumentoDaPessoa> ret;
                using (DocumentosDasPessoasFactory db = new DocumentosDasPessoasFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<DocumentoDaPessoa> Update(DocumentoDaPessoa value)
            {
                CrudResult<DocumentoDaPessoa> ret;
                using (DocumentosDasPessoasFactory db = new DocumentosDasPessoasFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<DocumentoDaPessoa> Delete(DocumentoDaPessoa value)
            {
                CrudResult<DocumentoDaPessoa> ret;
                using (DocumentosDasPessoasFactory db = new DocumentosDasPessoasFactory())
                    ret = db.Delete(value);
                return ret;
            }
        }
    }
}