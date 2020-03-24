namespace Generic.Interfaces
{
    #region Espaços de trabalho
    using Sec.Dados;
    using Sec.Dal;
    using Sec.Helpers.Errors;
    using Sec.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Data.Entity.Core;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    #endregion


    public interface IEntityCrud<T> where T : class
    {
        DbResponse<T> Insert(T value);
        DbResponse<T> Update(T value);
        DbResponse<T> Remove(T value);
        DbResponse<T> Reactivate(T value);
        DbResponse<T> Deactivate(T value);
        DbResponse<T> List();
        DbResponse<T> Filter(Expression<Func<T, bool>> predicate);
        DbResponse<T> FirstOrDefault(Expression<Func<T, bool>> filters);
        DbResponse<T> GetById(object id);
    }

    public class TipoDeDocumentoFactory : FactoryBase<TipoDeDocumento>    { }


    public class FactoryBase<T> : IDisposable, IEntityCrud<T> where T : class
    {

        #region IDisposable Support
        private bool disposedValue;

        private void CleanUp()
        {
            if (_db != null) { _db.Dispose(); }
        }
        protected virtual void Dispose(bool disposing) { if (!disposedValue) { if (disposing) { CleanUp(); } } disposedValue = true; }
        ~FactoryBase() { Dispose(false); }
        public FactoryBase() { disposedValue = false; }
        void IDisposable.Dispose() { Dispose(true); GC.SuppressFinalize(this); }
        #endregion


        private DB _db { get; set; }
        internal DB Dados
        {
            get
            {
                if (_db == null)
                    _db = (new DB());
                return _db;
            }
        }
        private void SetActive(bool setValue, T entity)
        {
            try
            {
                Type tipo = typeof(T);
                // Obter a chave de criptografia:
                PropertyInfo propriedadeDeAtivacao = tipo
                    .GetProperties()
                    .Where(p => p.GetCustomAttributes(typeof(ActivationAttribute), true)
                    .Any(a => p.PropertyType == typeof(String)))
                    .FirstOrDefault();
                if (propriedadeDeAtivacao != null)
                    propriedadeDeAtivacao.SetValue(entity, setValue);
            }
            catch (Exception ex) { ex.Log(); }
        }
        private EntityKey GetEntityKey(DbContext context, T entity)
        {
            if (entity != null)
            {
                var oc = ((IObjectContextAdapter)context).ObjectContext;
                ObjectStateEntry ose;
                if (null != entity && oc.ObjectStateManager.TryGetObjectStateEntry(entity, out ose))
                    return ose.EntityKey;
            }
            return null;
        }
        private EntityKey GetEntityKey(DbContext context, DbEntityEntry<T> dbEntityEntry)
        {
            if (dbEntityEntry != null)
                return GetEntityKey(context, dbEntityEntry.Entity);
            return null;
        }

        /// <summary>
        /// Desativar este registro.
        /// </summary>
        /// <param name="value">Entidade a ser desativada, se tiver a propriedade equivalente.</param>
        /// <returns>Objeto do tipo <see cref="DbResponse{T}"/>.</returns>
        public DbResponse<T> Deactivate(T value)
        {
            DbResponse<T> ret = new DbResponse<T>(DbAction.Deactivate);
            try {
                using (DB db = Dados)
                {
                    T t = db
                        .Set<T>()
                        .Find(GetEntityKey(db, value));
                    if (t != null)
                    {
                        SetActive(false, value);
                        db.Set<T>().Add(value);
                        db.Entry(value).State = EntityState.Modified;
                        db.SaveChanges();
                        ret.AddResult(t);
                    }
                }
            }
            catch (Exception ex) { ret.AddError("Generic", ex.Message); }
            return ret;
        }

        /// <summary>
        /// Retorna todos os registros cadastrados que cumpram o(s) filtro(s) especificado(s).
        /// </summary>
        /// <param name="filters">Cláusulas de filtragem.</param>
        /// <returns>Objeto do tipo <see cref="DbResponse{T}"/>.</returns>
        public DbResponse<T> Filter(Expression<Func<T, bool>> filters)
        {
            DbResponse<T> ret = new DbResponse<T>(DbAction.List);
            try
            {
                using (DB db = Dados)
                    ret.AddResult(db.Set<T>()
                        .Where(filters)
                        .ToList(), true);
            }
            catch (Exception ex) { ret.AddError("Generic", ex.Message); }
            return ret;
        }

        /// <summary>
        /// Retorna o primeiro dos registros cadastrados que cumpram o(s) filtro(s) especificado(s).
        /// </summary>
        /// <param name="filters">Cláusulas de filtragem.</param>
        /// <returns>Objeto do tipo <see cref="DbResponse{T}"/>.</returns>
        public DbResponse<T> FirstOrDefault(Expression<Func<T, bool>> filters)
        {
            DbResponse<T> ret = new DbResponse<T>(DbAction.List);
            try
            {
                using (DB db = Dados)
                    ret.AddResult(db.Set<T>()
                        .Where(filters)
                        .FirstOrDefault(), true);
            }
            catch (Exception ex) { ret.AddError("Generic", ex.Message); }
            return ret;
        }

        /// <summary>
        /// Retorna o primeiro dos registros cadastrados na ordem informada que cumpram o(s) filtro(s) especificado(s).
        /// </summary>
        /// <param name="filters">Cláusulas de filtragem.</param>
        /// <returns>Objeto do tipo <see cref="DbResponse{T}"/>.</returns>
        public DbResponse<T> FirstOrDefault(Expression<Func<T, bool>> filters, Expression<Func<T, string>> sort)
        {
            DbResponse<T> ret = new DbResponse<T>(DbAction.List);
            try
            {
                using (DB db = Dados)
                    ret.AddResult(db.Set<T>()
                        .Where(filters)
                        .OrderBy(sort)
                        .FirstOrDefault(), true);
            }
            catch (Exception ex) { ret.AddError("Generic", ex.Message); }
            return ret;
        }

        /// <summary>
        /// Retorna um registro com base na(s) chave(s) primária(s).
        /// </summary>
        /// <param name="id">Chaves primárias para a busca.</param>
        /// <returns>Objeto do tipo <see cref="DbResponse{T}"/>.</returns>
        public DbResponse<T> GetById(object id)
        {
            DbResponse<T> ret = new DbResponse<T>(DbAction.Reactivate);
            try
            {
                using (DB db = Dados)
                {
                    T t = db
                        .Set<T>()
                        .Find(id);
                    if (t != null)
                        ret.AddResult(t);
                }
            }
            catch (Exception ex) { ret.AddError("Generic", ex.Message); }
            return ret;
        }

        /// <summary>
        /// Incluir o registro informado.
        /// </summary>
        /// <param name="value">Registro a ser salvo.</param>
        /// <returns>Objeto do tipo <see cref="DbResponse{T}"/>.</returns>
        public DbResponse<T> Insert(T value)
        {
            DbResponse<T> ret = new DbResponse<T>(DbAction.Reactivate);
            try
            {
                using (DB db = Dados)
                {
                    db.Set<T>()
                        .Add(value);
                    var vr = db.Entry(value).GetValidationResult();
                    if (!vr.IsValid)
                        foreach (var ve in vr.ValidationErrors)
                            ret.AddError(ve.PropertyName, ve.ErrorMessage);
                    else
                    {
                        db.ChangeTracker.DetectChanges();
                        db.SaveChanges();
                        ret.AddResult(value);
                    }
                }
            }
            catch (Exception ex) { ret.AddError("Generic", ex.Message); }
            return ret;
        }

        /// <summary>
        /// Retorna a lista completa de registros deste tipo.
        /// </summary>
        /// <returns>Objeto do tipo <see cref="DbResponse{T}"/>.</returns>
        public DbResponse<T> List()
        {
            DbResponse<T> ret = new DbResponse<T>(DbAction.List);
            try
            {
                using (DB db = Dados)
                    ret.AddResult(db.Set<T>()
                        .ToList(), true);
            }
            catch (Exception ex) { ret.AddError("Generic", ex.Message); }
            return ret;
        }

        /// <summary>
        /// Reativar este registro.
        /// </summary>
        /// <param name="value">Entidade a ser reativada, se tiver a propriedade equivalente.</param>
        /// <returns>Objeto do tipo <see cref="DbResponse{T}"/>.</returns>
        public DbResponse<T> Reactivate(T value)
        {
            DbResponse<T> ret = new DbResponse<T>(DbAction.Reactivate);
            try
            {
                using (DB db = Dados)
                {
                    T t = db
                        .Set<T>()
                        .Find(GetEntityKey(db, value));
                    if (t != null)
                    {
                        SetActive(true, value);
                        db.Set<T>().Add(value);
                        db.Entry(value).State = EntityState.Modified;
                        db.SaveChanges();
                        ret.AddResult(t);
                    }
                }
            }
            catch (Exception ex) { ret.AddError("Generic", ex.Message); }
            return ret;
        }

        /// <summary>
        /// Remove efetivamente um registro da base de dados.
        /// </summary>
        /// <param name="value">Registro a ser excluído.</param>
        /// <returns>Objeto do tipo <see cref="DbResponse{T}"/>.</returns>
        public DbResponse<T> Remove(T value)
        {
            DbResponse<T> ret = new DbResponse<T>(DbAction.Remove);
            try
            {
                using (DB db = Dados)
                {
                    T t = db
                        .Set<T>()
                        .Find(GetEntityKey(db, value));
                    if (t != null)
                    {
                        db.Set<T>().Remove(value);
                        db.Entry(value).State = EntityState.Modified;
                        db.SaveChanges();
                        ret.AddResult(t);
                    }
                }
            }
            catch (Exception ex) { ret.AddError("Generic", ex.Message); }
            return ret;
        }

        /// <summary>
        /// Atualiza os dados para o registro informado.
        /// </summary>
        /// <param name="value">Registro com os dados atualizados.</param>
        /// <returns>Objeto do tipo <see cref="DbResponse{T}"/>.</returns>
        public DbResponse<T> Update(T value)
        {
            DbResponse<T> ret = new DbResponse<T>(DbAction.Update);
            try
            {
                using (DB db = Dados)
                {
                    T t = db
                        .Set<T>()
                        .Find(GetEntityKey(db, value));
                    if (t != null)
                    {
                        db.Set<T>().Add(value);
                        db.Entry(value).State = EntityState.Modified;
                        db.SaveChanges();
                        ret.AddResult(t);
                    }
                }
            }
            catch (Exception ex) { ret.AddError("Generic", ex.Message); }
            return ret;
        }
    }
}
