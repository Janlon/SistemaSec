namespace Sec.Business
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class Regras
        {
            public static CrudResult<IdentityRole> Insert(IdentityRole value)
            {
                CrudResult<IdentityRole> ret;
                using (RolesFactory db = new RolesFactory())
                    ret = db.Create(value);
                return ret;
            }
            public static CrudResult<IdentityRole> List()
            {
                CrudResult<IdentityRole> ret;
                using (RolesFactory db = new RolesFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<IdentityRole> Filter(Expression<Func<IdentityRole, bool>> where)
            {
                CrudResult<IdentityRole> ret;
                using (RolesFactory db = new RolesFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<IdentityRole> Find(object[] keys)
            {
                CrudResult<IdentityRole> ret;
                using (RolesFactory db = new RolesFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<IdentityRole> Update(IdentityRole value)
            {
                CrudResult<IdentityRole> ret;
                using (RolesFactory db = new RolesFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<IdentityRole> Delete(IdentityRole value)
            {
                CrudResult<IdentityRole> ret;
                using (RolesFactory db = new RolesFactory())
                    ret = db.Delete(value);
                return ret;
            }
        }
    }
}