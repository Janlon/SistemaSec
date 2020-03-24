namespace Sec.Business
{
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class Imagens
        {
            public static CrudResult<Imagem> Insert(Imagem value)
            {
                CrudResult<Imagem> ret;
                using (ImagensFactory db = new ImagensFactory())
                    ret = db.Create(value);
                return ret;
            }
            public static CrudResult<Imagem> List()
            {
                CrudResult<Imagem> ret;
                using (ImagensFactory db = new ImagensFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<Imagem> Filter(Expression<Func<Imagem, bool>> where)
            {
                CrudResult<Imagem> ret;
                using (ImagensFactory db = new ImagensFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<Imagem> Find(object[] keys)
            {
                CrudResult<Imagem> ret;
                using (ImagensFactory db = new ImagensFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<Imagem> Update(Imagem value)
            {
                CrudResult<Imagem> ret;
                using (ImagensFactory db = new ImagensFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<Imagem> Delete(Imagem value)
            {
                CrudResult<Imagem> ret;
                using (ImagensFactory db = new ImagensFactory())
                    ret = db.Delete(value);
                return ret;
            }
        }
    }
}