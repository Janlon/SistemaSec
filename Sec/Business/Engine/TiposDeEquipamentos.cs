namespace Sec.Business
{
    using Generics.Extensoes;
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class TiposDeEquipamentos
        {
            public static CrudResult<TipoDeEquipamento> Reactivate(TipoDeEquipamento value)
            {
                CrudResult<TipoDeEquipamento> ret = null;
                if (value != null)
                {
                    using (TiposDeEquipamentosFactory db = new TiposDeEquipamentosFactory())
                        ret = db.Reactivate(value);
                }
                return ret;
            }
            public static CrudResult<TipoDeEquipamento> Deactivate(TipoDeEquipamento value)
            {
                CrudResult<TipoDeEquipamento> ret = null;
                if (value != null)
                {
                    using (TiposDeEquipamentosFactory db = new TiposDeEquipamentosFactory())
                        ret = db.Deactivate(value);
                }
                return ret;
            }
            public static CrudResult<TipoDeEquipamento> Insert(TipoDeEquipamento value)
            {
                CrudResult<TipoDeEquipamento> ret = null;
                if (value != null)
                {
                    using (TiposDeEquipamentosFactory db = new TiposDeEquipamentosFactory())
                        ret = db.Create(value);
                }
                return ret;
            }
            public static CrudResult<TipoDeEquipamento> List()
            {
                CrudResult<TipoDeEquipamento> ret;
                using (TiposDeEquipamentosFactory db = new TiposDeEquipamentosFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<TipoDeEquipamento> Filter(Expression<Func<TipoDeEquipamento, bool>> where)
            {
                CrudResult<TipoDeEquipamento> ret;
                using (TiposDeEquipamentosFactory db = new TiposDeEquipamentosFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<TipoDeEquipamento> Find(object[] keys)
            {
                CrudResult<TipoDeEquipamento> ret;
                using (TiposDeEquipamentosFactory db = new TiposDeEquipamentosFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<TipoDeEquipamento> Update(TipoDeEquipamento value)
            {
                CrudResult<TipoDeEquipamento> ret;
                using (TiposDeEquipamentosFactory db = new TiposDeEquipamentosFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<TipoDeEquipamento> Delete(TipoDeEquipamento value)
            {
                CrudResult<TipoDeEquipamento> ret;
                try
                {
                    using (TiposDeEquipamentosFactory db = new TiposDeEquipamentosFactory())
                        ret = db.Delete(value);
                }
                catch (Exception ex) { ex.Log(); ret = new CrudResult<TipoDeEquipamento>(value); }
                return ret;
            }
        }
    }
}
