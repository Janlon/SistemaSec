namespace Sec.Business
{
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class SetoresDosEnderecos
        {
            public static CrudResult<SetorDoEndereco> Insert(SetorDoEndereco value)
            {
                CrudResult<SetorDoEndereco> ret;
                using (SetoresDosEnderecosFactory db = new SetoresDosEnderecosFactory())
                    ret = db.Create(value);
                return ret;
            }
            public static CrudResult<SetorDoEndereco> List()
            {
                CrudResult<SetorDoEndereco> ret;
                using (SetoresDosEnderecosFactory db = new SetoresDosEnderecosFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<SetorDoEndereco> Filter(Expression<Func<SetorDoEndereco, bool>> where)
            {
                CrudResult<SetorDoEndereco> ret;
                using (SetoresDosEnderecosFactory db = new SetoresDosEnderecosFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<SetorDoEndereco> Find(object[] keys)
            {
                CrudResult<SetorDoEndereco> ret;
                using (SetoresDosEnderecosFactory db = new SetoresDosEnderecosFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<SetorDoEndereco> Update(SetorDoEndereco value)
            {
                CrudResult<SetorDoEndereco> ret;
                using (SetoresDosEnderecosFactory db = new SetoresDosEnderecosFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<SetorDoEndereco> Delete(SetorDoEndereco value)
            {
                CrudResult<SetorDoEndereco> ret;
                using (SetoresDosEnderecosFactory db = new SetoresDosEnderecosFactory())
                    ret = db.Delete(value);
                return ret;
            }
        }
    }
}