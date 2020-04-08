namespace Sec.Business
{
    using Sec.Business.Core;
    using Sec.Models;
    using System;
    using System.Linq.Expressions;

    public partial class Engine
    {
        public static class Equipamentos
        {
            public static CrudResult<Equipamento> Insert(Equipamento value)
            {
                CrudResult<Equipamento> ret;
                using (EquipamentosFactory db = new EquipamentosFactory())
                    ret = db.Create(value);
                return ret;
            }
            public static CrudResult<Equipamento> List()
            {
                CrudResult<Equipamento> ret;
                using (EquipamentosFactory db = new EquipamentosFactory())
                    ret = db.List();
                return ret;
            }
            public static CrudResult<Equipamento> Filter(Expression<Func<Equipamento, bool>> where)
            {
                CrudResult<Equipamento> ret;
                using (EquipamentosFactory db = new EquipamentosFactory())
                    ret = db.Filter(where);
                return ret;
            }
            public static CrudResult<Equipamento> Find(object[] keys)
            {
                CrudResult<Equipamento> ret;
                using (EquipamentosFactory db = new EquipamentosFactory())
                    ret = db.GetById(keys);
                return ret;
            }
            public static CrudResult<Equipamento> Update(Equipamento value)
            {
                CrudResult<Equipamento> ret;
                using (EquipamentosFactory db = new EquipamentosFactory())
                    ret = db.Update(value);
                return ret;
            }
            public static CrudResult<Equipamento> Delete(Equipamento value)
            {
                CrudResult<Equipamento> ret;
                using (EquipamentosFactory db = new EquipamentosFactory())
                    ret = db.Delete(value);
                return ret;
            }

            public static CrudResult<Equipamento> QrCode(object[] keys)
            {
                CrudResult<Equipamento> ret;
                using (EquipamentosFactory db = new EquipamentosFactory())
                    ret = db.GetById(keys);

                return ret;
            }
        }
    }
}