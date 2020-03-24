namespace Sec.Business
{
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class Usuarios
        {
            public static CrudResult<Usuario> Insert(Usuario value)
            {
                CrudResult<Usuario> ret;
                using (UsuariosFactory db = new UsuariosFactory())
                    ret = db.Create(value);
                return ret;
            }
            public static CrudResult<Usuario> List()
            {
                CrudResult<Usuario> ret;
                using (UsuariosFactory db = new UsuariosFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<Usuario> Filter(Expression<Func<Usuario, bool>> where)
            {
                CrudResult<Usuario> ret;
                using (UsuariosFactory db = new UsuariosFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<Usuario> Find(object[] keys)
            {
                CrudResult<Usuario> ret;
                using (UsuariosFactory db = new UsuariosFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<Usuario> Update(Usuario value)
            {
                CrudResult<Usuario> ret;
                using (UsuariosFactory db = new UsuariosFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<Usuario> Delete(Usuario value)
            {
                CrudResult<Usuario> ret;
                using (UsuariosFactory db = new UsuariosFactory())
                    ret = db.Delete(value);
                return ret;
            }
        }
    }
}