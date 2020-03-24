namespace Sec.Business
{
    using Sec.Business.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class ObjectExtensions
    {
        /// <summary>
        /// Extension for 'Object' that copies the properties to a destination object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        public static void CopyProperties(this object source, object destination)
        {
            // If any this null throw an exception
            if (source == null || destination == null)
                throw new Exception("Source or/and Destination Objects are null");
            // Getting the Types of the objects
            Type typeDest = destination.GetType();
            Type typeSrc = source.GetType();
            // Collect all the valid properties to map
            var results = from srcProp in typeSrc.GetProperties()
                          let targetProperty = typeDest.GetProperty(srcProp.Name)
                          where srcProp.CanRead
                          && targetProperty != null
                          && (targetProperty.GetSetMethod(true) != null && !targetProperty.GetSetMethod(true).IsPrivate)
                          && (targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) == 0
                          && targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType)
                          select new { sourceProperty = srcProp, targetProperty = targetProperty };
            //map the properties
            foreach (var props in results)
            {
                props.targetProperty.SetValue(destination, props.sourceProperty.GetValue(source, null), null);
            }
        }
    }


    /// <summary>
    /// Fábrica de entidades.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Factory<T> : FactoryBase<T>, IDisposable where T : class
    {
        #region Manutenção
        private bool disposedValue = false;
        public Db Bd { get; internal set; }
        #endregion

        #region Instância
        public Factory() { disposedValue = false; Bd = new Db(); }
        public Factory(Db context) { disposedValue = false; Bd = context; }
        private void CleanUp() { }
        protected virtual void Dispose(bool disposing) { if (!disposedValue) { if (disposing) { CleanUp(); } disposedValue = true; } }
        ~Factory() { Dispose(false); }
        public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }
        #endregion

        #region Suporte
        
        private object[] FindPrimaryKeyDefaultValues(T entity)
        {
            List<object> ret = new List<object>();
            var db = Bd;
            var ctx = ((IObjectContextAdapter)db).ObjectContext;
            Type type = entity.GetType();
            PropertyInfo[] properties = entity.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var attribute = Attribute.GetCustomAttribute(property, typeof(KeyAttribute))                    as KeyAttribute;
                if (attribute != null)
                    ret.Add( property.GetValue(entity) );
            }
            return ret.ToArray();
        }
        /// <summary>
        /// Verifica se esta entidade pode sofrer "soft delete".
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool SoftDeletable()
        {
            bool ret = false;
            Type t = typeof(T);
            foreach (var prop in t.GetProperties())
            {
                var attr = prop.GetCustomAttributes(false)
                               .OfType<ActivationAttribute>()
                               .FirstOrDefault();
                ret = (attr != null);
                if (ret) break;
            }
            return ret;
        }
        /// <summary>
        /// Retorna o nome da propriedade "soft delete", se existir.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string PropertyNameByAttribute()
        {
            string ret = "";
            Type t = typeof(T);
            Attribute[] attrs = Attribute.GetCustomAttributes(t); 
            foreach (Attribute attr in attrs)
            {
                if (attr is ActivationAttribute)
                {
                    ActivationAttribute a = (ActivationAttribute)attr;
                    ret =t.GetProperties()
                        .Where(p=>p.Attributes
                        .Equals(a))
                        .FirstOrDefault().Name;
                }
            }
            return ret;
        }
        public static IEnumerable<T> ApplyFilter(List<T> sourceList, string propertyName, bool value)
        {
            foreach (T item in sourceList)
            {
                object propertyValue = GetPropertyValue(item, propertyName);
                if (ApplyOperator(propertyValue, value))
                    yield return item;
            }
        }
        private static object GetPropertyValue(object item, string propertyName)
        {
            PropertyInfo property = item.GetType().GetProperty(propertyName);
            return property.GetValue(item);
        }
        private static bool ApplyOperator(object propertyValue, object value)
        {
            return propertyValue.Equals(value);
        }
        #endregion


        #region Métodos públicos
        public CrudResult<T> GetById(dynamic keys)
        {
            CrudResult<T> ret = new CrudResult<T>() { Action = CrudAction.Select };
            try { ret.Result.Add(Bd.Set<T>().Find(keys)); }
            catch (DataException ex) { ret.AddError(ex); }
            catch (DbException ex) { ret.AddError(ex); }
            catch (Exception ex) { ret.AddError(ex); }
            ret.Finish();
            return ret;
        }
        #endregion

        #region Métodos públicos implementados
        public override CrudResult<T> Create(T value)
        {
            CrudResult<T> ret = new CrudResult<T>(value);
            try
            {
                DbContextTransaction transa = Bd.Database.BeginTransaction();
                Bd.Set<T>().Add(value);
                Bd.Entry(value).State = EntityState.Added;
                var vr = Bd.Entry(value).GetValidationResult();
                if (vr.IsValid)
                {
                    ret.SetAffected(Bd.SaveChanges());
                    transa.Commit();
                }
                else
                {
                    foreach (var ve in vr.ValidationErrors)
                        ret.AddError(ve.PropertyName, ve.ErrorMessage);
                    transa.Rollback();
                }
            }
            catch (DataException ex) { ret.AddError(ex); }
            catch (DbException ex) { ret.AddError(ex); }
            catch (Exception ex) { ret.AddError(ex); }
            ret.Finish();
            return ret;
        }

        public override CrudResult<T> Retrieve(Expression<Func<T, bool>> value)
        {
            CrudResult<T> ret = new CrudResult<T>() { Action = CrudAction.Select };
            try 
            { 
                ret.Result.Add(Bd.Set<T>().Where(value).FirstOrDefault());
                ret.SetAffected(ret.Result.Count);
            }
            catch (DataException ex) { ret.AddError(ex); }
            catch (DbException ex) { ret.AddError(ex); }
            catch (Exception ex) { ret.AddError(ex); }
            ret.Finish();
            return ret;
        }

        public override CrudResult<T> Update(T value)
        {
            CrudResult<T> ret = new CrudResult<T>(value) { Action = CrudAction.Update };
            try
            {
                DbContextTransaction transa = Bd.Database.BeginTransaction();
                Bd.Set<T>().Add(value);
                Bd.Entry(value).State = EntityState.Modified;
                var vr = Bd.Entry(value).GetValidationResult();
                if (vr.IsValid)
                {
                    ret.SetAffected(Bd.SaveChanges());
                    transa.Commit();
                }
                else
                {
                    foreach (var ve in vr.ValidationErrors)
                        ret.AddError(ve.PropertyName, ve.ErrorMessage);
                    transa.Rollback();
                }
            }
            catch (DataException ex) { ret.AddError(ex); }
            catch (DbException ex) { ret.AddError(ex); }
            catch (Exception ex) { ret.AddError(ex); }
            ret.Finish();
            return ret;
        }

        public override CrudResult<T> Delete(T value)
        {
            CrudResult<T> ret = new CrudResult<T>(value) { Action = CrudAction.Delete };
            try
            {
                DbContextTransaction transa = Bd.Database.BeginTransaction();
                T item = Bd.Set<T>().Find(FindPrimaryKeyDefaultValues(value));
                if (item != null)
                {
                    Bd.Set<T>().Remove(item);
                    ret.SetAffected(Bd.SaveChanges());
                    transa.Commit();
                }
            }
            catch (DataException ex) { ret.AddError(ex); }
            catch (DbException ex) { ret.AddError(ex); }
            catch (Exception ex) { ret.AddError(ex); }
            ret.Finish();
            return ret;
        }

        public override CrudResult<T> Deactivate(T value)
        {
            CrudResult<T> ret = new CrudResult<T>(value) { Action = CrudAction.Deactivate };
            bool ok = false;
            if (SoftDeletable())
            {
                try
                {
                    Type t = value.GetType();
                    foreach (var prop in t.GetProperties())
                    {
                        var attr = prop.GetCustomAttributes(false)
                                       .OfType<ActivationAttribute>()
                                       .FirstOrDefault();
                        if (attr != null)
                        { prop.SetValue(value, false); ok = true; }
                    }
                    if (ok)
                    {
                        try
                        {
                            DbContextTransaction transa = Bd.Database.BeginTransaction();
                            T item = Bd.Set<T>().Find(FindPrimaryKeyDefaultValues(value));
                            if (item != null)
                            {
                                Bd.Entry(item).State = EntityState.Modified;
                                value.CopyProperties(item);
                                var vr = Bd.Entry(item).GetValidationResult();
                                if (vr.IsValid)
                                {
                                    ret.SetAffected(Bd.SaveChanges());
                                    transa.Commit();
                                }
                                else
                                {
                                    foreach (var ve in vr.ValidationErrors)
                                        ret.AddError(ve.PropertyName, ve.ErrorMessage);
                                    transa.Rollback();
                                }
                            }
                            else
                                ret.AddError(new Exception("O registro não foi localizado."));
                        }
                        catch (DataException ex) { ret.AddError(ex); }
                        catch (DbException ex) { ret.AddError(ex); }
                        catch (Exception ex) { ret.AddError(ex); }
                        ret.Finish();
                    }
                    else
                    {
                        ret.AddError(new Exception("O item indicado não permite exclusão lógica"));
                    }
                }
                catch (Exception ex)
                {
                    ret = new CrudResult<T>();
                    ret.AddError(ex);
                }
            }
            else
            {
                ret.AddError(new Exception("O item indicado não permite exclusão lógica"));
            }
            return ret;
        }

        public override CrudResult<T> Reactivate(T value)
        {
            CrudResult<T> ret = new CrudResult<T>(value) { Action = CrudAction.Reactivate };
            bool ok = false;
            if (SoftDeletable())
            {
                try
                {
                    Type t = value.GetType();
                    foreach (var prop in t.GetProperties())
                    {
                        var attr = prop.GetCustomAttributes(false)
                                       .OfType<ActivationAttribute>()
                                       .FirstOrDefault();
                        if (attr != null)
                        { prop.SetValue(value, true); ok = true; }
                    }
                    if (ok)
                    {
                        try
                        {
                            DbContextTransaction transa = Bd.Database.BeginTransaction();
                            T item = Bd.Set<T>().Find(FindPrimaryKeyDefaultValues(value));
                            if (item != null)
                            {
                                Bd.Entry(item).State = EntityState.Modified;
                                value.CopyProperties(item);
                                var vr = Bd.Entry(item).GetValidationResult();
                                if (vr.IsValid)
                                {
                                    ret.SetAffected(Bd.SaveChanges());
                                    transa.Commit();
                                }
                                else
                                {
                                    foreach (var ve in vr.ValidationErrors)
                                        ret.AddError(ve.PropertyName, ve.ErrorMessage);
                                    transa.Rollback();
                                }
                            }                            
                        }
                        catch (DataException ex) { ret.AddError(ex); }
                        catch (DbException ex) { ret.AddError(ex); }
                        catch (Exception ex) { ret.AddError(ex); }
                        ret.Finish();
                    }
                    else
                    {
                        ret.AddError(new Exception("O item indicado não permite exclusão lógica"));
                    }
                }
                catch (Exception ex)
                {
                    ret = new CrudResult<T>();
                    ret.AddError(ex);
                }
            }
            else
            {
                ret.AddError(new Exception("O item indicado não permite exclusão lógica"));
            }
            return ret;
        }

        public override CrudResult<T> List()
        {
            CrudResult<T> ret = new CrudResult<T>() { Action = CrudAction.Select };
            try 
            { 
                ret.Result.AddRange(Bd.Set<T>());
                ret.SetAffected(ret.Result.Count);
            }
            catch (DataException ex) { ret.AddError(ex); }
            catch (DbException ex) { ret.AddError(ex); }
            catch (Exception ex) { ret.AddError(ex); }
            ret.Finish();
            return ret;
        }

        public override CrudResult<T> Filter(Expression<Func<T, bool>> value)
        {
            CrudResult<T> ret = new CrudResult<T>() { Action = CrudAction.Select };
            List<T> temp = new List<T>();
            try 
            { 
                temp.AddRange(Bd.Set<T>().Where(value));
                if (SoftDeletable())
                {
                    string ppt = PropertyNameByAttribute();
                    temp.AddRange( ApplyFilter(temp, ppt, true) );
                }
                ret.SetResult(temp);
                ret.SetAffected(temp.Count);
            }
            catch (DataException ex) { ret.AddError(ex); }
            catch (DbException ex) { ret.AddError(ex); }
            catch (Exception ex) { ret.AddError(ex); }
            ret.Finish();
            return ret;
        }
        #endregion




        
    }
}
